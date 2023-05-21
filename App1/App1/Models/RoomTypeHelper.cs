using App1.Converters;

using System;
using System.Linq;

namespace App1.Models
{
    public static class RoomTypeHelper
    {
       
        public static string[] GetRoomTypesNameFromConverter()
        {
           RoomTypeConverter roomTypeConverter = new RoomTypeConverter();  
           var rooms = GetRoomTypes();
           return rooms.Select(x=> (string)roomTypeConverter.Convert(x,null,null,null)).ToArray();
        }
        public static RoomTypeEnum[] GetRoomTypes()
        {
            var values =  Enum.GetValues(typeof(RoomTypeEnum));
            
            var results = new RoomTypeEnum[values.Length];

            for(int i=0; i < values.Length; i++)
            {
                results[i] = (RoomTypeEnum)values.GetValue(i);
            }
            return results;
        }
    }
}
