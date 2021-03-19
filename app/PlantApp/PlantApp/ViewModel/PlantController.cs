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
        private ObservableCollection<Plant> plants = new ObservableCollection<Plant>();
        public ObservableCollection<Plant> Plants { get { return plants; } set { plants = value; OnPropertyChanged(); } }

        public ICommand NewPlantCommand { get; private set; }
        INavigation navigation;

        public PlantController()
        {

        }
        public PlantController(INavigation nav)
        {
            navigation = nav;
            NewPlantCommand = new Command(NavigateToNewPlant);
            GetPlants();
        }

        private async void NavigateToNewPlant()
        {
            await navigation.PushAsync(new NewPlantView());
        }

        public async void GetPlants()
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://plantprojectapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("plant");
            Console.WriteLine("Besked fra server " + response.StatusCode);
            if (response.IsSuccessStatusCode)
            {
                var plantList = await response.Content.ReadAsAsync<IEnumerable<Plant>>();

                foreach (var item in plantList)
                {
                    Plants.Add(item);
                }
            }
            else
            {
                //Console.WriteLine("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        
    }
}
