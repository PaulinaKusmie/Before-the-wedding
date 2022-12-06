using Before_the_wedding.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Before_the_wedding.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private string loginText;
        private string passwordText;
        private Guid id;
        INavigation Navigation => Application.Current.MainPage.Navigation;
        #endregion
        public ICommand LoginCommand => new Command(() => { Navigation.PushModalAsync(new RegisterPage()); });

        #region Properties
        public string LoginText
        {
            get => loginText;
            set
            {
                loginText = value;
                OnPropertyChanged();
            }
        }

        public string PasswordText
        {
            get => passwordText;
            set
            {
                passwordText = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public LoginViewModel()
        {
            
        }

   
    }
}
