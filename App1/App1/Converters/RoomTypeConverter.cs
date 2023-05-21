using App1.Models;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Xamarin.Forms;

namespace App1.Converters
{
    public class RoomTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if(value == null) return null;
            if (value is RoomTypeEnum)
            {
                return (RoomTypeEnum)value switch
                {
                    RoomTypeEnum.Room => "Жилая комната",
                    RoomTypeEnum.Bathroom => "Ванная комната",
                    RoomTypeEnum.Kitchen => "Кухня",
                    RoomTypeEnum.Corridor => "Коридор квартиры",
                    RoomTypeEnum.Hallway => "Коридор дома/Холл",
                    RoomTypeEnum.Other => "Другое помещение",
                    _ => RoomTypeEnum.Other
                };
            }
            else
                return null;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
