using App1.Models;
using App1.Validation;
using App1.Validation.Validators;

using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;
using static Xamarin.Forms.Internals.GIFBitmap;
using App1.Converters;
using Xamarin.Forms.PlatformConfiguration;
using System.Text.Json;
using System.Diagnostics;
using static Xamarin.Essentials.AppleSignInAuthenticator;
using System.Text;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using App1.Services;
using System.Security.Cryptography.X509Certificates;

namespace App1.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
       
        public string[] RooomTypes { get; set; }
        public ValidatableObject<string> Flat { get; set; }

        public ValidatableObject<string> Floor { get; set; }

        [Reactive]
        public int RoomTypeSelectedIndex { get; set; }

        [Reactive]
        public ValidationManager ValidationManager { get; set; }

        public IReactiveCommand VideoRecordCommand { get; set; }
        public IReactiveCommand OpenFileCommand { get; set; }

        [Reactive]
        public Location GeoLoaction { get; set; }   

        [Reactive]
        public string ResultData { get; set; }


        public async Task GetLocation()
        {

            GeoLoaction = await Services.GeoLocationService.GetGeoLocation();
        }
        public MainViewModel()
        {

            
            RooomTypes = RoomTypeHelper.GetRoomTypesNameFromConverter();
            Title = "About";
            GetLocation();
           

         

            Flat = new ValidatableObject<string>(new NullValidator());
            Floor = new ValidatableObject<string>(new NullValidator());

            RoomTypeSelectedIndex = 0;

            ValidationManager = new ValidationManager();
            ValidationManager.Add(Flat,Floor);

            OpenFileCommand = ReactiveCommand.Create(async () =>
            {
                try
                {
                    var result = await FilePicker.PickAsync(new PickOptions()
                    {
                        PickerTitle = "Выберите видео",
                        FileTypes = FilePickerFileType.Videos,
                    });
                    var stream = File.OpenRead(result.FullPath);

                    VideoModel videoModel = CreateVideoModel();

                    var filePath = CreatePathFormModel(videoModel);

                    ResultData = "Видео выбрано, подготовка данных";
                    IsBusy = true;
                    await CamService.SaveStreamToJsonAsync(filePath, videoModel, stream);

                    ResultData = "Данные успешно сохранены в Файл: " + filePath + "\n\n GeoLoaction:" + GeoLoaction.Latitude + "  " + GeoLoaction.Longitude;
                   
                }
                catch (Exception ex)
                {

                }
                finally { IsBusy = false; }
            });

            VideoRecordCommand = ReactiveCommand.Create(async () =>
            {
                try
                {
                    var video = await MediaPicker.CaptureVideoAsync();

                    VideoModel videoModel = CreateVideoModel();
                    var filePath = CreatePathFormModel(videoModel);
                    using (var stream = await video.OpenReadAsync())
                    {
                        IsBusy = true;
                        ResultData = "Видео записано, подождите загрузки";
                        await CamService.SaveStreamToJsonAsync(filePath, videoModel, stream);
                      

                    }
                    ResultData = "Данные успешно сохранены в Файл: " + filePath + "\n\n GeoLoaction:" + GeoLoaction.Latitude + "  " + GeoLoaction.Longitude;
                   
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally { IsBusy = false; }
                
            });

            
            
        }
        public VideoModel CreateVideoModel()
        {
            VideoModel videoModel = new VideoModel();
            videoModel.flat = Flat.Value;
            videoModel.floor = Floor.Value;
            videoModel.video_suf = ".mp4";
            videoModel.room_type = RooomTypes[RoomTypeSelectedIndex];
            videoModel.geopos = $"[{GeoLoaction.Latitude},{GeoLoaction.Longitude}]";
            return videoModel;
        }
        public string CreatePathFormModel(VideoModel videoModel)
        {
            var directoryPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fileName = videoModel.flat + "_" + videoModel.floor + ".json";
            var filePath = Path.Combine(directoryPath, fileName);
            return filePath;
        }
       
       

    }
}