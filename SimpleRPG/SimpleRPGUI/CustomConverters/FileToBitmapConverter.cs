﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace SimpleRPGUI.CustomConverters
{
    public class FileToBitmapConverter : IValueConverter
    {
        private static readonly Dictionary<string, BitmapImage> _locations = new();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string filename)
            {
                return null;
            }

            if (!_locations.ContainsKey(filename))
            {
                _locations.Add(filename, new BitmapImage(new Uri($"{AppDomain.CurrentDomain.BaseDirectory}{filename}", UriKind.Absolute)));
            }

            return _locations[filename];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
