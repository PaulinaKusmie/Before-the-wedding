using Before_the_wedding.Models;
using Before_the_wedding.Services;
using Before_the_wedding.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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
        private List<Person> personList;

        INavigation Navigation => Application.Current.MainPage.Navigation;
        #endregion

        public ICommand RegisterCommand => new Command(() => { Navigation.PushModalAsync(new RegisterPage()); });
        public ICommand LoginCommand { get; set; }

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

        public List<Person> PersonList
        {
            get => personList;
            set
            {
                personList = value;
                OnPropertyChanged();
            }
        }
        #endregion

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLogin);
        }

        private async void OnLogin()
        {

            if (PersonList == null)
                PersonList = await DataPersonStore.Login();

            foreach (Person item in PersonList)
            {
                if (LoginText == item.UserLogin && PasswordText == item.Password)
                {
                    Login.copuleId = item.CopuleGuidId;
                    Login.personId = item.Id;
                    await Shell.Current.GoToAsync("..");
                    return;
                }
               
            }
            await App.Current.MainPage.DisplayAlert("Uwaga!", "Błedny login lub hasło sprój ponownie", "OK");
        }

    }
}

public static class Login
{
    public static Guid copuleId;
    public static Guid personId;
    
}
