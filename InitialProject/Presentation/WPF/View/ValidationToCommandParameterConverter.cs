using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace InitialProject.Presentation.WPF.View
{
    public class ValidationToCommandParameterConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool validationPassed = !(bool)values[0]; // Invert the validation result
                                                      // Additional values can be accessed from the values array as needed
                                                      // Combine the validation result with other values if necessary
            return validationPassed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
