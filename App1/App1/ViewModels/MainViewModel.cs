using HakatonApp.Validation.Validators;

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

using Xamarin.Forms.PlatformConfiguration;
using System.Text.Json;
using System.Diagnostics;
using static Xamarin.Essentials.AppleSignInAuthenticator;
using System.Text;

using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Security.Cryptography.X509Certificates;
using static System.Net.WebRequestMethods;
using System.Diagnostics.Contracts;

using Xamarin.Forms.Xaml;
using Android.Graphics;
using System.Collections;
using System.Linq;
using System.Net.Http;
using Android.Content.Res;
using Plugin;
using System.Text.Json.Serialization;
using HakatonApp.Services;
using HakatonApp.Models;
using HakatonApp.Validation;

namespace HakatonApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public string[] RooomTypes { get; set; }
        public string[] RooomTypesEng { get; set; }
        public ValidatableObject<string> Flat { get; set; }

        public ValidatableObject<string> Floor { get; set; }

        [Reactive]
        public int RoomTypeSelectedIndex { get; set; }
        [Reactive]
        public int AddressSelectedIndex { get; set; }

        [Reactive]
        public ValidationManager ValidationManager { get; set; }

        public IReactiveCommand VideoRecordCommand { get; set; }
        public IReactiveCommand OpenFileCommand { get; set; }

        [Reactive]

        public string[] AddResses { get; set; }

        [Reactive]
        public List<string> DetectedResults { get; set; }

        [Reactive]
        public string ResultData { get; set; }
        [Reactive]
        public bool ResultIsVisible { get; set; }
        [Reactive]
        public string WaitMessage { get; set; }

        [Reactive]
        public string VideoPath { get; set; }


        public async Task LoadAddresses()
        {
            IsBusy = true;
            AddResses = await NetworkService.GetHauseList();
            WaitMessage = "Получение адресов зданий, подождите";
            IsBusy = false;
        }

        public MainViewModel()
        {

            LoadAddresses();
            RooomTypes = RoomTypeHelper.GetRoomTypesRuName();
            RooomTypesEng = RoomTypeHelper.GetRoomTypesNames();



            Title = "Хакатон";


            Flat = new ValidatableObject<string>(new NullValidator());
            Floor = new ValidatableObject<string>(new NullValidator());


            RoomTypeSelectedIndex = 0;

            ValidationManager = new ValidationManager();
            ValidationManager.Add(Flat, Floor);

            OpenFileCommand = ReactiveCommand.Create(async () =>
            {
                try
                {
                    ResultIsVisible = false;
                    var result = await FilePicker.PickAsync(new PickOptions()
                    {
                        PickerTitle = "Выберите видео",
                        FileTypes = FilePickerFileType.Videos,
                    });


                    await LoadVideo(result.FullPath);

                }
                catch (Exception ex)
                {
                    ResultData = "Произошла ошибка при выборе выидео";

                }
                finally
                {
                    IsBusy = false;
                    ResultIsVisible = true;
                }
            });

            VideoRecordCommand = ReactiveCommand.Create(async () =>
            {
                try
                {
                    ResultIsVisible = false;
                    var videoCapture = await Plugin.Media.CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions()
                    {
                        CompressionQuality = 80,
                        AllowCropping = true,
                        Quality = Plugin.Media.Abstractions.VideoQuality.Medium,
                    });
                    await LoadVideo(videoCapture.GetStream());
                }
                catch (Exception ex)
                {
                    ResultData = "Произошла ошибка при записи видео";
                }
                finally
                {
                    IsBusy = false;
                    ResultIsVisible = true;
                }

            });



        }
        public async Task LoadVideo(string filePath)
        {

            var stream = System.IO.File.OpenRead(filePath);
            await LoadVideo(stream);

        }
        public async Task LoadVideo(Stream stream)
        {
            try
            {

                DetectedResults?.Clear();

                WaitMessage = "Отправка и обработка видео, подождите";
                VideoModel videoModel = CreateVideoModel();

                IsBusy = true;
                DetectedResults = await NetworkService.UploadVideo(videoModel, stream);


                ResultData = "Видео успешно обработано";

            }
            catch (Exception ex)
            {

                ResultData = $"Видео не было обработано ошибка: {ex.Message}";

            }

        }


        public VideoModel CreateVideoModel()
        {
            VideoModel videoModel = new VideoModel();
            videoModel.flat = Flat.Value;
            videoModel.floor = Floor.Value;
            videoModel.room_type = RooomTypesEng[RoomTypeSelectedIndex];
            videoModel.address = AddResses[AddressSelectedIndex];
            return videoModel;
        }
        public string CreatePathFormModel(VideoModel videoModel)
        {
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var fileName = videoModel.flat + "_" + videoModel.floor + ".json";
            var filePath = System.IO.Path.Combine(directoryPath, fileName);
            return filePath;
        }





    }
}