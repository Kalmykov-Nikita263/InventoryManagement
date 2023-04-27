using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Types;
using InventoryManagement.Services.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryManagement.Views;

/// <summary>
/// Окно, которое содержит информацию об имуществе
/// </summary>
public partial class AssetWindow : Window
{
    //Коллекция для интерфейса
    private readonly ObservableCollection<Asset> _assets;

    //Зависимости
    private readonly DataManager _dataManager;
    private readonly IUserService _userService;

    private static int _quantity;

    private static double _price;

    public string Role { get; set; }

    //Внедрение зависимостей
    public AssetWindow(DataManager dataManager,IUserService userService)
    {
        InitializeComponent();
        
        _dataManager = dataManager;
        _userService = userService;

        //Добавляем в коллекцию данные из БД
        _assets = new(_dataManager.Assets.GetAllAssets().OrderBy(x => x.Name));

        //Выводим всё, что есть в коллекции на интерфейс
        dgAssets.ItemsSource = _assets;

        //Валидация полей, чтобы писали корректные данные
        txtQuantity.LostFocus += (sender, e) =>
        {
            if (int.TryParse(txtQuantity.Text, out int quantity))
                _quantity = quantity;

            else
            {
                txtQuantity.Background = Brushes.Red;
                MessageBox.Show("Введите число в поле \"Количество\"");
            }
        };

        txtPrice.LostFocus += (sender, e) =>
        {
            if (int.TryParse(txtPrice.Text, out int price))
                _price = price;

            else
            {
                txtPrice.Background = Brushes.Red;
                MessageBox.Show("Введите число в поле \"Цена\"");
            }
        };
    }

    //Метод, который сканирует штрих-код
    private void btnScan_Click(object sender, RoutedEventArgs e)
    {

    }

    //Метод, который отвечает за добавление новой записи в БД и в коллекцию
    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        Asset asset = new()
        {
            InventoryNumber = txtBarcode.Text,
            Name = txtName.Text,
            AssetType = (AssetType) Enum.Parse(typeof(AssetType), cmbType.SelectedIndex.ToString()),
            Location = txtLocation.Text,
            Quantity = _quantity,
            Price = _price
        };

        _dataManager.Assets.SaveAsset(asset);
        _assets.Add(asset);

        //Очищаем введённый текст из полей
        foreach (var control in MainGrid.Children)
        {
            if (control is TextBox box)
            {
                box.Clear();
            }
        }
    }

    private void btnToPrevios_Click(object sender, RoutedEventArgs e)
    {
        if (Role == "admin")
        {
            MiddleWindowAdministrator windowAdministrator = new(_dataManager, _userService);
            windowAdministrator.Show();
            Close();
            return;
        }

        MiddleWindow middleWindow = new(_userService, _dataManager)
        {
            UserName = "user"
        };
        middleWindow.Show();
        Close();
    }

    //Метод, который отвечает за переход на окно редактирования
    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
        //Чтобы были не пустые поля в новом окне - собираем текущие данные записи, и передаём их окну
        EditAssetWindow editAsset = new(_dataManager, Guid.Parse((string) dgAssets.Columns[0].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty)), _userService);

        editAsset.txtInventoryNumber.Text = (string) dgAssets.Columns[1].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.txtName.Text = (string) dgAssets.Columns[2].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.txtLocation.Text = (string) dgAssets.Columns[4].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.txtQuantity.Text = (string) dgAssets.Columns[5].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.txtPrice.Text = (string) dgAssets.Columns[6].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.Show();
        Close();
    }

    //Метод, который отвечает за удаление записи
    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        //Получаем id записи
        Guid identifier = Guid.Parse((string) dgAssets.Columns[0].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty));

        //Удаляем запись из БД и из списка
        _dataManager.Assets.DeleteAsset(identifier);
        _assets.Remove(_assets.FirstOrDefault(x => x.AssetId == identifier));
    }
}
