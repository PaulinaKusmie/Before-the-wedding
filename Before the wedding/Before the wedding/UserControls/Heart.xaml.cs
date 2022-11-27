using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Before_the_wedding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Heart : ContentView
    {
        public Heart()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(Heart), string.Empty,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: textPropertyChanged);

        private static void textPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Heart)bindable;
            (control.FindByName("textLabel") as Label).Text = newValue.ToString();
        }


        public string Text
        {
            get { return base.GetValue(TextProperty).ToString(); }
            set
            {
                base.SetValue(TextProperty, value);
            }
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            path.RenderTransform = new ScaleTransform() { ScaleX = 0.5, ScaleY = 0.5 };
        }
    }
}