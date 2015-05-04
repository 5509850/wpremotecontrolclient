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
using WinPhoneFtp.FtpService;
using System.Threading;
using System.IO.IsolatedStorage;
using System.Windows.Media.Imaging;

namespace remotecontrolclient
{
    public partial class TimerMenu : PhoneApplicationPage
    {
        public TimerMenu()
        {
            InitializeComponent();
            var settings = IsolatedStorageSettings.ApplicationSettings;
            const string isHibernate = "isHibernate";
            radiobut_Hibernate.IsChecked = (bool)settings[isHibernate];
        }

        int sleep180 = 49;
        int sleep150 = 48;
        int sleep120 = 47;
        int sleep90 = 46;
        int sleep60 = 45;
        int sleep45 = 1111;
        int sleep30 = 44;
        int sleep20 = 51;
        int sleep10 = 52;
        int sleep5 = 101;
        int sleep3 = 1110;
        int sleep1 = 53;
        int sleepOff = 50;


        //NEW!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        const int hsleep1 = 9000;
        const int hsleep3 = 9001;
        const int hsleep5 = 9002;//--------------------------
        const int hsleep10 = 9003;
        const int hsleep20 = 9004;
        const int hsleep30 = 9005;//--------for pult code
        const int hsleep45 = 9006;
        const int hsleep60 = 9007;
        const int hsleep90 = 9008;
        const int hsleep120 = 9009;
        const int hsleep150 = 9010;
        const int hsleep180 = 9011;

        FtpClient ftpClient = null;



        string portudp = "4568";
        String ipBrdcst = "255.255.255.255";
        String localIP = "192.168.1.1";
        String versionprogram = myFTP.translate("versionprogram");
        String key = "empty";
        string usernameftp = "qwe#sdf6AsdfgzTs";
        String port = "22";
        myNetworkInterface ni = new myNetworkInterface();

        bool radiobutftp = false;
        bool ipall = true;
        String IP = "192.168.1.0";
        String code = "19216810";

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

            if (localIP == "192.168.1.1")
            {
                localIP = myFTP.GetlocalIP().ToString();
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
            //if (ftpClient != null)
            //{
            //    await ftpClient.DisconnectAsync();
            //    ftpClient = null;
            //}
        }

        private async void sendFtp(int command, String key)
        {

         
            try
            {
                if (ftpClient == null)
                    ftpClient = new FtpClient(IP, port, this.Dispatcher);
                //ftpClient.FtpAuthenticationSucceeded += ftpClient_FtpAuthenticationSucceeded;
                //ftpClient.FtpAuthenticationFailed += ftpClient_FtpAuthenticationFailed;
                //await 
                if (ftpClient != null && !ftpClient.IsConnected)
                {
                    await ftpClient.ConnectAsync();
                    if (!ftpClient.IsConnected)
                    {
                        MessageBox.Show(myFTP.translate("ipbad"));
                        ftpClient = null;
                        return;
                    }
                    await ftpClient.AuthenticateAsync(usernameftp, code);
                }

                // SystemTray.ProgressIndicator = new ProgressIndicator() { IsIndeterminate = true, Text = "Authenticating . . ." };
                //await 

                if (ftpClient.IsBusy)
                    Thread.Sleep(800);
                await ftpClient.SendCommandAsync(String.Format("{0}|{1}|{2}|{3}|{4}|{5}", command, IP, localIP, key, versionprogram, IP));
                //await ftpClient.DisconnectAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void SendCode(int code)
        {
            if (code == -1)
                return;

            try
            {
                if (radiobutftp)
                {
                    if (IP == "192.168.1.0")
                    {
                        MessageBox.Show(myFTP.translate("noip"));
                        return;
                    }
                    sendFtp(code, "empty");       

                }
                else //UDP
                {
                    //http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202864(v=vs.105).aspx#BKMK_CreatingtheUDPSocketClientUI
                    //http://10rem.net/blog/2012/06/23/using-udp-sockets-to-connect-a-windows-8-metro-style-app-to-a-net-micro-framework-device-part-3                    

                    string portudp = "4568";
                    String ipBrdcst = "255.255.255.255";
                  
                    String versionprogram = "20";
                    String key = "empty";
                    String ip = "";
                    if (ipall)
                        ip = ipBrdcst;
                    else
                        ip = IP;

                    String fullcode = code + "|"
              + code + "|"
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
                       await ni.SendMessage(fullcode, new HostName(ip), portudp);
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
            NavigationService.GoBack();
        }
      

        private void bt_off_Click(object sender, RoutedEventArgs e)
        {
            SendCode(sleepOff);
        }

        private void bt_m1_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep1);
            else
                SendCode(hsleep1);
        }

        private void bt_m3_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep3);
            else
                SendCode(hsleep3);
        }

        private void bt_m5_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep5);
            else
                SendCode(hsleep5);
        }

        private void bt_m10_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep10);
            else
                SendCode(hsleep10);
        }

        private void bt_m20_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep20);
            else
                SendCode(hsleep20);
        }

        private void bt_m30_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep30);
            else
                SendCode(hsleep30);
        }

        private void bt_m45_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep45);
            else
                SendCode(hsleep45);
        }

        private void bt_m60_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep60);
            else
                SendCode(hsleep60);
        }

        private void bt_m90_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep90);
            else
                SendCode(hsleep90);
        }

        private void bt_m120_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep120);
            else
                SendCode(hsleep120);
        }

        private void bt_m150_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep150);
            else
                SendCode(hsleep150);
        }

        private void bt_m180_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)radiobut_POFF.IsChecked)
                SendCode(sleep180);
            else
                SendCode(hsleep180);
        }

        private void but_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void radiobut_Hibernate_Checked(object sender, RoutedEventArgs e)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings["isHibernate"] = true;
            settings.Save();
        }

        private void radiobut_Hibernate_Unchecked(object sender, RoutedEventArgs e)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings["isHibernate"] = false;
            settings.Save();
        }

       

      
    }
}