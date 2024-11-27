using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TestFurstWPF.Converters
{
    
    public class VisibilityToWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visibility)
            {
                if (visibility == Visibility.Collapsed || visibility == Visibility.Hidden)
                {
                    return new GridLength(1, GridUnitType.Star);
                }
                else if(visibility == Visibility.Visible)
                {
                    return new GridLength(0, GridUnitType.Star);
                }
                else
                {
                    return new GridLength(2, GridUnitType.Star);
                }
            }
            return new GridLength(0, GridUnitType.Star); // Значение по умолчанию
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
