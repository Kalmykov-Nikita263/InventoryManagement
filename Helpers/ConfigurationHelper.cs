namespace InventoryManagement.Helpers;

/// <summary>
/// Класс конфигурации, который сопоставляется с веткой "ConnectionStrings" файла appsettings.json
/// </summary>
public class ConfigurationHelper
{
    //Свойство, которое содержит в себе строку для подключния
    public static string DefaultConnection { get; set; }
}
