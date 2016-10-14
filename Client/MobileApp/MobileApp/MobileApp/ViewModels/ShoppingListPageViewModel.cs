using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MobileApp.ViewModels
{
    public class ShoppingListPageViewModel : BindableBase, INavigationAware
    {
        public ShoppingListPageViewModel()
        {

        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            ShoppingList = new ObservableCollection<ShoppingItemGroupViewModel>
            {
                new ShoppingItemGroupViewModel("Energy drinks")
                {
                    new ShoppingItemViewModel
                    {
                        Name = "Red bull",
                        Image = "red_bull.png",
                        Price = 1.5m
                    }
                }
            };
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
    }
}
