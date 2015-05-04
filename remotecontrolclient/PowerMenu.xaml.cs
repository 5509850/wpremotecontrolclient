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
using System.Windows.Media.Imaging;

namespace remotecontrolclient
{
    public partial class PowerMenu : PhoneApplicationPage
    {
        public PowerMenu()
        {
            InitializeComponent();
        }

        int lockscr = 1100;
        int shutdown = 8;
        int reboot = 9;
        int poweroff = 10;
        int hibernate = 31;
        int standby = 32;
        bool radiobutftp = false;
        bool ipall = true;
        String IP = "192.168.1.0";
        String code = "19216810";
        String mac = "00:00:00:00:00:00";
        String port = "22";
        FtpClient ftpClient = null;



        string portudp = "4568";
        String ipBrdcst = "255.255.255.255";
        String localIP = "192.168.1.1";
        String versionprogram = myFTP.translate("versionprogram");
        String key = "empty";
        string usernameftp = "qwe#sdf6AsdfgzTs";

        myNetworkInterface ni = new myNetworkInterface();

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

            if (NavigationContext.QueryString.TryGetValue("mac", out mac))
            {
                if (String.IsNullOrEmpty(mac))
                {
                    mac = "00:00:00:00:00:00";
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

        private void bt_on_Click(object sender, RoutedEventArgs e)
        {
             String ipBrdcst = "255.255.255.255";
             string portudp = "9050";
            if (mac.Equals("00:00:00:00:00:00"))
            {
                //MessageBox.Show("MAC address is not valid!");
                MessageBox.Show("MAC address некорректный!");
                NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
                return;
            }

            if (ni == null)
            {
                ni = new myNetworkInterface();
            }
            try
            {
                ni.SendMessage(StrToMac(mac), new HostName(ipBrdcst), portudp);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        static byte[] StrToMac(string s)
        {
            List<byte> arr = new List<byte>(102);

            string[] macs = s.Split(' ', ':', '-');

            for (int i = 0; i < 6; i++)
                arr.Add(0xff);

            for (int j = 0; j < 16; j++)
                for (int i = 0; i < 6; i++)
                    arr.Add(Convert.ToByte(macs[i], 16));

            return arr.ToArray();
        }

        private void bt_poweroff_Click(object sender, RoutedEventArgs e)
        {
            SendCode(poweroff);
        }

        private void bt_reboot_Click(object sender, RoutedEventArgs e)
        {
            SendCode(reboot);
        }

        private void bt_shutdown_Click(object sender, RoutedEventArgs e)
        {
            SendCode(shutdown);
        }

        private void bt_hibernate_Click(object sender, RoutedEventArgs e)
        {
            SendCode(hibernate);
        }

        private void bt_standby_Click(object sender, RoutedEventArgs e)
        {
            SendCode(standby);
        }

        private void bt_lockscr_Click(object sender, RoutedEventArgs e)
        {
            SendCode(lockscr);
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


        private void SendCode(int code)
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

            NavigationService.GoBack();
        }

        private void but_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
      
    }
}