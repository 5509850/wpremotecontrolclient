using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Networking;
using System.Threading;
using System.Windows.Media.Imaging;

namespace remotecontrolclient
{
    public partial class ChMenu : PhoneApplicationPage
    {
        public ChMenu()
        {
            InitializeComponent();
        }

        myNetworkInterface ni = new myNetworkInterface();
        

        bool radiobutftp = false;
        bool ipall = true;
        String IP = "192.168.1.0";
        String code = "19216810";
        String port = "22";

        string portudp = "4568";
        String ipBrdcst = "255.255.255.255";
        String localIP = "192.168.1.1";
        String versionprogram =  myFTP.translate("versionprogram");
        String key = "empty";
        string usernameftp = "qwe#sdf6AsdfgzTs";

        const int ___ = 908;
        const int _0 = 909;
        const int _1 = 910;
        const int _2 = 911;
        const int _3 = 912;
        const int _4 = 913;
        const int _5 = 914;
        const int _6 = 915;
        const int _7 = 916;
        const int _8 = 917;
        const int _9 = 918;
        const int _c_back = 919;

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            versionprogram = myFTP.translate("versionprogram");
            string rbftp;
            string ip_all;
            if (NavigationContext.QueryString.TryGetValue("radiobutftp", out rbftp))
            {
                if (!String.IsNullOrEmpty(rbftp))
                {
                    radiobutftp = (rbftp == "1");
                }
            }

            if (NavigationContext.QueryString.TryGetValue("ip", out IP))
            {
                if (String.IsNullOrEmpty(IP))
                {
                    IP = "192.168.1.0";
                }
            }

            if (NavigationContext.QueryString.TryGetValue("ipall", out ip_all))
            {
                if (!String.IsNullOrEmpty(ip_all))
                {
                    ipall = (ip_all == "1");
                }
            }

            if (NavigationContext.QueryString.TryGetValue("code", out code))
            {
                if (String.IsNullOrEmpty(code))
                {
                    code = "19216810";
                }
            }

            if (!IsDarkTheme())
            {
                LayoutRoot.Background = null;
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/bg2.screen-wvga.jpg", UriKind.Relative)).Stream);
                background.ImageSource = tn;
            }

        }

        private bool IsDarkTheme()
        {
            try
            {
                return (double)Application.Current.Resources["PhoneDarkThemeOpacity"] > 0;
            }
            catch (Exception er)
            {
                return true;
            }
        }

        protected async override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        private async void sendFtp(int command, String key)
        {

           await myFTP.sendFtp(command, key, IP, port, this.Dispatcher, versionprogram, localIP, code);
        }


        private void SendCode(int command)
        {
            if (command == -1)
                return;

            try
            {
                if (radiobutftp)
                {
                    //http://msdn.microsoft.com/ru-ru/magazine/dn385710.aspx

                    if (IP == "192.168.1.0")
                    {
                        MessageBox.Show(myFTP.translate("noip"));
                        
                        return;
                    }
                    sendFtp(command, "empty");         

                }
                else //UDP
                {
                    //http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202864(v=vs.105).aspx#BKMK_CreatingtheUDPSocketClientUI
                    //http://10rem.net/blog/2012/06/23/using-udp-sockets-to-connect-a-windows-8-metro-style-app-to-a-net-micro-framework-device-part-3                    

                    string portudp = "4568";
                    String ipBrdcst = "255.255.255.255";
                    String localIP = "192.168.1.1";
                    String versionprogram = "20";
                    String key = "empty";
                    String ip = "";
                    if (ipall)
                        ip = ipBrdcst;
                    else
                        ip = IP;

                    String fullcode = command + "|"
              + command + "|"
                  + localIP + "|"
                  + key + "|"
                  + versionprogram + "|"
                  + IP;//  1}|{2}|{3}|{4}|{5}", , textBox_code.Text, listIP[0], key, versionprogram, textBox_ip.Text), portudp);  

                    fullcode = fullcode + "|#";//add "|#" for not wait answer from server!

                    if (ni == null)
                    {
                        ni = new myNetworkInterface();
                    }
                    try
                    {
                        ni.SendMessage(fullcode, new HostName(ip), portudp);                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }            
        }
      

        private void but_cback_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            //NavigationService.Navigate(new Uri("/MainPage.xaml?code=1", UriKind.Relative));

        }

        private void but_c1_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_1);
        }

        private void but_c2_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_2);
        }

        private void but_c3_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_3);
        }

        private void but_c4_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_4);
        }

        private void but_c5_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_5);
        }

        private void but_c6_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_6);
        }

        private void but_c7_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_7);
        }

        private void but_c8_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_8);
        }

        private void but_c9_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_9);
        }

        private void but_c__Click(object sender, RoutedEventArgs e)
        {
            SendCode(___);
        }

        private void but_c0_Click(object sender, RoutedEventArgs e)
        {
            SendCode(_0);
        }
    }
}