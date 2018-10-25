using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Atlas.Efes.Manager.Converter
{
    [ValueConversion(typeof(Boolean), typeof(Visibility))]
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return Binding.DoNothing;

            if (parameter == null)
                return Binding.DoNothing;

            Boolean input = false;
            Boolean.TryParse(value.ToString(), out input);

            Boolean invertActive = false;
            Boolean.TryParse(parameter.ToString(), out invertActive);

            if (input)
            {
                return invertActive ? Visibility.Visible : Visibility.Hidden;
            }
            else
                return invertActive ? Visibility.Hidden : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
