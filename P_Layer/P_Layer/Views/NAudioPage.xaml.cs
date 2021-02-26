using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P_Layer.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace P_Layer.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NAudioPage : ContentPage
    {
        public NAudioPage()
        {
            InitializeComponent();
            var mainDor = FileSystem.AppDataDirectory;
        }
    }
}