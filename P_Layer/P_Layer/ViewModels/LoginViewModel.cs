using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using P_Layer.Views;
using Xamarin.Forms;
using Command = MvvmHelpers.Commands.Command;

namespace P_Layer.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        public LoginViewModel()
        {
            IncreaseCount = new Command(OnLoginClicked);
            AddUser = new Command(OnAddUser);
            Title = "Login View";
        }

        private async void OnAddUser()
        {
            var t = await Application.Current.MainPage.DisplayPromptAsync("Question 1", "What's your name?");
            var s = await Application.Current.MainPage.DisplayPromptAsync("Question 2", "What's your password?");

            var s2 = s.ToCharArray();
            s = "";
            foreach (var i in s2)
            {
                s += "*";
            }

            LoginText = "Bruger navn: "+t + "\nPassword: " + s;

        }

        public ICommand IncreaseCount { get; }
        public ICommand AddUser { get; }
        private void OnIncrease()
        {
            Count++;
            LoginText = Count.ToString();
        }


        private int _count;
        public int Count
        {
            get => _count;
            set => SetProperty(ref _count, value);
        }

        private string loginText = "Start text";

        public string LoginText
        {
            get => loginText;
            set => SetProperty(ref loginText, value);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }
}
