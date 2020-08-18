using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WaitInPlace.Classes;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeroFiveBit.Utils;

namespace WaitInPlace
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodePage : ContentPage
    {
        int yourNum;
        int waitTimeOrig2;
        string yourNumStr;
        int origNum;
        static Countdown countdown;

          protected async Task setExitTime(int venue_uid)
          {
               Console.WriteLine("hie from set exit");
              //Console.WriteLine("starting of the get func!!");
              ExitInfo newexit = new ExitInfo();
              newexit.user_id = Preferences.Get("customer_id", 0);
              newexit.uid = venue_uid;
              //string now = DateTime.Now.TimeOfDay.ToString("h:mm:ss tt");
              DateTime now = DateTime.Now.ToLocalTime();
              string currentTime = (string.Format("{0}", now));
              //string now = "12:02:32";
              Console.WriteLine("The current time is " + now.TimeOfDay);
              newexit.exit_time = currentTime.Substring(9, 9);
              //Console.WriteLine("the uid1 is :" + venue_uid);
              var newExitJSONString = JsonConvert.SerializeObject(newexit);
              var content = new StringContent(newExitJSONString, Encoding.UTF8, "application/json");
              var request = new HttpRequestMessage();
              request.RequestUri = new Uri("https://61vdohhos4.execute-api.us-west-1.amazonaws.com/dev/api/v2/update_queue");
              request.Method = HttpMethod.Post;
              request.Content = content;
              var client = new HttpClient();
              HttpResponseMessage response = await client.SendAsync(request);
              Console.WriteLine("set ecitInfo ends");
          }
        
        public BarcodePage(int waitTimeOrig, int placeInLine)
        {
            InitializeComponent();
            yourNum = placeInLine;
            origNum = placeInLine;
            waitTimeOrig2 = waitTimeOrig;
            countdown = new Countdown();
            countdown.StartUpdating(300);
            cdLabel.SetBinding(Label.TextProperty,
                new Binding("RemainTime", BindingMode.Default, new CountdownConverter()));
            cdLabel.BindingContext = countdown;
            
            Device.StartTimer(TimeSpan.FromMinutes(5), () =>
            {
                if (origNum < yourNum)
                {
                    origNum += 5;
                    return true;
                }

                //Navigation.PushAsync(new BarcodePage(yourNum, waitTimeOrig2));
                return false; // True = Repeat again, False = Stop the timer
            });
        }
        private void bump_in(object sender, EventArgs e)
        {
            yourNum += 5;
            yourNumStr = yourNum.ToString();
            Navigation.PushAsync(new yourNumberPage(waitTimeOrig2, yourNum));
        }

        private void exit_Clicked(object sender, EventArgs e)
        {
            int v_uid = Preferences.Get("v_uid", 1);
            Console.WriteLine("the given v__uid:" + v_uid);
            setExitTime(v_uid);
            DisplayAlert("Exit Store", "You are exitting the store. Thanks for using WIP!.", "Continue");
            Navigation.PushAsync(new MainPage());
        }
    }
}