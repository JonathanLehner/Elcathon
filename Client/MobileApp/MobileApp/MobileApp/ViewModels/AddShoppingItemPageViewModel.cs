using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace MobileApp.ViewModels
{
    public class AddShoppingItemPageViewModel : BindableBase, INavigationAware
    {

        public ShoppingCategoryViewModel Category { get; set; } 

        private INavigationService _navigationService;
        public AddShoppingItemPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Category = new ShoppingCategoryViewModel();
        }

        private ICommand _addToListCommand;
        public ICommand AddToListCommand => _addToListCommand ?? (_addToListCommand = new DelegateCommand(async ()=> {
            await _navigationService.GoBackAsync(new NavigationParameters
            {
                ["AddCategory"]=Category
            });
        }));
    }
}
