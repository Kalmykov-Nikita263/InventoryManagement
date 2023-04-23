using System.Windows;

namespace InventoryManagement.Views;

public partial class MiddleWindow : Window
{
    public MiddleWindow()
    {
        InitializeComponent();
        
        Loaded += (sender, e) =>
        {
            lbWelcome.Content = "Добро пожаловать, " + Application.Current.MainWindow.Tag;
        };
    }
}
