using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace Before_the_wedding.UserControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Heard : ContentView
    {
        public Heard()
        {
            InitializeComponent();
        }

        #region BindableProperty

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(string), typeof(Heard), string.Empty,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: textPropertyChanged);

        private static void textPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Heard)bindable;
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



        public static readonly BindableProperty AngleProperty =
    BindableProperty.Create("Angle", typeof(int), typeof(Heard), 0,
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: anglePropertyChanged);

        private static void anglePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Heard)bindable;
            (control.FindByName("rotateTransform") as RotateTransform).Angle = (int)newValue;
        }


        public int Angle
        {
            get { return (int)base.GetValue(AngleProperty); }
            set
            {
                base.SetValue(AngleProperty, value);
            }
        }

        public static readonly BindableProperty HClickCommandProperty =
         BindableProperty.Create(nameof(HClickCommand), typeof(ICommand), typeof(Heard));

        public static readonly BindableProperty HClickCommandParameterProperty =
          BindableProperty.Create(nameof(HClickCommandParametr), typeof(ICommand), typeof(Heard));
        public ICommand HClickCommand
        {
            get => (ICommand)base.GetValue(HClickCommandProperty);
            set => base.SetValue(HClickCommandProperty, value);
        }

        public ICommand HClickCommandParametr
        {
            get => (ICommand)base.GetValue(HClickCommandParameterProperty);
            set => base.SetValue(HClickCommandParameterProperty, value);
        }

        #endregion


        private void tapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (HClickCommand != null && HClickCommand.CanExecute(null))
            {
                HClickCommand.Execute(null);
            }
        }

    }
}