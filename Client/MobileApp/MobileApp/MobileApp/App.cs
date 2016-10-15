using MobileApp.ViewModels;
using MobileApp.Views;
using Prism;
using Prism.Modularity;
using Prism.Unity;
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
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            await NavigationService.NavigateAsync($"{nameof(ShellPage)}/{nameof(ShellNavigationPage)}/{nameof(ShoppingListPage)}");
            //await NavigationService.NavigateAsync($"{nameof(ShellNavigationPage)}/{nameof(AddShoppingItemPage)}");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<ShellPage>();
            Container.RegisterTypeForNavigation<ShellNavigationPage>();
            Container.RegisterTypeForNavigation<WelcomePage>();
            Container.RegisterTypeForNavigation<ShoppingListPage>();
            Container.RegisterTypeForNavigation<AddShoppingItemPage>();
        }

        public void SendMessageForScan()
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
