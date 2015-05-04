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
using System.Windows.Media;

namespace remotecontrolclient
{
    public partial class photosearch : PhoneApplicationPage
    {
        public photosearch()
        {
            InitializeComponent();
        }
        myNetworkInterface ni = new myNetworkInterface();

        bool radiobutftp = false;
        bool ipall = true;
        String IP = "192.168.1.0";
        String code = "19216810";
        bool isPlay = false;
        int qntwords = 0;
        String port = "22"; 


        string portudp = "4568";
        String ipBrdcst = "255.255.255.255";
        String localIP = "192.168.1.1";
        String versionprogram = myFTP.translate("versionprogram");
        String key = "empty";
        string usernameftp = "qwe#sdf6AsdfgzTs";

        const int photosearchStart = 7000;
        const int presetSearch = 7001;
        const int psearchRemove = 7002;
        const int pscanPrepare = 7003;
        const int porderToLeft = 7004;
        const int porderToRight = 7005;
        const int pstopScan = 7006;
        const int pselectedDown = 7007;
        const int pselectedUp = 7008;
        const int pplay = 7009;
        const int pselectTimeOrder = 7010;
        const int pselectNameOrder = 7011;

        const int pfavoriteadd = 7012;
        const int pselectFolderOrder = 7013;
        const int pselectFavoriteOrder = 7014;

        const int pdriverToLeft = 7015;
        const int pdriverToRight = 7016;

        const int pfullscan = 7017;
        const int ppgup = 7018;
        const int ppgdn = 7019;


        const int pdel = 7022;

        const int KillphotosearchStart = 8000;


        const int keysend = 2020;

        const int ruskey = 2021;

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
                xscan2.Background =
                    xscan.Background = new SolidColorBrush(new Color() { R = 168, G = 255, B = 255, A = 255 });
            }
            SendCode(photosearchStart, "empty");
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
            if (!isPlay)
                SendCode(KillphotosearchStart, "empty");           
        }

        private async void sendFtpCommand(int command, string key)
        {
            await myFTP.sendFtp(command, key, IP, port, this.Dispatcher, versionprogram, localIP, code);
        }

        private void SendCode(int code, string key)
        {
            if (code == -1)
                return;

            try
            {
                if (radiobutftp)//FTP
                {
                    if (code == keysend)
                    {
                        if (key.Equals("й"))
                            sendFtpCommand(ruskey, "1");
                        else
                            if (key.Equals("ц"))
                                sendFtpCommand(ruskey, "2");
                            else
                                if (key.Equals("у"))
                                    sendFtpCommand(ruskey, "3");
                                else
                                    if (key.Equals("к"))
                                        sendFtpCommand(ruskey, "4");
                                    else
                                        if (key.Equals("е"))
                                            sendFtpCommand(ruskey, "5");
                                        else
                                            if (key.Equals("н"))
                                                sendFtpCommand(ruskey, "6");
                                            else
                                                if (key.Equals("г"))
                                                    sendFtpCommand(ruskey, "7");
                                                else
                                                    if (key.Equals("ш"))
                                                        sendFtpCommand(ruskey, "8");
                                                    else
                                                        if (key.Equals("щ"))
                                                            sendFtpCommand(ruskey, "9");
                                                        else
                                                            if (key.Equals("з"))
                                                                sendFtpCommand(ruskey, "10");
                                                            else
                                                                if (key.Equals("х"))
                                                                    sendFtpCommand(ruskey, "11");
                                                                else
                                                                    if (key.Equals("ъ"))
                                                                        sendFtpCommand(ruskey, "12");
                                                                    else
                                                                        if (key.Equals("ф"))
                                                                            sendFtpCommand(ruskey, "13");
                                                                        else
                                                                            if (key.Equals("ы"))
                                                                                sendFtpCommand(ruskey, "14");
                                                                            else
                                                                                if (key.Equals("в"))
                                                                                    sendFtpCommand(ruskey, "15");
                                                                                else
                                                                                    if (key.Equals("а"))
                                                                                        sendFtpCommand(ruskey, "16");
                                                                                    else
                                                                                        if (key.Equals("п"))
                                                                                            sendFtpCommand(ruskey, "17");
                                                                                        else
                                                                                            if (key.Equals("р"))
                                                                                                sendFtpCommand(ruskey, "18");
                                                                                            else
                                                                                                if (key.Equals("о"))
                                                                                                    sendFtpCommand(ruskey, "19");
                                                                                                else
                                                                                                    if (key.Equals("л"))
                                                                                                        sendFtpCommand(ruskey, "20");
                                                                                                    else
                                                                                                        if (key.Equals("д"))
                                                                                                            sendFtpCommand(ruskey, "21");
                                                                                                        else
                                                                                                            if (key.Equals("ж"))
                                                                                                                sendFtpCommand(ruskey, "22");
                                                                                                            else
                                                                                                                if (key.Equals("э"))
                                                                                                                    sendFtpCommand(ruskey, "23");
                                                                                                                else
                                                                                                                    if (key.Equals("я"))
                                                                                                                        sendFtpCommand(ruskey, "24");
                                                                                                                    else
                                                                                                                        if (key.Equals("ч"))
                                                                                                                            sendFtpCommand(ruskey, "25");
                                                                                                                        else
                                                                                                                            if (key.Equals("с"))
                                                                                                                                sendFtpCommand(ruskey, "26");
                                                                                                                            else
                                                                                                                                if (key.Equals("м"))
                                                                                                                                    sendFtpCommand(ruskey, "27");
                                                                                                                                else
                                                                                                                                    if (key.Equals("и"))
                                                                                                                                        sendFtpCommand(ruskey, "28");
                                                                                                                                    else
                                                                                                                                        if (key.Equals("т"))
                                                                                                                                            sendFtpCommand(ruskey, "29");
                                                                                                                                        else
                                                                                                                                            if (key.Equals("ь"))
                                                                                                                                                sendFtpCommand(ruskey, "30");
                                                                                                                                            else
                                                                                                                                                if (key.Equals("б"))
                                                                                                                                                    sendFtpCommand(ruskey, "31");
                                                                                                                                                else
                                                                                                                                                    if (key.Equals("ю"))
                                                                                                                                                        sendFtpCommand(ruskey, "32");
                                                                                                                                                    else
                                                                                                                                                        if (key.Equals("ё"))
                                                                                                                                                            sendFtpCommand(ruskey, "33");
                                                                                                                                                        else
                                                                                                                                                            sendFtpCommand(code, key);
                        return;
                    }
                    sendFtpCommand(code, key);
                }
                else //UDP
                {
                    //http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202864(v=vs.105).aspx#BKMK_CreatingtheUDPSocketClientUI
                    //http://10rem.net/blog/2012/06/23/using-udp-sockets-to-connect-a-windows-8-metro-style-app-to-a-net-micro-framework-device-part-3                    

                    string portudp = "4568";
                    String ipBrdcst = "255.255.255.255";
                 
                    String versionprogram = "20";
                    String ip = "";
                    if (ipall)
                        ip = ipBrdcst;
                    else
                        ip = IP;
                    if (localIP == "192.168.1.1")
                    {
                        localIP = myFTP.GetlocalIP().ToString();
                    }
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
        }


        private bool isTextValid()
        {
            return (Char.IsLetter(searchword.Text[searchword.Text.Length - 1]) || Char.IsDigit(searchword.Text[searchword.Text.Length - 1]));
        }

        private void left_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pdriverToLeft, "empty");
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            //   SendCode(resetSearch, "empty");
            searchword.Text = String.Empty;
        }

        private void scan_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pscanPrepare, "empty");
        }

        private void right_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pdriverToRight, "empty");
        }

        private void fullscan_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pfullscan, "empty");
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pstopScan, "empty");
        }

        private void searchword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchword.Text == String.Empty)
            {
                SendCode(presetSearch, "empty");
                qntwords = 0;
                return;
            }

            if (!isTextValid())
            {
                searchword.Text = searchword.Text.Remove(searchword.Text.Length - 1);
                searchword.Select(searchword.Text.Length, searchword.Text.Length);
            }
            else
            {
                if (searchword.Text.Length < qntwords)
                {
                    SendCode(psearchRemove, "empty");
                }
                else
                {
                    SendCode(keysend, searchword.Text[searchword.Text.Length - 1].ToString());
                }
                qntwords = searchword.Text.Length;
            }
        }



        private void close_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void but_play_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pplay, "empty");
            isPlay = true;
            NavigationService.GoBack();
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            SendCode(porderToLeft, "empty");
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            SendCode(porderToRight, "empty");
        }

        private void PgUp_Click(object sender, RoutedEventArgs e)
        {
            SendCode(ppgup, "empty");
        }

        private void FavAdd_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pfavoriteadd, "empty");
        }

        private void PgDw_Click(object sender, RoutedEventArgs e)
        {
            SendCode(ppgdn, "empty");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pdel, "empty");
        }

        private void OrderTime_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pselectTimeOrder, "empty");
        }

        private void OrderFile_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pselectNameOrder, "empty");
        }

        private void OrderFolder_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pselectFolderOrder, "empty");
        }

        private void OrderFav_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pselectFavoriteOrder, "empty");
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pselectedDown, "empty");
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pselectedUp, "empty");
        }
    }
}