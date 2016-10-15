using MobileApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MobileApp.ViewModels
{
    public class ShoppingListPageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;
        public ShoppingListPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
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
