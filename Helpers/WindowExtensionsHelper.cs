using System.Windows;

namespace InventoryManagement.Helpers;

public static class WindowExtensionsHelper
{
    public static void Logout(this Window currentContext, Window navigateTo)
    {
        currentContext.Close();
        navigateTo.Show();
    }
}
