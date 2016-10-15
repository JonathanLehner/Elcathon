using MobileApp.ViewModels;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public partial class ScannedListPage : ContentPage
    {
        public ScannedListPage()
        {
            InitializeComponent();
            BindingContext = new ScannedListPageViewModel();
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ScannedList.SelectedItem = null;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            MessagingCenter.Send(this, "ScanItem", new ShoppingItemViewModel
            {
                Name = "Red bull",
                CategoryName = "Breverage",
                Image = "red_bull.png",
                Quantity = 1,
                Price = 1.5m
            });
        }
    }
}
