﻿using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Services.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryManagement.Views;

/// <summary>
/// Окно "Инвентаризация"
/// </summary>
public partial class InventoryWindow : Window
{
    //Коллекция, отвечающая за отображение элементов на интерфейсе
    private ObservableCollection<Inventory> _inventories;

    //Зависимости
    private readonly DataManager _dataManager;
    private readonly IUserService _userService;

    private static int _quantityOnStock;

    private static int _actualQuantity;

    private static string _portName;

    public string Role { get; set; }

    //Внедрение зависимостей
    public InventoryWindow(DataManager dataManager, IUserService userService)
    {
        InitializeComponent();

        _dataManager = dataManager;
        _userService= userService;

        //Добавляем в коллекцию данные из БД
        _inventories = new(_dataManager.Inventories.GetAllInventories().OrderBy(x => x.Name));

        //Выводим всё, что есть в коллекции на интерфейс
        dgInventory.ItemsSource = _inventories;

        //Валидация полей, чтобы писали корректные данные
        txtQuantityOnStock.LostFocus += (sender, e) =>
        {
            if (int.TryParse(txtQuantityOnStock.Text, out int quantity))
                _quantityOnStock = quantity;

            else
            {
                txtQuantityOnStock.Background = Brushes.Red;
                MessageBox.Show("Введите число в поле \"Количество на складе\"");
            }
        };

        txtActualQuantity.LostFocus += (sender, e) =>
        {
            if (int.TryParse(txtActualQuantity.Text, out int actual))
                _actualQuantity = actual;

            else
            {
                txtActualQuantity.Background = Brushes.Red;
                MessageBox.Show("Введите число в поле \"Цена\"");
            }
        };

        foreach (var port in SerialPort.GetPortNames())
            if (port.Contains("COM"))
                _portName = port;
    }

    //Метод отв. за переход на пред. окно с навигацией
    private void btnToPrevios_Click(object sender, RoutedEventArgs e)
    {
        if (Role == "admin")
        {
            MiddleWindowAdministrator middleWindow = new(_dataManager, _userService);
            middleWindow.Show();
            Close();
            return;
        }

        MiddleWindow middle = new(_userService, _dataManager)
        {
            UserName = "user"
        };
        middle.Show();
        Close();
    }

    //Метод, который сканирует штрих-код
    private void btnScan_Click(object sender, RoutedEventArgs e)
    {
        if (!isBarcodeScannerConnected())
        {
            MessageBox.Show("Сканер не подключен. Подключите сканер и попробуйте попытку снова");
            return;
        }

        SerialPort serialPort = new SerialPort(_portName, 9600, Parity.None, 8, StopBits.One); // задаем настройки порта, к которому подключен сканер штрих-кода
        serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived); // подписываемся на событие получения данных из порта

        serialPort.Open(); // открываем порт
    }

    private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        SerialPort serialPort = (SerialPort)sender; // получаем объект SerialPort для чтения данных
        string barcode = serialPort.ReadLine(); // читаем данные, полученные со сканера штрих-кода

        txtBarcode.Text = barcode;
    }

    private bool isBarcodeScannerConnected()
    {
        var ports = SerialPort.GetPortNames();

        foreach (var port in ports)
        {
            if (!port.Contains("COM"))
                return false;
        }

        using var sp = new SerialPort(_portName, 9600, Parity.None, 8, StopBits.One);

        try
        {
            // Попытка открытия порта
            sp.Open();
            if (sp.IsOpen)
            {
                // Проверяем, отправка ли команды приведет к получению ответа от сканера
                sp.Write("*IDN?\r\n");
                System.Threading.Thread.Sleep(1000); // Ждем некоторое время для получения ответа
                string response = sp.ReadExisting(); // Получаем ответ от устройства
                if (response.Contains("barcode scanner"))
                    return true; // Удачное подключение сканера
            }
        }
        catch
        {
            // Ошибка при открытии порта
            return false;
        }
        finally
        {
            sp.Close(); // Закрываем порт
        }

        return false;
    }

    //Метод, который отвечает за добавление новой записи в БД и в коллекцию
    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        if(string.IsNullOrEmpty(txtAssetId.Text))
        {
            MessageBox.Show("Заполните поле \"Идентификатор имущества\"");
            return;
        }

        Inventory inventory = new()
        {
            InventoryNumber = txtBarcode.Text,
            Name = txtName.Text,
            QuantityOnStock = _quantityOnStock,
            ActualQuantity = _actualQuantity,
            AssetId = Guid.Parse(txtAssetId.Text)
        };

        //Сохраняем в БД, добавляем в коллекцию
        _dataManager.Inventories.SaveInventory(inventory);
        _inventories.Add(inventory);

        //Очищаем введённый текст из полей
        foreach (var control in MainGrid.Children)
        {
            if (control is TextBox box)
            {
                box.Clear();
            }
        }
    }

    //Метод, который отвечает за переход на окно редактирования
    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
        //Чтобы были не пустые поля в новом окне - собираем текущие данные записи, и передаём их окну
        EditInventoryWindow editInventory = new(_dataManager, Guid.Parse((string) dgInventory.Columns[0].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty)), Guid.Parse((string)dgInventory.Columns[5].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty)), _userService);

        editInventory.txtInventoryNumber.Text = (string) dgInventory.Columns[1].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty);

        editInventory.txtName.Text = (string) dgInventory.Columns[2].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty);

        editInventory.txtQuantityOnStock.Text = (string) dgInventory.Columns[3].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty);

        editInventory.txtActualQuantity.Text = (string) dgInventory.Columns[4].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty);

        editInventory.Show();
        Close();
    }

    //Метод, который отвечает за удаление записи
    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        //Получаем id записи
        Guid identifier = Guid.Parse((string) dgInventory.Columns[0].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty));

        //Удаляем запись из БД и из списка
        _dataManager.Inventories.DeleteInventory(identifier);
        _inventories.Remove(_inventories.FirstOrDefault(x => x.InventoryId == identifier));
    }
}
