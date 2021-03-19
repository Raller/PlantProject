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
            BindingContext = new PlantController(Navigation);
            PlantList.ItemsSource = ctr.GenerateCustomerListAsync();

        }


        private void PlantList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e.SelectedItem == null)
                return;

            Plant selectedPlant = e.SelectedItem as Plant;

            Navigation.PushAsync(new PlantInfoTabbedPage(selectedPlant));
            ((ListView)sender).SelectedItem = null;
        }
    }
}