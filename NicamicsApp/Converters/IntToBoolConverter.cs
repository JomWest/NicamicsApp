using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicamicsApp.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue && parameter is string stringValue && int.TryParse(stringValue, out var paramValue))
            {
                return intValue == paramValue;  // Devuelve true si el valor coincide
            }
            return false;  // Devuelve false si no coincide
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? int.Parse((string)parameter) : 0; // Convierte el valor a entero al hacer el enlace hacia atrás
        }
    }
}
