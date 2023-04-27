using InventoryManagement.Domain;
using InventoryManagement.Services.Abstractions;
using System.Windows;

namespace InventoryManagement.Views
{
    /// <summary>
    /// Навигационное окно администратора
    /// </summary>
    public partial class MiddleWindowAdministrator : Window
    {
        private readonly DataManager _dataManager;
        private readonly IUserService _userService;

        //Внедрение зависимостей
        public MiddleWindowAdministrator(DataManager dataManager, IUserService userService)
        {
            InitializeComponent();
            _dataManager = dataManager;
            _userService = userService;
        }

        //Методы отвечающие за навигацию на другие окна (Инвентаризация, Имущество и т.д.)
        private void btnInventories_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow inventoryWindow = new(_dataManager, _userService);
            inventoryWindow.Show();
            Close();
        }

        private void btnAssets_Click(object sender, RoutedEventArgs e)
        {
            AssetWindow assetWindow = new(_dataManager, _userService);
            assetWindow.Show();
            Close();
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow window = new(_userService, _dataManager);
            window.Show();
            Close();
        }

        //Выход из приложения
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
