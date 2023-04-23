using InventoryManagement.Domain;
using System.Windows;

namespace InventoryManagement.Views
{
    public partial class MiddleWindowAdministrator : Window
    {
        private readonly DataManager _dataManager;

        public MiddleWindowAdministrator(DataManager dataManager)
        {
            InitializeComponent();
            _dataManager = dataManager;
        }

        private void btnInventories_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow inventoryWindow = new(_dataManager);
            inventoryWindow.Show();
            Close();
        }

        private void btnAssets_Click(object sender, RoutedEventArgs e)
        {
            AssetWindow assetWindow = new(_dataManager);
            assetWindow.Show();
            Close();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
