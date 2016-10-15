using MobileApp.Views;
using Newtonsoft.Json.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using RestSharp.Portable;
using RestSharp.Portable.HttpClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class ShoppingListPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        public ShoppingListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            MessagingCenter.Subscribe<ShoppingListPage, ShoppingItemViewModel>(this, "AddScanItem", (sender, vm) => {
                var cat = ShoppingList.Where(sl => sl.Name == vm.Category).SingleOrDefault() as ShoppingCategoryViewModel;
                if (cat == null) return;
                cat.ScannedQuantity++;
                if(cat.QuantityLeft<=0)
                {
                    // remove cat from list
                    ShoppingList.Remove(cat);
                }
            });

        }


        public static Dictionary<string, ShoppingItemViewModel> Data = new Dictionary<string, ShoppingItemViewModel>();

        private async Task GetDataFromServer()
        {
            using (var client = new RestClient(new Uri("https://ytv3odwce7.execute-api.us-west-2.amazonaws.com/prod/")))
            {
                var request = new RestRequest("ProductCatalog", Method.GET);
                request.AddParameter("TableName", "ProductCatalog");
                var result = await client.Execute<JObject>(request);
                foreach(var item in result.Data.Value<JArray>("Items"))
                {
                    var vm = new ShoppingItemViewModel();
                    vm.Name = item["Name"].ToString();
                    vm.Category = item["Category"].ToString();
                    vm.Price = decimal.Parse(item["Price"].ToString());
                    vm.Image = "_"+item["ProductId"].ToString()+".png";
                    vm.ProductId = item["ProductId"].ToString();
                    Data.Add(vm.ProductId, vm);
                }
            }
        }


        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }


        bool first = true;
        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (first)
            {
                await GetDataFromServer();
                first = false;
            }

            if (parameters.ContainsKey("AddCategory"))
            {
                ShoppingList.Add((ShoppingCategoryViewModel)parameters["AddCategory"]);
            }
            else
            {
                ShoppingList = new ObservableCollection<ShoppingCategoryViewModel>
                {
                    new ShoppingCategoryViewModel()
                    {
                        Name = "Juice",
                        Quantity = 2,
                        ScannedQuantity = 0,

                    },
                    new ShoppingCategoryViewModel()
                    {
                        Name = "Umbrella",
                        Quantity = 1,
                        ScannedQuantity = 0,

                    },
                    new ShoppingCategoryViewModel()
                    {
                        Name = "Raspberry Pi",
                        Quantity = 1,
                        ScannedQuantity = 0,

                    }
                };
            }
        }

        private ObservableCollection<ShoppingCategoryViewModel> _shoppingList;
        public ObservableCollection<ShoppingCategoryViewModel> ShoppingList
        {
            get
            {
                return _shoppingList;
            }
            set
            {
                if (_shoppingList != value)
                {
                    _shoppingList = value;
                    OnPropertyChanged();
                }
            }
        }

        private ICommand _addItemCommand;
        public ICommand AddItemCommand => _addItemCommand ?? (_addItemCommand = new DelegateCommand(async () => {
            await _navigationService.NavigateAsync(nameof(AddShoppingItemPage));
        }));

    }
}
