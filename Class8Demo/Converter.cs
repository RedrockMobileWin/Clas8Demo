using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Class8Demo
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value + "个";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
