using MobileApp.ViewModels;
using Xamarin.Forms;

namespace MobileApp.Views
{
    public partial class ShoppingListPage : ContentPage
    {
        public ShoppingListPage()
        {
            InitializeComponent();
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
