using PlantApp.Model;
using PlantApp.Utils;
using PlantApp.View;
using RateABeer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PlantApp.ViewModel
{
    public class NewPlantViewModel : Bindable
    {

        public static Location position;
        string path = "";

        private Plant plant = new Plant();
        public Plant Plant { get { return plant; } set { plant = value; OnPropertyChanged(); } }

        private Image image = new Image();
        public Image Image { get { return image; } set { image = value; OnPropertyChanged(); } }

        public ICommand CancelCommand { get; private set; }
        public ICommand TakePhotoCommand { get; private set; }
        public ICommand OpenMapCommand { get; private set; }
        public ICommand SavePlantCommand { get; private set; }

        INavigation navigation;
        public NewPlantViewModel()
        {

        }
        public NewPlantViewModel(INavigation nav)
        {
            navigation = nav;
            CancelCommand = new Command(CancelPlant);
            TakePhotoCommand = new Command(TakePicture);
            OpenMapCommand = new Command(OpenMap);
            SavePlantCommand = new Command(SavePlant);

        }

        private async void CancelPlant()
        {
            await navigation.PopAsync();
        }

        private async void TakePicture()
        {
            //Image.Source = await Camera.TakePhotoAsync();
            path = await Camera.TakePhotoAsync();
            Console.WriteLine(path);
            Image.Source = path;
        }

        private async void OpenMap()
        {
            Console.WriteLine("navigated");
            await navigation.PushAsync(new MapView());
        }

        private async void SavePlant()
        {
            Plant.Latitude = position.Latitude.ToString();
            Plant.Longitude = position.Longitude.ToString();
            Console.WriteLine("Long " + position.Longitude + " lat " + position.Latitude);
            await Post("http://192.168.39.147:3000/plant");
        }

        /*private async void Post()
        {
            //var fileStream = new FileStream(path, FileMode.Open);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://192.168.39.147:3000/");
            MultipartFormDataContent form = new MultipartFormDataContent();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));


            form.Add(new StringContent(Plant.Name), "name");
            form.Add(new StringContent(Plant.Type), "type");
            form.Add(new StringContent(Plant.Latitude), "latitude");
            form.Add(new StringContent(Plant.Longitude), "longitude");

            form.Add(new ByteArrayContent(File.ReadAllBytes(path)), "image");

            Console.WriteLine("file added");

            HttpResponseMessage response = await httpClient.PostAsync("plant", form);

            Console.WriteLine(response.StatusCode);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            string sd = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(sd);
        }

        /*private byte[] ConvertImage()
        {
            public byte[] imageByte;
            Stream imageStream = Image.GetStream();
            BinaryReader br = new BinaryReader(imageStream);
            imageByte = br.ReadBytes((int)imageStream.Length);
        }*/

        private async Task<System.IO.Stream> Post(string actionUrl)
        {
            HttpContent bytesContent = new ByteArrayContent(File.ReadAllBytes(path));
            using (var client = new HttpClient())
            using (var form = new MultipartFormDataContent())
            {
                form.Add(new StringContent(Plant.Name), "name");
                form.Add(new StringContent(Plant.Type), "type");
                form.Add(new StringContent(Plant.Latitude), "latitude");
                form.Add(new StringContent(Plant.Longitude), "longitude");
                form.Add(bytesContent, "image", "image");
                var response = await client.PostAsync(actionUrl, form);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                return await response.Content.ReadAsStreamAsync();
            }
        }
    }
}
