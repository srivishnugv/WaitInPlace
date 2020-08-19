using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace WaitInPlace
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void To_Registration_Page(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegistrationPage());
        }

        private void To_Venue_Page(object sender, EventArgs e)
        {
           // Preferences.Set("name1", "name");
           // Preferences.Set("email1", "email");
           // Preferences.Set("phone1", "phone");

            Navigation.PushAsync(new VenuePage());
        }

        private void To_Data_Page(object sender, EventArgs e)
        {
            Navigation.PushAsync(new DataDisplayPage());
        }

    }
}
