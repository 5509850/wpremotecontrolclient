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
    public partial class musicsearch : PhoneApplicationPage
    {
        public musicsearch()
        {
            InitializeComponent();
        }
        myNetworkInterface ni = new myNetworkInterface();
       

        
        
        String port = "22"; 
        String localIP = "192.168.1.1";
        String versionprogram = myFTP.translate("versionprogram");    
        string usernameftp = "qwe#sdf6AsdfgzTs";

        bool radiobutftp = false;
        bool ipall = true;
        String IP = "192.168.1.0";
        String code = "19216810";
        bool isPlay = false;
        int qntwords = 0;

        const int audiosearchStart = 5000;

        const int resetSearch = 5001;
        const int searchRemove = 5002;
        const int scanPrepare = 5003;
        const int orderToLeft = 5004;
        const int orderToRight = 5005;
        const int stopScan = 5006;
        const int selectedDown = 5007;
        const int selectedUp = 5008;
        const int play = 5009;
        const int selectTimeOrder = 5010;
        const int selectNameOrder = 5011;

        const int favoriteadd = 5012;
        const int selectFolderOrder = 5013;
        const int selectFavoriteOrder = 5014;

        const int driverToLeft = 5015;
        const int driverToRight = 5016;

        const int fullscan = 5017;
        const int pgup = 5018;
        const int pgdn = 5019;

        const int playfolder = 5022;
        const int playalbum = 5023;
        const int playartist = 5024;
        const int playall = 5025;
        const int playrandom = 5026;
        const int ArtistOrder = 5027;
        const int AlbumOrder = 5028;

        const int KillaudiosearchStart = 6000;

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
            SendCode(audiosearchStart, "empty");
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
                SendCode(KillaudiosearchStart, "empty");          
            //комманду закрытия - если не плей(добавить проверку)
        }

        private async void sendFtpCommand(int command, string key)
        {
            await myFTP.sendFtp(command, key, IP, port, this.Dispatcher, versionprogram, localIP, code);
        }

        private async void SendCode(int code, string key)
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
                    if (localIP == "192.168.1.1")
                    {
                        localIP = myFTP.GetlocalIP().ToString();
                    }
                 
                    String versionprogram = "20";
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
        }


        private bool isTextValid()
        {
            return (Char.IsLetter(searchword.Text[searchword.Text.Length - 1]) || Char.IsDigit(searchword.Text[searchword.Text.Length - 1]));
        }

        private void left_Click(object sender, RoutedEventArgs e)
        {
            SendCode(driverToLeft, "empty");
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            //   SendCode(resetSearch, "empty");
            searchword.Text = String.Empty;
        }

        private void scan_Click(object sender, RoutedEventArgs e)
        {
            SendCode(scanPrepare, "empty");
        }

        private void right_Click(object sender, RoutedEventArgs e)
        {
            SendCode(driverToRight, "empty");
        }

        private void fullscan_Click(object sender, RoutedEventArgs e)
        {
            SendCode(fullscan, "empty");
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            SendCode(stopScan, "empty");
        }

        private void searchword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (searchword.Text == String.Empty)
            {
                SendCode(resetSearch, "empty");
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
                    SendCode(searchRemove, "empty");
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
            pivot.SelectedIndex = 1;
        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {
            SendCode(orderToLeft, "empty");
        }

        private void next_Click(object sender, RoutedEventArgs e)
        {
            SendCode(orderToRight, "empty");
        }

        private void PgUp_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pgup, "empty");
        }

        private void FavAdd_Click(object sender, RoutedEventArgs e)
        {
            SendCode(favoriteadd, "empty");
        }

        private void PgDw_Click(object sender, RoutedEventArgs e)
        {
            SendCode(pgdn, "empty");
        }

        private void OrderTime_Click(object sender, RoutedEventArgs e)
        {
            SendCode(selectTimeOrder, "empty");
        }

        private void OrderFile_Click(object sender, RoutedEventArgs e)
        {
            SendCode(selectNameOrder, "empty");
        }

        private void OrderFolder_Click(object sender, RoutedEventArgs e)
        {
            SendCode(selectFolderOrder, "empty");
        }

        private void OrderFav_Click(object sender, RoutedEventArgs e)
        {
            SendCode(selectFavoriteOrder, "empty");
        }

        private void down_Click(object sender, RoutedEventArgs e)
        {
            SendCode(selectedDown, "empty");
        }

        private void up_Click(object sender, RoutedEventArgs e)
        {
            SendCode(selectedUp, "empty");
        }

        private void OrderArtist_Click(object sender, RoutedEventArgs e)
        {
            SendCode(ArtistOrder, "empty");
        }

        private void OrderAlbum_Click(object sender, RoutedEventArgs e)
        {
            SendCode(AlbumOrder, "empty");
        }

        private void but_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void one_Click(object sender, RoutedEventArgs e)
        {
            SendCode(play, "empty");
            isPlay = true;
            NavigationService.GoBack();
        }

        private void allfolder_Click(object sender, RoutedEventArgs e)
        {
            SendCode(playfolder, "empty");
            isPlay = true;
            NavigationService.GoBack();
        }

        private void allalbum_Click(object sender, RoutedEventArgs e)
        {
            SendCode(playalbum, "empty");
            isPlay = true;
            NavigationService.GoBack();
        }

        private void artist_Click(object sender, RoutedEventArgs e)
        {
            SendCode(playartist, "empty");
            isPlay = true;
            NavigationService.GoBack();
        }

        private void alltrack_Click(object sender, RoutedEventArgs e)
        {
            SendCode(playall, "empty");
            isPlay = true;
            NavigationService.GoBack();
        }

        private void random_Click(object sender, RoutedEventArgs e)
        {
            SendCode(playrandom, "empty");
            isPlay = true;
            NavigationService.GoBack();
        }
    }
}