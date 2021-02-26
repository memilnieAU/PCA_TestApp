using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;

using Xamarin.Essentials;

namespace P_Layer.ViewModels
{
    class NAudioViewModel: ViewModelBase
    {
        public NAudioViewModel()
        {
            StartRecordCommand = new Command(StartRecord);
        }
        private string status = "Du har nu åbnet NAudioPage";

        public string Status
        {
            get => status;
            set => SetProperty(ref status, value);
        }

        public ICommand StartRecordCommand { get; }

        private void StartRecord()
        {
        }
    }

}
