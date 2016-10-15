using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class PaymentPageViewModel : BindableBase, INavigationAware
    {
        public PaymentPageViewModel()
        {

        }

        private FormattedString _paymentText;
        public FormattedString PaymentText
        {
            get
            {
                return _paymentText;
            }
            set
            {
                if (_paymentText != value)
                {
                    _paymentText = value;
                    OnPropertyChanged();
                }
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            var sum = parameters["sum"];
            var fs = new FormattedString();
            fs.Spans.Add(new Span { Text = "You have checked out the sum of " });
            fs.Spans.Add(new Span { Text = sum.ToString() , ForegroundColor = Color.FromHex("#6cb558") });
            fs.Spans.Add(new Span { Text = " CHF"});
            PaymentText = fs;
        }
    }
}
