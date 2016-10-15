﻿using MobileApp.ViewModels;
using MobileApp.Views;
using Newtonsoft.Json.Linq;
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

        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
        }

        protected override async void OnInitialized()
        { 
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
            var vm = ShoppingListPageViewModel.Data[productId];
            MessagingCenter.Send(this, "ScanItem", vm);
        }
    }
}
