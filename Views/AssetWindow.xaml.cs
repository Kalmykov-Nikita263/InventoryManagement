using InventoryManagement.Domain;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Types;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InventoryManagement.Views;

public partial class AssetWindow : Window
{
    private ObservableCollection<Asset> _assets;

    private readonly DataManager _dataManager;

    private static int _quantity;

    private static double _price;

    public AssetWindow(DataManager dataManager)
    {
        InitializeComponent();
        
        _dataManager = dataManager;
        _assets = new(_dataManager.Assets.GetAllAssets().OrderBy(x => x.Name));

        dgAssets.ItemsSource = _assets;

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

    private void btnScan_Click(object sender, RoutedEventArgs e)
    {

    }

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
        MiddleWindowAdministrator windowAdministrator = new(_dataManager);
        windowAdministrator.Show();
        Close();
    }

    private void btnEdit_Click(object sender, RoutedEventArgs e)
    {
        EditAssetWindow editAsset = new(_dataManager, Guid.Parse((string) dgAssets.Columns[0].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty)));

        editAsset.txtInventoryNumber.Text = (string) dgAssets.Columns[1].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.txtName.Text = (string) dgAssets.Columns[2].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.txtLocation.Text = (string) dgAssets.Columns[4].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.txtQuantity.Text = (string) dgAssets.Columns[5].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.txtPrice.Text = (string) dgAssets.Columns[6].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty);

        editAsset.Show();
        Close();
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        Guid identifier = Guid.Parse((string) dgAssets.Columns[0].GetCellContent(dgAssets.SelectedItem).GetValue(TextBlock.TextProperty));

        _dataManager.Assets.DeleteAsset(identifier);
        _assets.Remove(_assets.FirstOrDefault(x => x.AssetId == identifier));
    }
}
