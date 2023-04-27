using InventoryManagement.Domain;
using InventoryManagement.Services.Abstractions;
using System.Windows;

namespace InventoryManagement.Views;

public partial class MiddleWindow : Window
{
    // Поля, которые получают экземпляры зависимостей с помощью DI
    private readonly IUserService _userService;
    private readonly DataManager _dataManager;

    //Свойство для хранения роли
    public string UserName { get; set; }

    //Внедрение зависимостей
    public MiddleWindow(IUserService userService, DataManager dataManager)
    {
        InitializeComponent();

        //При загрузке формы выводим надпись
        Loaded += (sender, e) =>
        {
            lbWelcome.Content = $"Добро пожаловать, {UserName}";
        };

        _userService = userService;
        _dataManager = dataManager;

    }

    //Методы отвечающие за навигацию на другие окна (Инвентаризация, Имущество и т.д.)

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

    //Выход из приложения
    private void btnExit_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}
