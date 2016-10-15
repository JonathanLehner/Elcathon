using MobileApp.Views;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class ScannedListPageViewModel : BindableBase
    {
        public ScannedListPageViewModel()
        {
            MessagingCenter.Subscribe<ShoppingListPage, ShoppingItemViewModel>(this, "AddScanItem", (sender, vm) =>
            {
                Device.BeginInvokeOnMainThread(() => {
                    ShoppingItemViewModel target;
                    if ((target = ScannedList.SingleOrDefault(i => i.Name == vm.Name)) != null)
                    {
                        target.Quantity++;
                    }
                    else
                    {
                        ScannedList.Add(vm);
                    }
                });
                
            });

        }

        private ObservableCollection<ShoppingItemViewModel> _scannedList = new ObservableCollection<ShoppingItemViewModel>();
        public ObservableCollection<ShoppingItemViewModel> ScannedList
        {
            get
            {
                return _scannedList;
            }
            set
            {
                if (_scannedList != value)
                {
                    _scannedList = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
