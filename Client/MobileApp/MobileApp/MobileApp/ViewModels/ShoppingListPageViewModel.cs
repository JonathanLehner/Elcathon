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
                ShoppingList.Where(sl => sl.Category.Name == vm.CategoryName).SingleOrDefault().Add(vm);
            });
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("AddCategory"))
            {
                ShoppingList.Add(new ShoppingItemGroupViewModel
                {
                    Category = (ShoppingCategoryViewModel) parameters["AddCategory"]
                });
            }
            else
            {
                var group = new ShoppingItemGroupViewModel()
                {
                    Category = new ShoppingCategoryViewModel
                    {
                        Name = "Breverage",
                        Quantity = 2,
                        ScannedQuantity = 1,
                    }

                };
                group.Add(new ShoppingItemViewModel
                {
                    Name = "Red bull",
                    Image = "red_bull.png",
                    Price = 1.5m,
                    CategoryName = "Breverage",
                    Quantity = 1
                });
                ShoppingList = new ObservableCollection<ShoppingItemGroupViewModel>
                {
                    group
                };
            }
        }

        private ObservableCollection<ShoppingItemGroupViewModel> _shoppingList;
        public ObservableCollection<ShoppingItemGroupViewModel> ShoppingList
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
