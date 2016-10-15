using MobileApp.ViewModels;
using MobileApp.Views;
using Prism;
using Prism.Modularity;
using Prism.Unity;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MobileApp
{
    public partial class App : PrismApplication
    {
        private Dictionary<string, ShoppingItemViewModel> data = new Dictionary<string, ShoppingItemViewModel>();

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        private async Task GetDataFromServer()
        {
            using (var client = new RestClient(new Uri("https://ytv3odwce7.execute-api.us-west-2.amazonaws.com/prod/")))
            {
                var request = new RestRequest("ProductCatalog?TableName=ProductCatalog", Method.GET);
                var result = await client.Execute<dynamic>(request);

            }
        }

        protected override async void OnInitialized()
        {
            // get data from server


            // navigate 
            await NavigationService.NavigateAsync($"{nameof(ShellPage)}/{nameof(ShellNavigationPage)}/{nameof(ShoppingListPage)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<ShellPage>();
            Container.RegisterTypeForNavigation<ShellNavigationPage>();
            Container.RegisterTypeForNavigation<WelcomePage>();
            Container.RegisterTypeForNavigation<ShoppingListPage>();
            Container.RegisterTypeForNavigation<AddShoppingItemPage>();
        }

        public void SendMessageForScan(string productId)
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
