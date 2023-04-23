using System;
using System.Globalization;
using System.Windows.Data;

namespace InventoryManagement.Helpers;

public class AssetTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value != null && value.ToString() == "Immovable")
            return "Недвижимое";
        
        else if (value.ToString() == "Movable")
            return "Движимое";

        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}