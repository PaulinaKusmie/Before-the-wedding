using Before_the_wedding.Models;
using Before_the_wedding.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Before_the_wedding.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage(Item item)
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel(item);
        }
    }
}