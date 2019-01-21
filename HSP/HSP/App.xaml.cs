using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HSP.Abstractions;
using HSP.Services;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace HSP
{
    public partial class App : Application
    {
        public static ICloudService CloudService { get; set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Pages.EntryPage());

            CloudService = new AzureCloudService();

        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
