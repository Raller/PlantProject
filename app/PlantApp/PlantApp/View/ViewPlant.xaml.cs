using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlantApp.Model;
using PlantApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PlantApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewPlant : ContentPage
    {
      

        ObservableCollection<Plant> _plants = new ObservableCollection<Plant>();
        public ObservableCollection<Plant> Plants { get { return _plants; } }
        PlantController ctr = new PlantController();

        public ViewPlant()
        {

            InitializeComponent();

            PlantList.ItemsSource = ctr.GenerateCustomerListAsync();

        }


        private void PlantList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tappedItem = e.SelectedItem;
            PlantGrid.IsVisible = true;
            PlantList.IsVisible = false;
            Console.WriteLine("Dette er imageUrl: " + ((Plant)tappedItem).imageUrl);
            if ((((Plant) tappedItem).imageUrl) != null)
            {
                
                PlantPicture.Source = (((Plant)tappedItem).imageUrl);
            } 


           
            plantName.Text = "Navn: " + (((Plant) tappedItem).name);
            plantId.Text = "ID: " +(((Plant)tappedItem).id);
            plantType.Text = "Type: " + (((Plant)tappedItem).type);
            Console.WriteLine("Jeg er blevet klikket");
            Console.WriteLine(((Plant)tappedItem).id);
            List<AirHumidity> airList = ctr.GetAirHumidity();
            
            AirHumidity airhums = airList.FindLast(item => item.plantId.Equals(((Plant)tappedItem).id));
            Console.WriteLine("Dette er airhums: " + airhums );
            if (airhums != null)
            {
                plantAirHum.Text = "Air Humidity: " + airhums.humidity;
            }

            List<Temperature> TempList = ctr.GetAirTemperature();
            Temperature airTemp = TempList.FindLast(item => item.plantId.Equals(((Plant)tappedItem).id));
            Console.WriteLine("Dette er air Temperature: " + airTemp);
            if (airTemp != null)
            {
                plantAirTemp.Text = "Air Temperature: " + airTemp.temp;
            }

            List<SoilHumidity> soilHumList = ctr.GetSoilHumidity();
            SoilHumidity soilHum = soilHumList.FindLast(item => item.plantId.Equals(((Plant)tappedItem).id));
            Console.WriteLine("Dette er air Temperature: " + soilHum);
            if (soilHum != null)
            {
                plantSoilHum.Text = "Soil Humidity: " + soilHum.humidity;
            }

        }
    }
}