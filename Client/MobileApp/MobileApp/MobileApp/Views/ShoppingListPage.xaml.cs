using MobileApp.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage()
        {
            InitializeComponent();

            MessagingCenter.Subscribe<App,ShoppingItemViewModel>(this, "ScanItem", (sender,vm) => {
                Device.BeginInvokeOnMainThread(async () => {
                    ScanPopup.IsVisible = true;
                    await Task.Delay(3000);
                    ScanPopup.IsVisible = false;

                    MessagingCenter.Send(this, "AddScanItem", vm);
                });
               
            });
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ShoppingListView.SelectedItem = null;
        }

        public Command GroupHeaderTapCommand
        {
            get
            {
                return new Command((object x) =>
                {
                    var groupViewModel = x as ShoppingItemGroupViewModel;
                    groupViewModel.Toggle();
                });
            }
        }

    }
}
