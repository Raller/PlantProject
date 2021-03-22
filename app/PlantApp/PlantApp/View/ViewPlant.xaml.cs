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
        PlantController plantCtr;
        public ViewPlant()
        {
            InitializeComponent();
            plantCtr = new PlantController(Navigation);
            BindingContext = plantCtr;
        }


        private async void PlantList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            Plant selectedPlant = e.SelectedItem as Plant;
            await Navigation.PushAsync(new PlantInfoTabbedPage(selectedPlant));

            ((ListView)sender).SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await plantCtr.GetPlants();
        }
    }
}