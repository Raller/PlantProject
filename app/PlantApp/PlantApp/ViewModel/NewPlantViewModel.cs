using PlantApp.Model;
using PlantApp.Utils;
using PlantApp.View;
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
        INavigation navigation;

        private Plant plant = new Plant();
        public Plant Plant { get { return plant; } set { plant = value; OnPropertyChanged(); } }

        private Image image = new Image();
        public Image Image { get { return image; } set { image = value; OnPropertyChanged(); } }

        public ICommand CancelCommand { get; private set; }
        public ICommand TakePhotoCommand { get; private set; }
        public ICommand OpenMapCommand { get; private set; }
        public ICommand SavePlantCommand { get; private set; }


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
            path = await Camera.TakePhotoAsync();
            Image.Source = path;
        }

        private async void OpenMap()
        {
            await navigation.PushAsync(new MapView());
        }

        private async void SavePlant()
        {
            
            try
            {
                Plant.Latitude = position.Latitude.ToString();
                Plant.Longitude = position.Longitude.ToString();
                await Post("https://plantprojectapi.azurewebsites.net/plant");
                await Application.Current.MainPage.DisplayAlert("Plante oprettet", "Planten er blevet oprettet", "Ok");
                await navigation.PopAsync();
            } catch(Exception e)
            {
                await Application.Current.MainPage.DisplayAlert("Fejl", "Oprettelse af plante fejlede.\nAlle felter skal udfyldes.", "Ok");
            }
        }

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
