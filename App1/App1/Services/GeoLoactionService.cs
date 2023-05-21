using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration;

namespace App1.Services
{
    public static class GeoLocationService
    {
        public static async Task<Location> GetGeoLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            return  await Geolocation.GetLocationAsync(request);
            
        }
    }
}
