using System.Globalization;

namespace TP_ConnectFour.Converters
{
    public class TokenColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is char color)
            {
                return color switch
                {
                    'Y' => Colors.Yellow,
                    'R' => Colors.Red,
                    _ => Colors.LightGray // Empty token
                };
            }

            return Colors.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                if (color.Equals(Colors.Yellow))
                {
                    return 'Y';
                }
                else if (color.Equals(Colors.Red))
                {
                    return 'R';
                }
                else
                {
                    return ' ';
                }
            }
            return ' ';
        }
    }
}
