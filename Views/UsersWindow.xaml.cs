using InventoryManagement.Domain;
using InventoryManagement.Helpers;
using InventoryManagement.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagement.Views;

/// <summary>
/// Окно, которое содержит данные о всех пользователях системы
/// </summary>
public partial class UsersWindow : Window
{
    //Зависимости
    private readonly DataManager _dataManager;
    private readonly IUserService _userService;

    //Коллекция для обновления интерфейса
    private ObservableCollection<IdentityUser> _users;

    //Внедрение зависимостей
    public UsersWindow(IUserService userService, DataManager dataManager)
    {
        InitializeComponent();
        _userService = userService;
        _dataManager = dataManager;

        //Инициализируем данные пользователей на интерфейсе
        InitializeDataAsync().GetAwaiter().GetResult();
    }

    private async Task InitializeDataAsync()
    {
        var users = await _userService.GetAllUsersAsync();

        _users = new(users.ToList());

        dgUsers.ItemsSource = _users;

        var newUser = new List<ApplicationUserHelper>();

        foreach (var user in users)
        {
            var roles = await _userService.GetAllRolesAsync(user.Id);
            var userHelper = new ApplicationUserHelper
            {
                UserName = user.UserName,
                Email = user.Email,
                Role = roles.FirstOrDefault(x => x == "user")
            };

            newUser.Add(userHelper);
        }
    }

    //Кнопка, отвечающая за переход на предыдущее окно
    private void btnToPrevious_Click(object sender, RoutedEventArgs e)
    {
        MiddleWindowAdministrator middleWindow = new MiddleWindowAdministrator(_dataManager, _userService);
        middleWindow.Show();
        Close();
    }
}