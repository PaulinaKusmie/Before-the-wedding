using Before_the_wedding.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Before_the_wedding.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoveTestPage : ContentPage
    {
        public LoveTestPage()
        {
            InitializeComponent();
            this.BindingContext = new LoveTestViewModel();

        }

   
    }
}