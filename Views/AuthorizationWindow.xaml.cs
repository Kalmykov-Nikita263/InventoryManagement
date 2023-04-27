using InventoryManagement.Domain;
using InventoryManagement.Services.Abstractions;
using InventoryManagement.Views;
using System.Windows;

namespace InventoryManagement;

/// <summary>
/// Окно авторизации
/// </summary>
public partial class AuthorizationWindow : Window
{
    // Поля, которые получают экземпляры зависимостей с помощью DI

    private readonly IAuthorizationService _authorizationService;
    private readonly IUserService _userService;
    private readonly DataManager _dataManager;

    //Внедрение зависимостей в поля
    public AuthorizationWindow(IAuthorizationService authorizationService, DataManager dataManager, IUserService userService)
    {
        InitializeComponent();
        _authorizationService = authorizationService;
        _dataManager = dataManager;
        _userService = userService;
    }

    /// <summary>
    /// Метод, который авторизует пользователя и выполнит переход на следующее окно
    /// </summary>
    /// <param name="sender">Элемент-инициатор события</param>
    /// <param name="e">Аргументы события</param>
    private async void SignIn_btn_Click(object sender, RoutedEventArgs e)
    {
        if (await _authorizationService.LoginPasswordSignInAsync(Login_txt.Text, Password_txt.Password) && await _authorizationService.CheckRoleIsAdministrator(Login_txt.Text))
        {
            MiddleWindowAdministrator windowAdministrator = new(_dataManager, _userService);
            windowAdministrator.Show();
            Close();
            return;
        }

        else if (await _authorizationService.LoginPasswordSignInAsync(Login_txt.Text, Password_txt.Password))
        {
            MiddleWindow middleWindow = new(_userService, _dataManager)
            {
                UserName = await _userService.GetUserNameAsync(Login_txt.Text)
            };

            middleWindow.Show();
            Close();
            return;
        }

        ErrorMessage_lb.Text = "Неправильный логин или пароль";
    }
}