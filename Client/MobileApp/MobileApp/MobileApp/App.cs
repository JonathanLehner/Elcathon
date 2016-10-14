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

namespace MobileApp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        public void SetupResources()
        {
            // setup global resources
            Resources = new DefaultResources();
        }

        protected override async void OnInitialized()
        {
            SetupResources();

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

    }
}
