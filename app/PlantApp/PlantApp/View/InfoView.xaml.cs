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
        private Plant plant;

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
            plant = tappedItem as Plant;
            Console.WriteLine("lat: " + plant.Latitude + " long " + plant.Longitude);
            PlantGrid.IsVisible = true;
            Console.WriteLine("Dette er imageUrl: " + ((Plant)tappedItem).ImageUrl);
            if ((((Plant)tappedItem).ImageUrl) != null)
            {

                PlantPicture.Source = (((Plant)tappedItem).ImageUrl);
            }



          

            List<SoilHumidity> soilHumList = ctr.GetSoilHumidity();
            SoilHumidity soilHum = soilHumList.FindLast(item => item.PlantId.Equals(((Plant)tappedItem).Id));
            Console.WriteLine("Dette er air Temperature: " + soilHum);
            if (soilHum != null)
            {
                plantSoilHum.Text = "Soil Humidity: " + soilHum.Humidity;
            }

        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlantLocationView(plant));
        }
    }
}