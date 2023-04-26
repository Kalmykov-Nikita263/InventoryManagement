using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Services.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryManagement.Views;

public partial class InventoryWindow : Window
{
    private ObservableCollection<Inventory> _inventories;

    private readonly DataManager _dataManager;
    private readonly IUserService _userService;

    private static int _quantityOnStock;

    private static int _actualQuantity;

    public string Role { get; set; }

    public InventoryWindow(DataManager dataManager, IUserService userService)
    {
        InitializeComponent();

        _dataManager = dataManager;
        _userService= userService;

        _inventories = new(_dataManager.Inventories.GetAllInventories().OrderBy(x => x.Name));

        dgInventory.ItemsSource = _inventories;

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
        _userService = userService;
    }

    private void btnToPrevios_Click(object sender, RoutedEventArgs e)
    {
        if(Role == "admin")
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

    private void btnScan_Click(object sender, RoutedEventArgs e)
    {

    }

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

        _dataManager.Inventories.SaveInventory(inventory);
        _inventories.Add(inventory);

        foreach (var control in MainGrid.Children)
        {
            if (control is TextBox box)
            {
                box.Clear();
            }
        }
    }

    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
        EditInventoryWindow editInventory = new(_dataManager, Guid.Parse((string) dgInventory.Columns[0].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty)), Guid.Parse((string)dgInventory.Columns[5].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty)), _userService);

        editInventory.txtInventoryNumber.Text = (string) dgInventory.Columns[1].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty);

        editInventory.txtName.Text = (string) dgInventory.Columns[2].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty);

        editInventory.txtQuantityOnStock.Text = (string) dgInventory.Columns[3].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty);

        editInventory.txtActualQuantity.Text = (string) dgInventory.Columns[4].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty);

        editInventory.Show();
        Close();
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        Guid identifier = Guid.Parse((string) dgInventory.Columns[0].GetCellContent(dgInventory.SelectedItem).GetValue(TextBlock.TextProperty));

        _dataManager.Inventories.DeleteInventory(identifier);
        _inventories.Remove(_inventories.FirstOrDefault(x => x.InventoryId == identifier));
    }
}
