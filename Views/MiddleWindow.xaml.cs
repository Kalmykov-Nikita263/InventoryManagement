using InventoryManagement.Domain;
using InventoryManagement.Services.Abstractions;
using System.Windows;

namespace InventoryManagement.Views;

public partial class MiddleWindow : Window
{
    private readonly IUserService _userService;
    private readonly DataManager _dataManager;

    public string UserName { get; set; }

    public MiddleWindow(IUserService userService, DataManager dataManager)
    {
        InitializeComponent();

        Loaded += (sender, e) =>
        {
            lbWelcome.Content = $"Добро пожаловать, {UserName}";
        };

        _userService = userService;
        _dataManager = dataManager;

    }

    private void btnInventories_Click(object sender, RoutedEventArgs e)
    {
        InventoryWindow inventoryWindow = new(_dataManager, _userService)
        {
            Role = "user"
        };
        inventoryWindow.Show();
        Close();
    }

    private void btnAssets_Click(object sender, RoutedEventArgs e)
    {
        AssetWindow assetWindow = new(_dataManager, _userService)
        {
            Role = "user"
        };

        assetWindow.Show();
        Close(); 
    }

    private void btnExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
