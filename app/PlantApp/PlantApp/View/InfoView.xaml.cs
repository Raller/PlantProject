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
    public partial class InfoView : ContentPage
    {
       

        ObservableCollection<Plant> _plants = new ObservableCollection<Plant>();
        public ObservableCollection<Plant> Plants { get { return _plants; } }
        PlantController ctr = new PlantController();
        private SelectedItemChangedEventArgs e; 

        public InfoView(SelectedItemChangedEventArgs e)
        {
            this.e = e;

            InitializeComponent();
            BindingContext = new PlantController(Navigation);
            PlantList_OnItemSelected();


        }


        private void PlantList_OnItemSelected()
        {
            var tappedItem = e.SelectedItem;
            PlantGrid.IsVisible = true;
            Console.WriteLine("Dette er imageUrl: " + ((Plant)tappedItem).ImageUrl);
            if ((((Plant)tappedItem).ImageUrl) != null)
            {

                PlantPicture.Source = (((Plant)tappedItem).ImageUrl);
            }



            plantName.Text = "Navn: " + (((Plant)tappedItem).Name);
            plantId.Text = "ID: " + (((Plant)tappedItem).Id);
            plantType.Text = "Type: " + (((Plant)tappedItem).Type);
            Console.WriteLine("Jeg er blevet klikket");
            Console.WriteLine(((Plant)tappedItem).Id);
            List<AirHumidity> airList = ctr.GetAirHumidity();

            AirHumidity airhums = airList.FindLast(item => item.PlantId.Equals(((Plant)tappedItem).Id));
            Console.WriteLine("Dette er airhums: " + airhums);
            if (airhums != null)
            {
                plantAirHum.Text = "Air Humidity: " + airhums.Humidity;
            }

            List<Temperature> TempList = ctr.GetAirTemperature();
            Temperature airTemp = TempList.FindLast(item => item.PlantId.Equals(((Plant)tappedItem).Id));
            Console.WriteLine("Dette er air Temperature: " + airTemp);
            if (airTemp != null)
            {
                plantAirTemp.Text = "Air Temperature: " + airTemp.Temp;
            }

            List<SoilHumidity> soilHumList = ctr.GetSoilHumidity();
            SoilHumidity soilHum = soilHumList.FindLast(item => item.PlantId.Equals(((Plant)tappedItem).Id));
            Console.WriteLine("Dette er air Temperature: " + soilHum);
            if (soilHum != null)
            {
                plantSoilHum.Text = "Soil Humidity: " + soilHum.Humidity;
            }

        }
    }
}