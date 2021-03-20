using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using PlantApp.Model;
using PlantApp.View;
using Xamarin.Forms;

namespace PlantApp.ViewModel
{
    public class PlantController : Bindable
    {
        private IEnumerable<Plant> plants = new ObservableCollection<Plant>();
        public IEnumerable<Plant> Plants { get { return plants; } set { plants = value; OnPropertyChanged(); } }

        public ICommand NewPlantCommand { get; private set; }
        INavigation navigation;

        public PlantController()
        {

        }
        public PlantController(INavigation nav)
        {
            navigation = nav;
            NewPlantCommand = new Command(NavigateToNewPlantAsync);
            Task.Run(async () => await GetPlants());
        }

        private async void NavigateToNewPlantAsync()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewPlantView());
        }

        public async Task GetPlants()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("plant");
            Console.WriteLine("Besked fra server " + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                Plants = await response.Content.ReadAsAsync<IEnumerable<Plant>>();
            }
            else
            {
                //Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        
    }
}
