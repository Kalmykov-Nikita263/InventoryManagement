using InventoryManagement.Domain;
using InventoryManagement.Helpers;
using InventoryManagement.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace InventoryManagement.Views
{
    public partial class UsersWindow : Window
    {
        private readonly DataManager _dataManager;
        private readonly IUserService _userService;

        private ObservableCollection<IdentityUser> _users;

        public UsersWindow(IUserService userService, DataManager dataManager)
        {
            InitializeComponent();
            _userService = userService;
            _dataManager = dataManager;
            InitializeDataAsync().GetAwaiter().GetResult();
        }

        public async Task InitializeDataAsync()
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

        private void btnToPrevious_Click(object sender, RoutedEventArgs e)
        {
            MiddleWindowAdministrator middleWindow = new MiddleWindowAdministrator(_dataManager, _userService);
            middleWindow.Show();
            Close();
        }
    }
}