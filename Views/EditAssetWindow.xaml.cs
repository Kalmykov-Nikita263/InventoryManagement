using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Types;
using InventoryManagement.Services.Abstractions;
using System;
using System.Windows;

namespace InventoryManagement.Views
{
    public partial class EditAssetWindow : Window
    {
        private readonly DataManager _dataManager;
        private readonly IUserService _userService;

        private readonly Guid _assetId;

        public EditAssetWindow(DataManager dataManager, Guid assetId, IUserService userService)
        {
            InitializeComponent();
            _dataManager = dataManager;
            _assetId = assetId;
            _userService = userService;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
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

            _dataManager.Assets.SaveAsset(asset);

            MessageBox.Show("Успешно");

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
}
