using MobileApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                var cat = ShoppingList.Where(sl => sl.Name == vm.CategoryName).SingleOrDefault() as ShoppingCategoryViewModel;
                if (cat == null) return;
                cat.ScannedQuantity++;
                if(cat.QuantityLeft<=0)
                {
                    // remove cat from list
                    ShoppingList.Remove(cat);
                }
            });

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("AddCategory"))
            {
                ShoppingList.Add((ShoppingCategoryViewModel)parameters["AddCategory"]);
            }
            else
            {
                var group = new ShoppingCategoryViewModel()
                {
                    Name = "Breverage",
                    Quantity = 2,
                    ScannedQuantity = 0,

                };
                ShoppingList = new ObservableCollection<ShoppingCategoryViewModel>
                {
                    group
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
