using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Services.Abstractions;
using System;
using System.Windows;

namespace InventoryManagement.Views;

public partial class EditInventoryWindow : Window
{
    private readonly DataManager _dataManager;

    private readonly Guid _inventoryId;

    private readonly Guid _assetId;

    private readonly IUserService _userService;

    public EditInventoryWindow(DataManager dataManager, Guid inventoryId, Guid assetId, IUserService userService)
    {
        InitializeComponent();
        _dataManager = dataManager;
        _inventoryId = inventoryId;
        _assetId = assetId;
        _userService = userService;
    }

    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        Inventory inventory = new()
        {
            InventoryId = _inventoryId,
            InventoryNumber = txtInventoryNumber.Text,
            Name = txtName.Text,
            QuantityOnStock = int.Parse(txtQuantityOnStock.Text),
            ActualQuantity = int.Parse(txtActualQuantity.Text),
            AssetId = _assetId
        };

        _dataManager.Inventories.SaveInventory(inventory);
        MessageBox.Show("Успешно");

        NavigateToInventoryWindow();
    }

    private void NavigateToInventoryWindow()
    {
        InventoryWindow window = new(_dataManager, _userService);
        window.Show();
        Close();
    }

    private void btnPrevious_Click(object sender, RoutedEventArgs e)
    {
        NavigateToInventoryWindow();
    }

    private void btnScan_Click(object sender, RoutedEventArgs e)
    {

    }
}
