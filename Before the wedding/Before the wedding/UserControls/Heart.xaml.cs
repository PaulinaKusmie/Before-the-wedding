using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using Before_the_wedding.ViewModels;
using System.Runtime.CompilerServices;

namespace Before_the_wedding
{
    public partial class Heart : ContentView
    {

        public Heart()
        {
            InitializeComponent();
        }

        #region BindableProperty

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



            public static readonly BindableProperty AngleProperty =
        BindableProperty.Create("Angle", typeof(int), typeof(Heart), 0,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: anglePropertyChanged);

        private static void anglePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (Heart)bindable;
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

        private void tapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
        #endregion



        //public static readonly BindableProperty JAPIERDOLEProperty =
        //   BindableProperty.Create(nameof(JAPIERDOLE), typeof(ICommand), typeof(Heart), null,
        //    defaultBindingMode: BindingMode.TwoWay,
        //    propertyChanged: commandPropertyChanged);

        //private static void commandPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var control = (Heart)bindable;
        //    (control.FindByName("tapGestureRecognizer") as TapGestureRecognizer).Command = (ICommand)newValue;
        //}


        //public ICommand JAPIERDOLE

        //{
        //    get { return (ICommand)GetValue(JAPIERDOLEProperty); }
        //    set
        //    {
        //        SetValue(JAPIERDOLEProperty, value);
        //    }
        //}


        /////////////////////////////////////////////////

        //public static readonly BindableProperty ClickCommandProperty = 
        //    BindableProperty.Create(nameof(ClickCommand), typeof(ICommand), typeof(Heart));

        //public static readonly BindableProperty ClickCommandParameterProperty =
        //  BindableProperty.Create(nameof(ClickCommandParametr), typeof(ICommand), typeof(Heart));
        //public ICommand ClickCommand
        //{
        //    get => (ICommand)GetValue(ClickCommandProperty);
        //    set => SetValue(ClickCommandProperty, value);
        //}

        //public ICommand ClickCommandParametr
        //{
        //    get => (ICommand)GetValue(ClickCommandParameterProperty);
        //    set => SetValue(ClickCommandParameterProperty, value);
        //}

        //private void tapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        //{
        //    if(ClickCommand != null && ClickCommand.CanExecute(null))
        //    {
        //        ClickCommand.Execute(null);
        //    }
        //}



        //protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)        
        //{
        //    base.OnPropertyChanged(propertyName);
        //     if(propertyName == m)

        //}
      






        //public static void Execute(ICommand command)
        //{
        //    if (command == null) return;
        //    if (command.CanExecute(null))
        //    {
        //        command.Execute(null);
        //    }
        //}



        //public Command OnTap => new Command(() => Execute(ClickCommand));













    }
}