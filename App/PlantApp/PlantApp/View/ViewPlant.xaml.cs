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
      

        ObservableCollection<Plant> employees = new ObservableCollection<Plant>();
        public ObservableCollection<Plant> Employees { get { return employees; } }


        public ViewPlant()
        {
            InitializeComponent();
            PlantController ctr = new PlantController();

            PlantList.ItemsSource = ctr.GenerateCustomerListAsync();

            // ObservableCollection allows items to be added after ItemsSource
            // is set and the UI will react to changes
            employees.Add(new Plant() { name = "Kaktus", imageUrl = "TEST", type = "kaktus"});
            employees.Add(new Plant() { name = "Egetræ" , type = "Skovtræ"});
         
        }


        private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        {
            Console.WriteLine("Jeg er blevet klikket på");
        }

        private void PlantList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Console.WriteLine("Jeg er blevet klikket");
            
        }

        private void PlantList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var tappedItem = e.SelectedItem;
            PlantGrid.IsVisible = true;
            PlantList.IsVisible = false;
            PlantPicture.Source = (((Plant)tappedItem).imageUrl);
            plantName.Text = "Navn: " + (((Plant) tappedItem).name);
            plantId.Text = "ID: " +(((Plant)tappedItem).id);
            plantType.Text = "Tyoe: " + (((Plant)tappedItem).type);
            Console.WriteLine("Jeg er blevet klikket");
            Console.WriteLine(((Plant)tappedItem).id);
        }
    }
}