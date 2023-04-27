using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Types;
using InventoryManagement.Services.Abstractions;
using System;
using System.Windows;

namespace InventoryManagement.Views;

/// <summary>
/// Окно для изменения информации об имуществе
/// </summary>
public partial class EditAssetWindow : Window
{
    //Зависимости
    private readonly DataManager _dataManager;
    private readonly IUserService _userService;

    private readonly Guid _assetId;

    //Внедрение зависимостей
    public EditAssetWindow(DataManager dataManager, Guid assetId, IUserService userService)
    {
        InitializeComponent();
        _dataManager = dataManager;
        _assetId = assetId;
        _userService = userService;
    }

    //Метод, который отвечает за сохранение изменений о имуществе
    private void btnSave_Click(object sender, RoutedEventArgs e)
    {
        //Создаём новое имущество. В поля записываем данные из формочки
        Asset asset = new()
        {
            AssetId = _assetId,
            InventoryNumber = txtInventoryNumber.Text,
            Name = txtName.Text,
            AssetType = (AssetType) Enum.Parse(typeof(AssetType), cmbType.SelectedIndex.ToString()),
            Location = txtLocation.Text,
            Quantity = int.Parse(txtQuantity.Text),
            Price = double.Parse(txtPrice.Text),
        };

        //Сохраняем
        _dataManager.Assets.SaveAsset(asset);

        MessageBox.Show("Успешно");

        //Переходим на предыдущее окно
        NavigateToAssetWindow();
    }

    private void btnPrevious_Click(object sender, RoutedEventArgs e)
    {
        NavigateToAssetWindow();
    }

    private void NavigateToAssetWindow()
    {
        AssetWindow assetWindow = new(_dataManager, _userService);
        assetWindow.Show();
        Close();
    }
}
