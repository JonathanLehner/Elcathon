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
        private static ScannedListPageViewModel _instance;
        public ScannedListPageViewModel()
        {
            _instance = this;
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
                        vm.Quantity = 1;
                        ScannedList.Add(vm);
                        OnPropertyChanged(nameof(SumText));
                    }
                });
                
            });

        }

        public FormattedString SumText
        {
            get
            {
                var fs = new FormattedString();
                fs.Spans.Add(new Span { Text = "Sum: " });
                fs.Spans.Add(new Span { Text = GetTotalSum().ToString(), ForegroundColor = Color.FromHex("#6cb558") });
                fs.Spans.Add(new Span { Text = " CHF" });
                return fs;
            }
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

        public static decimal GetTotalSum()
        {
            return _instance.ScannedList.Sum(l => l.TotalPrice);
        }

    }
}
