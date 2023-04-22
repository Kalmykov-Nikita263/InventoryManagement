using InventoryManagement.Services.Abstractions;
using System.Windows;

namespace InventoryManagement;

public partial class AuthorizationWindow : Window
{
   private readonly IAuthorizationService _authorizationService;

    public AuthorizationWindow(IAuthorizationService authorizationService)
    {
        InitializeComponent();
        _authorizationService = authorizationService;
    }

    private async void SignIn_btn_Click(object sender, RoutedEventArgs e)
    {
        if (await _authorizationService.LoginPasswordSignInAsync(Login_txt.Text, Password_txt.Password))
        {

            return;
        }

        ErrorMessage_lb.Text = "Неправильный логин или пароль";
    }
}