using InventoryManagement.Domain;
using InventoryManagement.Services.Abstractions;
using InventoryManagement.Views;
using System.Windows;

namespace InventoryManagement;

public partial class AuthorizationWindow : Window
{
    private readonly IAuthorizationService _authorizationService;
    private readonly DataManager _dataManager;

    public AuthorizationWindow(IAuthorizationService authorizationService, DataManager dataManager)
    {
        InitializeComponent();
        _authorizationService = authorizationService;
        _dataManager = dataManager;
    }

    private async void SignIn_btn_Click(object sender, RoutedEventArgs e)
    {
        if (await _authorizationService.LoginPasswordSignInAsync(Login_txt.Text, Password_txt.Password) && await _authorizationService.CheckRoleIsAdministrator(Login_txt.Text))
        {
            MiddleWindowAdministrator windowAdministrator = new(_dataManager);
            windowAdministrator.Show();
            Close();
            return;
        }

        else if (await _authorizationService.LoginPasswordSignInAsync(Login_txt.Text, Password_txt.Password))
        {
            MiddleWindow middleWindow = new()
            {
                Tag = await _authorizationService.GetUserNameAsync(Login_txt.Text)
            };

            middleWindow.Show();
            Close();
            return;
        }

        ErrorMessage_lb.Text = "Неправильный логин или пароль";
    }
}