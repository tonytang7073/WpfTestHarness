using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace WpfTestHarness.Converters
{
    public class BooleanToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool inverse = (parameter as string) == "inverse";

            var bold = value as bool?;
            if (bold.HasValue && bold.Value)
            {
                return inverse ? FontWeights.Normal : FontWeights.Bold;
            }
            return inverse ? FontWeights.Bold : FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
