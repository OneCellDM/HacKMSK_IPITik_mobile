using DynamicData;

using HakatonApp.Models;

using RestSharp;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.Xml.Linq;

using static Android.Renderscripts.ScriptGroup;

namespace HakatonApp.Services
{
    public class NetworkService
    {

        const string VideoLoadUri = "http://192.168.1.41:5050/api/houses/update";
        const string HouseList = "http://192.168.1.41:5050/api/houses/list";

        private static HttpClient httpClient = new HttpClient()
        {
            Timeout = TimeSpan.FromHours(80)
        };

        public static async Task<string[]> GetHauseList()
        {
            var res = await httpClient.GetStringAsync(HouseList);
            return JsonSerializer.Deserialize<string[]>(res);
        }
        public static async Task<List<string>> UploadVideo(VideoModel videoModel, Stream stream)
        {
            var json = JsonSerializer.Serialize(videoModel);

            StreamContent streamContent = new StreamContent(stream);

            StringContent stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent()
            {
                {streamContent,"video","videoName.mp3"},
                {stringContent,"info" }
            };

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, VideoLoadUri);

            httpRequestMessage.Content = multipartFormDataContent;

            var response = await httpClient.SendAsync(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                var data = ReadDataResponse(await response.Content.ReadAsStringAsync());
                return data;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public static List<string> ReadDataResponse(string data)
        {
            return data.Replace("[", "").Replace("]", "").Replace("\"", "").Split(",")
                    .Select(x => x.Split(" ").FirstOrDefault().Trim().Replace("picture", "window")).Distinct().ToList();
        }

    }
}
