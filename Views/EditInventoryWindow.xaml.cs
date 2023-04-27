using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Services.Abstractions;
using System;
using System.Windows;

namespace InventoryManagement.Views;

/// <summary>
/// Окно для изменения информации о инвентаризации
/// </summary>
public partial class EditInventoryWindow : Window
{
    //Зависимости
    private readonly DataManager _dataManager;

    private readonly Guid _inventoryId;

    private readonly Guid _assetId;

    private readonly IUserService _userService;

    //Внедрение зависимостей
    public EditInventoryWindow(DataManager dataManager, Guid inventoryId, Guid assetId, IUserService userService)
    {
        InitializeComponent();
        _dataManager = dataManager;
        _inventoryId = inventoryId;
        _assetId = assetId;
        _userService = userService;
    }

    //Метод, который сохраняет инвентаризацию в БД
    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        //Создаём инвентаризацию. В поля записываем данные из формочки
        Inventory inventory = new()
        {
            InventoryId = _inventoryId,
            InventoryNumber = txtInventoryNumber.Text,
            Name = txtName.Text,
            QuantityOnStock = int.Parse(txtQuantityOnStock.Text),
            ActualQuantity = int.Parse(txtActualQuantity.Text),
            AssetId = _assetId
        };

        //Сохраняем в БД
        _dataManager.Inventories.SaveInventory(inventory);
        MessageBox.Show("Успешно");

        //Возвращаемся на пред. окно
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
