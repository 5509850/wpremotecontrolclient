using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using remotecontrolclient.Resources;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Phone.Net.NetworkInformation;
using System.Text;
using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using Windows.Networking;
using WinPhoneFtp.FtpService;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Devices;
using Windows.Networking.Connectivity;
using Microsoft.Phone.Tasks;
using System.Reflection;
using GoogleAds;



//FTP DEMO
//e:\wp8sample\FTP.0913-Final\WinPhoneFtp\WinPhoneFtp.DemoApp\

namespace remotecontrolclient
{
    public partial class MainPage : PhoneApplicationPage
    {
       // SomaAdViewer somaAdViewer;       
        
        //ApplicationBarIconButton iconButtonStart;
        //ApplicationBarIconButton iconButtonStop;


        bool isLoaded = false;
        MultiResImageChooserUri mi;// = new MultiResImageChooserUri();
        private InterstitialAd interstitialAd;
        private bool InterAdsIsLoaded = false;


        AdView bannerAd = null;
        // Конструктор
        public MainPage()
        {
            InitializeComponent();
            // Задайте для контекста данных элемента управления listbox пример данных
            DataContext = App.ViewModel;
            // Пример кода для локализации ApplicationBar
            //BuildLocalizedApplicationBar();
            InitializeSettings();
            ShowIP();
            //ReSizeButton();
            isLoaded = true;

//-------------Admob Google+---------------------------------------
           

          

         //  

            #region Application Bar

            //ApplicationBar aB = new ApplicationBar();
            //aB.IsMenuEnabled = true;
            //aB.IsVisible = true;

            //iconButtonStart = new ApplicationBarIconButton(new Uri("/images/start.png", UriKind.Relative));
            //iconButtonStart.Text = "Start";
            //iconButtonStart.Click += new EventHandler(iconButtonStart_Click);
            //iconButtonStart.IsEnabled = false;
            //aB.Buttons.Add(iconButtonStart);

            //iconButtonStop = new ApplicationBarIconButton(new Uri("/images/stopp.png", UriKind.Relative));
            //iconButtonStop.Text = "Stop";
            //iconButtonStop.Click += new EventHandler(iconButtonStop_Click);
            //iconButtonStop.IsEnabled = true;
            //aB.Buttons.Add(iconButtonStop);


            //this.ApplicationBar = aB;

            #endregion
            
         //   ShowAds();
           
                       
            //---------------------------------
            interstitialAd = new InterstitialAd(MY_AD_INTERS_UNIT_ID);
            AdRequest adRequest2 = new AdRequest();

            interstitialAd.ReceivedAd += OnAdReceived;
            interstitialAd.FailedToReceiveAd += OnFailedToReceiveAd;
            interstitialAd.DismissingOverlay += OnDismissingOverlay;
           
           // adRequest2.ForceTesting = true; //TESTING!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            interstitialAd.LoadAd(adRequest2);        
        }

       

        private void ShowAds()
        {
            if (bannerAd == null)
            {
                bannerAd = new AdView
                {
                    Format = AdFormats.Banner, //Banner
                    AdUnitID = MY_AD_UNIT_ID
                };
            }

            AdRequest adRequest = new AdRequest();

         //   adRequest.ForceTesting = true;//TESTING!!!!!!!!!!!!!!!!!!!!!!!!!!

            switch (pivot.SelectedIndex)
            {
                case 0:
                    {                      
                        ads1.Children.Add(bannerAd);
                        break;
                    }
                case 1:
                    {
                        ads2.Children.Add(bannerAd);                        
                        break;
                    }
                case 2:
                    {
                        ads3.Children.Add(bannerAd);
                        break;
                    }
                case 3:
                    {
                        ads4.Children.Add(bannerAd);
                        break;
                    }
                default:
                    {
                        ads1.Children.Add(bannerAd);
                        break;
                    }
            }

            bannerAd.LoadAd(adRequest);       
        }


        private void ClearAds()
        { 
            if (bannerAd == null)
                return;

            switch (pivot.SelectedIndex)
            {
                case 0:
                    {                        
                        ads1.Children.Remove(bannerAd);
                        break;
                    }
                case 1:
                    {
                        ads2.Children.Remove(bannerAd);
                        break;
                    }
                case 2:
                    {
                        ads3.Children.Remove(bannerAd);
                        break;
                    }
                case 3:
                    {
                        ads4.Children.Remove(bannerAd);
                        break;
                    }
                default:
                    {

                        ads1.Children.Remove(bannerAd);
                        break;
                    }
            }
        }

        void OnAdReceived(object sender, AdEventArgs e)
        {
           //
            InterAdsIsLoaded = true;
        }

        private void OnDismissingOverlay(object sender, AdEventArgs e)
        {
           // Debug.WriteLine("Dismissing interstitial.");
            InterAdsIsLoaded = false;
         
        }

        private void OnFailedToReceiveAd(object sender, AdErrorEventArgs errorCode)
        {
           // Debug.WriteLine("Failed to receive interstitial with error " + errorCode.ErrorCode);         
            InterAdsIsLoaded = false;
        }
      


       

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            //if (MessageBox.Show("Do you want to exit PULT?", "Application Closing", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            //{
            //    // Cancel default navigation
            //    e.Cancel = true;                 

            //}            
            if (InterAdsIsLoaded)
                interstitialAd.ShowAd();


            e.Cancel = false; 
        }

        //void iconButtonStop_Click(object sender, EventArgs e)
        //{
        //    somaAdViewer.StopAds();
        //    iconButtonStart.IsEnabled = true;
        //    iconButtonStop.IsEnabled = false;
        //}

        //void iconButtonStart_Click(object sender, EventArgs e)
        //{
        //    somaAdViewer.StartAds();
        //    iconButtonStart.IsEnabled = false;
        //    iconButtonStop.IsEnabled = true;
        //}


        VibrateController vibro = VibrateController.Default;

        int DEF_VIDEO_INDEX = 0;
        int DEF_MUSIC_INDEX = 1;
        int DEF_PHOTO_INDEX = 2;
        int DEF_BEHOLDTV_INDEX = 3;
        int DEF_SETTINGS_INDEX = 4;

        String port = "22";
        string portudp = "4568";
        String ipBrdcst = "255.255.255.255";
        String localIP = "192.168.1.1";
        String versionprogram = translate("versionprogram");
        String key = "empty";

        string usernameftp = "qwe#sdf6AsdfgzTs";

        #region Key Codes
        int nowtime = 42;//вывод на экран круглого времени
        int lockscr = 1100;       

        int vol7 = 60;
        int vol18 = 54;
        int vol25 = 55;
        int vol33 = 56;
        int vol55 = 57;
        int vol77 = 58;
        int vol99 = 59;

        int mute = 1;
        int volup = 2;
        int voldown = 3;

        int videosearchStart = 2000;
        int audiosearchStart = 5000;
        int photosearchStart = 7000;
        //------------------------------------videoPLAYER - new menu
        int VIDEOvu = 4000;
        int VIDEOvd = 4001;
        int VIDEOff = 4002;
        int VIDEOnext = 4003;
        int VIDEOrw = 4004;
        int VIDEOprev = 4005;
        int VIDEOmute = 4006;
        int VIDEOfull = 4007;
        int VIDEOplay = 4008;
        int killAllApps = 62;
        //-------------------------------------------------audioPlayer - new menu

        int aVOLUP = 4009;
        int aVOLDWN = 4010;
        int aSTOP = 4011;
        int aNEXT = 4012;
        int aRESTORE = 4013;
        int aPREV = 4014;
        int aMUTE = 4015;
        int aPAUSE = 4016;
        int aPLAY = 4017;
        int aKill = 4028;
        //-------------------------------------------------PhotoViewver - new menu
                
        int pZoomIn = 4018;
        int pZoomOut = 4019;
        int pPREVFolder = 4020;
        int pPrev = 4021;
        int pPAUSESlideShow = 4022;
        int pNEXT = 4023;
        int pNEXTFolder = 4024;
        int pFullScreen = 4025;
        int pShowClose = 4026;
        const int pDelete = 4030; //!!!!!!!!!!!!!!!!!!! изменился!!!!!!

        //-------------------------------BEHOLDER        
        const int bchup = 900;
        const int bvoldown = 901;
        const int bfullscreen = 902;
        const int bvolup = 903;
        const int bchdown = 904;
        const int bmute = 905;

        const int bsleep = 906;
        const int bosd = 907;

      
        const int BEHOLDERstartApp = 920;


        const int setAuto = 11111;//!!!!!!!!!!!!!!!!!!! new!!!!!!!!!!!!!!!
        const int setVideoLAN = 11112;
        const int setAcePlayerHD = 11113;
        const int setBSPlayer = 11114;
        const int setLightAlloy = 11115;
        const int setPotPlayer = 11116;
        const int setMediaPlayerClassic = 11117;
        const int setGomPlayer = 11118;
        const int setJetAudio = 11119;
        const int setWindowsMediaPlayer = 11120;

        const int changemonit = 11121;
        const int changeplayer = 11122;

        bool connectionsOK = false;
 
        #endregion
        myNetworkInterface ni = null;
       // FtpClient ftpClient = null;
        public bool isPlaylist = false;


     

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            if (State.ContainsKey("pivotIndex"))
                pivot.SelectedIndex = (int)State["pivotIndex"];
            isPlaylist = false;
            versionprogram = translate("versionprogram");
            if (localIP == "192.168.1.1" && IsWifiConnected())
            {
                localIP = Find();
            }
        }    

        protected  override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            State["pivotIndex"] = pivot.SelectedIndex;
            SaveSetting();
            if (!isPlaylist)
                myFTP.Disconnect();
        }

        private void ShowIP()
        {
            if (textblockIP  == null ||
                textblockIP2.Content == null ||
                textblockIP3.Content == null ||
                textblockIP4.Content == null)
                return;

            textblockIP.Content =
                textblockIP2.Content =
                textblockIP3.Content =
                textblockIP4.Content =
                 GetSSIDName() +
                "\n " + GetTypeSever()
               + "\n " + GetIpServer();
        }

        private string GetTypeSever()
        {
            if (radiobut_udp == null)
                return String.Empty;

            if ((bool)radiobut_udp.IsChecked)
                return "UDP (win7)";
            else
                return "FTP (win8)";
        }

        private string GetIpServer()
        {
            if (cb_ip_all == null || radiobut_ftp == null)
                return String.Empty;

            if ((bool)cb_ip_all.IsChecked && !(bool)radiobut_ftp.IsChecked)
                return "All PC";
            else
                return TextBoxIp.Text;
        }

        private void InitializeSettings()
        {
            bool needSaveDafault = false;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            const string isFTP = "isFTP";
            const string goa = "backgoa";
            const string minsk = "backminsk";
            const string dubai = "backdubai";
            const string stambul = "backstambul";
            const string cat = "backcat";
            
            const string ip = "TextBoxIp";
            const string mac = "TextBoxMac";
            const string code = "TextBoxCode";
            const string ipall = "ipall";
            const string pivoteSelectedIndex = "pivotIndex";
            const string vibro = "vibro";
            const string isHibernate = "isHibernate";
            //const string vollock = "vollock";
            
            

            if (!settings.Contains(isFTP))
            {
                settings.Add(isFTP, false);
                needSaveDafault = true;                
            }

            if (!settings.Contains(goa))
            {
                settings.Add(goa, false);
                needSaveDafault = true;                                
            }

            if (!settings.Contains(minsk))
            {
                settings.Add(minsk, false);
                needSaveDafault = true;                                
            }

            if (!settings.Contains(dubai))
            {
                settings.Add(dubai, false);
                needSaveDafault = true;
            }

            if (!settings.Contains(stambul))
            {
                settings.Add(stambul, false);
                needSaveDafault = true;
            }

            if (!settings.Contains(cat))
            {
                settings.Add(cat, false);
                needSaveDafault = true;
            }

            if (!settings.Contains(ip))
            {
                settings.Add(ip, "192.168.1.0");
                needSaveDafault = true;
            }

            if (!settings.Contains(mac))
            {
                settings.Add(mac, "00:00:00:00:00:00");
                needSaveDafault = true;
            }

            if (!settings.Contains(code))
            {
                settings.Add(code, "19216810");
                needSaveDafault = true;
            }

            if (!settings.Contains(ipall))
            {
                settings.Add(ipall, true);
                needSaveDafault = true;
            }

            if (!settings.Contains(pivoteSelectedIndex))
            {
                settings.Add(pivoteSelectedIndex, "5");
                needSaveDafault = true;
            }

            if (!settings.Contains(vibro))
            {
                settings.Add(vibro, true);
                needSaveDafault = true;
            }

            if (!settings.Contains(isHibernate))
            {
                settings.Add(isHibernate, false);
                needSaveDafault = true;                
            }

            

            //if (!settings.Contains(vollock))
            //{
            //    settings.Add(vollock, false);
            //    needSaveDafault = true;
            //}

            

            if (needSaveDafault)
                settings.Save();

            TextBoxCode.Text = settings[code].ToString();
            TextBoxMac.Text = settings[mac].ToString();
            TextBoxIp.Text = settings[ip].ToString();
            radiobut_backgoa.IsChecked = (bool)settings[goa];
            radiobut_backminsk.IsChecked = (bool)settings[minsk];
            radiobut_backstambul.IsChecked = (bool)settings[stambul];
            radiobut_dubai.IsChecked = (bool)settings[dubai];
            radiobut_backcat.IsChecked = (bool)settings[cat];

            if ((bool)radiobut_tema.IsChecked)
                showDefaultTopic();

            radiobut_ftp.IsChecked = (bool)settings[isFTP];
            cb_ip_all.IsChecked = (bool)settings[ipall];
            cb_vibro.IsChecked = (bool)settings[vibro];
           // cb_vol_lock.IsChecked = (bool)settings[vollock];

            if (!(bool)radiobut_ftp.IsChecked && (bool)cb_ip_all.IsChecked)
            {
                tb_ip.Visibility =
                   TextBoxIp.Visibility = System.Windows.Visibility.Collapsed;
            }

            if ((bool)radiobut_ftp.IsChecked)
            {
                cb_ip_all.Visibility = System.Windows.Visibility.Collapsed;
            }

            int selectedInd = 0;
            if (!Int32.TryParse(settings[pivoteSelectedIndex].ToString(), out selectedInd))
            {
                selectedInd = 5;
            }
            pivot.SelectedIndex = selectedInd;

            if (!IsDarkTheme())
            { 
                textblockIP2.Background = null;
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"images\02_network_status2.screen-wxga.png", UriKind.Relative)).Stream);
                ib.ImageSource = tn;
                ib2.ImageSource = tn;
                ib3.ImageSource = tn;
                ib4.ImageSource = tn;
            }
        }

        private void PowerMenu()
        {
            int t = 0;
            //switch (t)
            //{
            //    case POWER_ON:
            //        {
            //            powerON();
            //            break;
            //        }
            //    case POWER_OFF:
            //        {
            //            operate(poweroff);
            //            break;
            //        }
            //    case POWER_REBOOT:
            //        {
            //            operate(reboot);
            //            break;
            //        }
            //    case POWER_SHUTDOWN:
            //        {
            //            operate(shutdown);
            //            break;
            //        }

            //    case POWER_hibernate:
            //        {
            //            operate(hibernate);
            //            break;
            //        }
            //    case POWER_Standby:
            //        {
            //            operate(standby);
            //            break;
            //        }

            //    case POWER_Lockscreen:
            //        {
            //            operate(lockscr);
            //            break;
            //        }
            //}
        }

        // Загрузка данных для элементов ViewModel

#region FTP ------------------------------------------------------------------------------------------------------------------------------

        //private async void sendFtp(int code, String key) {

        //    
        //    try
        //    {
        //        if (ftpClient == null)
        //            ftpClient = new FtpClient(TextBoxIp.Text, port, this.Dispatcher);
        //        //ftpClient.FtpAuthenticationSucceeded += ftpClient_FtpAuthenticationSucceeded;
        //        //ftpClient.FtpAuthenticationFailed += ftpClient_FtpAuthenticationFailed;
        //        //await 
        //        if (ftpClient != null && !ftpClient.IsConnected)
        //        {
        //            await ftpClient.ConnectAsync();
        //            if (!ftpClient.IsConnected)
        //            {
        //                MessageBox.Show("Ошибка подключения к серверу!", "неверный IP адрес сервера или на ПК не запущен RC SERVER!", MessageBoxButton.OK);
        //                pivot.SelectedIndex = DEF_SETTINGS_INDEX;
        //                ftpClient = null;
        //                return;
        //            }
        //            await ftpClient.AuthenticateAsync(usernameftp, TextBoxCode.Text);
        //        }

        //        // SystemTray.ProgressIndicator = new ProgressIndicator() { IsIndeterminate = true, Text = "Authenticating . . ." };
        //        //await 

        //        if (ftpClient.IsBusy)
        //            Thread.Sleep(500);
        //        await ftpClient.SendCommandAsync(String.Format("{0}|{1}|{2}|{3}|{4}|{5}", code, TextBoxIp.Text, localIP, key, versionprogram, TextBoxIp.Text));
        //        //await ftpClient.DisconnectAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


#endregion

        public static String Find()
        {
            List<string> ipAddresses = new List<string>();
            IPAddress address;
            try
            {
                var hostnames = NetworkInformation.GetHostNames();
                foreach (var hn in hostnames)
                {
                    if (hn.IPInformation != null)
                    {
                        string ipAddress = hn.DisplayName;
                        ipAddresses.Add(ipAddress);
                    }
                }

                address = IPAddress.Parse(ipAddresses[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "192.168.1.1";
            }

            return address.ToString();
        }

        int qnt = 1;      
        bool adsshowed = false;

        private async Task SendCode(int code)
        {
            if (qnt % 5 == 0)
            {                
                    ShowAds();                
            }
            else            
                    ClearAds();

            if (qnt == 1000)
                qnt = 0;
                
            qnt++;

            if (code == -1 || radiobut_ftp == null)
                return;

            if (localIP == "192.168.1.1")
            {
                localIP = Find();
            }
            // MessageBox.Show(code.ToString()); 
            if (vibro != null && (bool)cb_vibro.IsChecked)
            {
                vibro.Start(TimeSpan.FromSeconds(0.05));
              //  vibro.Stop();
            }
            
            try
            {
                if ((bool)radiobut_ftp.IsChecked)
                {
                    //http://msdn.microsoft.com/ru-ru/magazine/dn385710.aspx

                    if (TextBoxIp.Text == "192.168.1.0")
                    {

                        MessageBox.Show(translate("noip"));                       
                        
                        pivot.SelectedIndex = DEF_SETTINGS_INDEX;
                        return;
                    }
                   // sendFtp(code, "empty"); 
                    await myFTP.sendFtp(code, "empty", TextBoxIp.Text, port, this.Dispatcher, versionprogram, localIP, TextBoxCode.Text);
                    if (!myFTP.isFtpOk() && code != killAllApps && code != BEHOLDERstartApp && code != pShowClose)
                        pivot.SelectedIndex = DEF_SETTINGS_INDEX;
                }
                else //UDP
                {
                    //http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202864(v=vs.105).aspx#BKMK_CreatingtheUDPSocketClientUI
                    //http://10rem.net/blog/2012/06/23/using-udp-sockets-to-connect-a-windows-8-metro-style-app-to-a-net-micro-framework-device-part-3                    

                      
                      String ip = "";
                      if ((bool)cb_ip_all.IsChecked)                      
                          ip = ipBrdcst;
                      else
                          ip = TextBoxIp.Text;
                      


                        //IPHostEntry localHostEntry = Dns.GetHostByName(Dns.GetHostName());
                        //sampleTcpUdpClient2 client = new sampleTcpUdpClient2(sampleTcpUdpClient2.clientType.UDP);
                        //new IPEndPoint(IPAddress.Broadcast, 9050)
                        //client.sampleUdpClientServerName(TextBoxIp.Text, String.Format("#{0}#", code), portudp);
                      String fullcode = code + "|"
                + TextBoxCode.Text + "|"
                    + localIP + "|"
                    + key + "|"
                    + versionprogram + "|"
                    + TextBoxIp.Text;//  1}|{2}|{3}|{4}|{5}", , textBox_code.Text, listIP[0], key, versionprogram, textBox_ip.Text), portudp);  
                      
                      fullcode = fullcode + "|#";//add "|#" for not wait answer from server!


                    #region old UDP
                      //try
                      //{
                      //    if (ni == null)
                      //    {
                      //        ni = new myNetworkInterface();
                      //    }
                      //    ni.Connect(new HostName(ip), portudp);
                      //    ni.SendMessage(fullcode);
                      //}
                    #endregion

                      try
                      {
                          if (ni == null)
                          {
                              ni = new myNetworkInterface();
                          }                       
                        await  ni.SendMessage(fullcode, new HostName(ip), portudp);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }        
                }
            }
            catch (Exception er)
            {
                if (code != killAllApps && code != BEHOLDERstartApp && code !=  pShowClose)
                { 
                    connectionsOK = false;
                    MessageBox.Show(er.Message);
                }
            }
        }

     
              
#region Нажатие кнопок
        private void but_mute_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
          
          //  cb_vol_lock.IsChecked = false;
            SendCode(mute);
        }

        private void but_power_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            isPlaylist = true;
            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";

            SendCode(killAllApps);
            NavigationService.Navigate(new Uri(String.Format("/PowerMenu.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}&mac={4}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text, TextBoxMac.Text), UriKind.Relative));
        }

        private void but_lock_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(lockscr);
        }

        private void but_volup_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(VIDEOvu);
        }

        private async void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
          //  Thread.Sleep(100);
            if (!IsConnectOK())
                return;
            
            //7 18 25 33 55 77 99
            //0 2  3  5  7  8  10
            int val = (int)e.NewValue;
            if (val == 4 || !isLoaded)
                return;
            if (e.NewValue > e.OldValue)
            {
                //up             
             if (val == 9)              
                 await SendCode(vol77);
             else
                 if (val == 10)                 
                     await SendCode(vol99);                       
                 else
                     await SendCode(volup);
            }
            else
            {
                //down
                //7 18 25 33 55 77 99
                //0 2  3  5  7  8  10
            
             if (val == 0)
             {
                 await SendCode(vol7);
                 
                 //   slider3.Value 
                    //=  slider.Value 
                // = 0;
                 return;
             }                 
             else
                 if (val == 1)                 
                     await SendCode(vol18);                          
                 else
                     await SendCode(voldown);
            }
                //int volup = 2;
                //  int voldown = 3;
          
            //    slider.Value = 4;
                
        }

        private void but_play_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(VIDEOplay);
        }

        private void but_voldwn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(VIDEOvd);
        }

        private void but_playlst_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            // SendCode(videosearchStart);        
            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";
            isPlaylist = true;
            NavigationService.Navigate(new Uri(String.Format("/videosearch.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text), UriKind.Relative));
       
        }

        private void but_timer_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            isPlaylist = true;
                  string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";
            NavigationService.Navigate(new Uri(String.Format("/TimerMenu.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text), UriKind.Relative));

        }

        
        private void but_vlcmute_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;

            SendCode(VIDEOmute);                       
        }

     
        private void but_rew_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(VIDEOrw);
        }

        private void but_kill_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(killAllApps);
        }

        private void but_ff_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(VIDEOff);
        }

      

        private void but_time_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(nowtime);
        }

        private void but_volup2_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aVOLUP);
        }

        private void but_prev2_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aPREV);
        }

        private void but_stop_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aSTOP);
        }

        private void but_playmusic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aPLAY);
        }

        private void but_pause_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aPAUSE);
        }

        private void but_nextmusic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aNEXT);
        }

        private void but_voldwnmusic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aVOLDWN);
        }

        private void but_vlcmutemusic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aMUTE);
        }

        private void but_playlstmusic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";
            isPlaylist = true;
            NavigationService.Navigate(new Uri(String.Format("/musicsearch.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text), UriKind.Relative));
       
        }

        private void but_fullscreenp_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(pFullScreen);
        }

        private void but_photoclose_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;

            if (deletedfile)
            {
                deletedfile = false;
            }
            else
            {
                SendCode(pShowClose);
            }
                
                //MessageBox.Show("but_photoclose_Click");
        }

        private void but_fullscreenv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(VIDEOfull);
        }

        private void but_fullscreenm_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(aRESTORE);
        }

        private void but_playlstphoto_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";
            isPlaylist = true;
            NavigationService.Navigate(new Uri(String.Format("/photosearch.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text), UriKind.Relative));
        }

        private void but_zoomin_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(pZoomIn);
        }

        private void but_prevfolderpic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;

          //  SendCode(pShowClose);
          //  Thread.Sleep(1000);
            SendCode(pPREVFolder);
        }

        private void but_prevpic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(pPrev);
        }

        private void but_picslideshow_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(pPAUSESlideShow);
        }

        private void but_nextpic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(pNEXT);
        }

        private void but_deletep_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            if (MessageBox.Show(translate("deletephoto"), translate("delfile"), MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                SendCode(pDelete);
        }

        private void but_nextfolderpic_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
          //   SendCode(pShowClose);
          //   Thread.Sleep(1000);
             SendCode(pNEXTFolder);
        }

        private void but_zoomout_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
             SendCode(pZoomOut);
        }

        private void but_voluptv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(bvolup);
        }

        private void but_voldwntv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(bvoldown);
        }

        private void but_fulloktv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(bfullscreen);          
        }   
        
        private void but_vlcmutetv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(bmute);
        } 
        
        private void but_channeltv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;

            SendCode(bosd);

           }

       
        
        private void but_chdwntv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(bchdown);
        } 
        
        private void but_chuptv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(bchup);
        }


        private void but_starttv_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(BEHOLDERstartApp);
        }


#endregion

       
#region Настройки
        private void radiobut_backgoa_Checked(object sender, RoutedEventArgs e)
        {            
            if (background != null)
            {
                background.Source = null;
                BitmapImage tn = new BitmapImage();
                tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backgoa.jpg", UriKind.Relative)).Stream);
                background.Source = tn;
            }
        }

        private void ReSizeButton()
        {
            if (but_play != null)
            {
                switch (ResolutionHelper.CurrentResolution)
                {                                      
                        
                    case Resolutions.WXGA:
                        {
                            if (but_play != null)
                            {
                                but_play.Width =
                            but_play.Height = 320;
                            }
                            if (but_rew != null)
                            {
                                but_rew.Width = 224;
                            but_rew.Height = 320;
                            }
                            if (but_ff != null)
                            {
                                but_ff.Width = 224;
                                but_ff.Height = 320;
                            }
                            
                            break;
                        }                        
                        
                    case Resolutions.WVGA:
                        {
                            if (but_play != null)
                            {
                                but_play.Width =
                            but_play.Height = 200;
                                but_play.UpdateLayout();
                            }
                            if (but_rew != null)
                            {
                                but_rew.Width = 140;
                                but_rew.Height = 200;
                                but_rew.UpdateLayout();
                            }
                            if (but_ff != null)
                            {
                                but_ff.Width = 140;
                                but_ff.Height = 200;
                                but_ff.UpdateLayout();
                            }
                            break;
                        }                                                                
                }

                
                
            }    
            //ImageBrush bg = new ImageBrush();
            //if (mi == null)
            //{
            //    mi = new MultiResImageChooserUri();
            //}
            //bg.ImageSource = new System.Windows.Media.Imaging.BitmapImage(mi.BestResolutionImage_Mute_pc);


            //if (but_mute != null && bg != null)
            //{
            //    but_mute.Background = bg;
            //}   
            //TODO: добавить все кнопки!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
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

        private void showDefaultTopic()
        {
            
            if (background != null)
            {
                background.Source = null;
                BitmapImage tn = new BitmapImage();
                // tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/black.jpg", UriKind.Relative)).Stream);
                //tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/bg.jpg", UriKind.Relative)).Stream);

                //if (mi == null)
                //{
                //  mi = new MultiResImageChooserUri();
                //}

               // tn.SetSource(Application.GetResourceStream(mi.BestResolutionImageBG).Stream);                
                if (IsDarkTheme())
                    tn.SetSource(Application.GetResourceStream(new Uri("Assets/bg.screen-wvga.jpg", UriKind.Relative)).Stream);                
                else
                    tn.SetSource(Application.GetResourceStream(new Uri("Assets/bg2.screen-wvga.jpg", UriKind.Relative)).Stream);                
                
                background.Source = tn;

            }
            
            /* ------------------- LOAD IMG FOR BUTTON----------------------------------------------------------------
            ImageBrush bg = new ImageBrush();            
            bg.ImageSource = new System.Windows.Media.Imaging.BitmapImage(new Uri(@"Assets/11_play.png", UriKind.Relative));                     


            if (but_play != null && bg != null )
            {
                but_play.Width =
                but_play.Height  = 160;
                but_play.Background = bg;
            }            
           */
        }


        private void radiobut_topic_Checked(object sender, RoutedEventArgs e)
        {
            showDefaultTopic();
         }

        private void radiobut_backcat_Checked(object sender, RoutedEventArgs e)
        {
            if (background != null)
            {
                background.Source = null;
                BitmapImage tn = new BitmapImage();
                if (IsDarkTheme())
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backblack.jpg", UriKind.Relative)).Stream);
                else
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backblack2.jpg", UriKind.Relative)).Stream);
                background.Source = tn;
                
            }   
           
         }

        private void radiobut_backminsk_Checked(object sender, RoutedEventArgs e)
        {
            if (background != null)
            {
                background.Source = null;
                BitmapImage tn = new BitmapImage();
                if (IsDarkTheme())
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backminsk.jpg", UriKind.Relative)).Stream);
                else
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backminsk2.jpg", UriKind.Relative)).Stream);
                background.Source = tn;
            }
        }
#endregion

#region WIFI      

        private string GetSSIDName()
        {           
            if (IsWifiConnected())
            {             
                foreach (var network in new NetworkInterfaceList())
                {
                    if (
                        (network.InterfaceType == NetworkInterfaceType.Wireless80211) &&
                        (network.InterfaceState == ConnectState.Connected)
                        )
                    {
                        return network.InterfaceName;
                    }
                }
            }
            return "Wi-Fi Off";
        }

        private bool IsConnectOK()
        {
            if (!isLoaded)
                return true;
            if (connectionsOK)
                return true;
            //1 1 1(ok)
            //0 0 0(wifi phone off)
            //0 1 0 (router is off)
            //1 1 0 (router OK but without internet)
            bool WifiConnected = IsWifiConnected();
            if (WifiConnected)
            {
                connectionsOK = true;
                return true;
            }
                

            bool NetworkAvailable = IsNetworkAvailable();
            bool WifiEnabled = IsWifiEnabled();
            

            if (NetworkAvailable && WifiEnabled && WifiConnected)
                return true;
            else
                if (!NetworkAvailable && !WifiEnabled && !WifiConnected)
                    MessageBox.Show(translate("wifiofferror"));            
                else
                    if (!NetworkAvailable && WifiEnabled && !WifiConnected)
                        MessageBox.Show(translate("wifinoconnect"));
                    else
                        if (NetworkAvailable && WifiEnabled && !WifiConnected)
                            MessageBox.Show(translate("wifinointernet"));
                        else
                            MessageBox.Show(translate("networkerror"));
                
            return false;
        }

        private bool IsWifiConnected()
        {
            if (IsNetworkAvailable() && IsWifiEnabled())
            {
              //  return NetworkInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211;
                return true;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            }

            return false;
        }

        private bool IsWifiEnabled()
        {
            return DeviceNetworkInformation.IsWiFiEnabled;
        }

        private bool IsNetworkAvailable()
        {
           // return true;
            return DeviceNetworkInformation.IsNetworkAvailable;
        }
        #endregion

        #region buttons EVENTs
        private void radiobut_ftp_Checked(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
                return;

            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings["isFTP"] = true;
            settings.Save();
            cb_ip_all.Visibility = System.Windows.Visibility.Collapsed;
            if (isLoaded) 
                ShowIP();
                tb_ip.Visibility =
                   TextBoxIp.Visibility = System.Windows.Visibility.Visible;            
        }

        private void radiobut_ftp_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!isLoaded)
                return;
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings["isFTP"] = false;
            settings.Save();
            cb_ip_all.Visibility = System.Windows.Visibility.Visible;
            if (isLoaded) 
                ShowIP();
            if (!(bool)radiobut_ftp.IsChecked && (bool)cb_ip_all.IsChecked)
            {
                tb_ip.Visibility =
                   TextBoxIp.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

      

        private void SaveSetting()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings["backgoa"] = radiobut_backgoa.IsChecked;
            settings["backminsk"] = radiobut_backminsk.IsChecked;

            settings["backdubai"] = radiobut_dubai.IsChecked;
            settings["backstambul"] = radiobut_backstambul.IsChecked;
            settings["backcat"] = radiobut_backcat.IsChecked;
                        
            settings["TextBoxIp"] = TextBoxIp.Text;
            settings["ipall"] = cb_ip_all.IsChecked;
            settings["vibro"] = cb_vibro.IsChecked;

           // settings["vollock"] = cb_vol_lock.IsChecked;
            
            
         
            if (pivot.SelectedIndex == DEF_VIDEO_INDEX || pivot.SelectedIndex == DEF_MUSIC_INDEX || pivot.SelectedIndex == DEF_PHOTO_INDEX || pivot.SelectedIndex == DEF_BEHOLDTV_INDEX) //видео/аудио/фото/тв
                settings["pivotIndex"] = pivot.SelectedIndex;

            if (!IsValidIP(TextBoxIp.Text))
            {
                MessageBox.Show(translate("ipnotcorret"));
                TextBoxIp.Focus();
                return;
            }           

            settings["TextBoxCode"] = TextBoxCode.Text;
            settings["TextBoxMac"] = TextBoxMac.Text;

            if (!IsValidMAC(TextBoxMac.Text))
                {
                    MessageBox.Show(translate("macnotcorrect"));
                    TextBoxMac.Focus();
                    return;
                }
            settings.Save();            
        }
      
        /// <summary>
        /// method to validate an IP address
        /// using regular expressions. The pattern
        /// being used will validate an ip address
        /// with the range of 1.0.0.0 to 255.255.255.255
        /// </summary>
        /// <param name="addr">Address to validate</param>
        /// <returns></returns>
        private bool IsValidIP(string addr)
        {
            
            IPAddress ip;
            return  IPAddress.TryParse(addr, out ip);
            
            /*
            bool valid = false;
            //create our match pattern
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            //create our Regular Expression object
            Regex check = new Regex(pattern);
            //boolean variable to hold the status
         
            //check to make sure an ip address was provided
            if (addr == "")
            {
                //no address provided so return false
                valid = false;
            }
            else
            {
                //address provided so use the IsMatch Method
                //of the Regular Expression object
                valid = check.IsMatch(addr, 0);
            }
            //return the results
            return valid;
             * */
        }

        private bool IsValidMAC(string addr)
        {
            //create our match pattern
            string pattern = "^([0-9A-F]{2}[:-]){5}([0-9A-F]{2})$";    
            
            Regex check = new Regex(pattern);            
            bool valid = false;
            
            if (addr == "")
            {             
                valid = false;
            }
            else
            {            
                valid = check.IsMatch(addr, 0);
            }
            
            return valid;
        }
            

        private void cb_code_auto_Checked(object sender, RoutedEventArgs e)
        {
            if (TextBoxCode != null)
                TextBoxCode.IsReadOnly = true;
          
        }

        private void cb_code_auto_Unchecked(object sender, RoutedEventArgs e)
        {
            if (TextBoxCode != null)
                TextBoxCode.IsReadOnly = false;
        }
        
        private void TextBoxIp_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isLoaded && cb_code_auto != null && (bool)cb_code_auto.IsChecked)
                TextBoxCode.Text = TextBoxIp.Text.Replace(".", string.Empty);


            if (isLoaded && (bool)radiobut_ftp.IsChecked)
            {
                myFTP.Disconnect();
                ShowIP();
            }                
        }

  #endregion

        private void radiobut_backstambul_Checked(object sender, RoutedEventArgs e)
        {
            if (background != null)
            {
                background.Source = null;
                BitmapImage tn = new BitmapImage();
                if (IsDarkTheme())
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backstambul.jpg", UriKind.Relative)).Stream);
                else
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backstambul2.jpg", UriKind.Relative)).Stream);
                background.Source = tn;
            }
        }

        private void radiobut_dubai_Checked(object sender, RoutedEventArgs e)
        {
            if (background != null)
            {
                background.Source = null;
                BitmapImage tn = new BitmapImage();
                if (IsDarkTheme())
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backdubai.jpg", UriKind.Relative)).Stream);
                else
                    tn.SetSource(Application.GetResourceStream(new Uri(@"Assets/backdubai2.jpg", UriKind.Relative)).Stream);
                background.Source = tn;
            }
        }

        //private void Slider_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        //{
            
        //        //slider3.IsEnabled =
        //    //slider.IsEnabled =             false;
        //   // cb_vol_lock.IsChecked = true;
        //    MessageBox.Show(translate("volmasteroff"));
        //}

        private void tb_about2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //MessageBox.Show(GetCulture().ToString());
            //загрузить инструкцию с картинками

           // MessageBox.Show(translate("ApplicationTitle"));

            try
            {

                if (GetCulture() == 0)// en
                {
                    WebBrowserTask wbtask = new WebBrowserTask();
                    wbtask.URL = "http://alexsoft.in/readmenglish";
                    wbtask.Show();
                }
                if (GetCulture() == 1 || GetCulture() == 2)// ru - be
                {

                    WebBrowserTask wbtask = new WebBrowserTask();
                    wbtask.URL = "http://alexsoft.in/readme";
                    wbtask.Show();
                }
            }
            catch (Exception s)
            { 

            }
        }

        //get lang string resource
        public static string translate(string messageCode)
        {
            PropertyInfo info = typeof(AppResources)
                .GetProperty(
                    messageCode,
                    BindingFlags.Public | BindingFlags.Static
                )
            ;
            return info != null
                ? info.GetValue(null, null) as string
                : messageCode
            ;
        }

        private int GetCulture()
        {          
            /*0 en
              1 ru
              2 by 
             * */
            string lang = Thread.CurrentThread.CurrentCulture.Name;
            if (lang == null)
                return 0;
              
            if (lang.Contains("ru-RU"))
                return 1;
            if (lang.Contains("be-BY"))
                return 2;

            return 0; //if (lang.Contains("en"))
        }

        private async void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //  Thread.Sleep(100);
            if (!IsConnectOK())
                return;

            //7 18 25 33 55 77 99
            //0 2  3  5  7  8  10
            int val = (int)e.NewValue;
            if (val == 4 || !isLoaded)
                return;
            if (e.NewValue > e.OldValue)
            {
                //up             
                if (val == 9)
                    await SendCode(vol77);
                else
                    if (val == 10)
                        await SendCode(vol99);
                    else
                        await SendCode(volup);
            }
            else
            {
                //down
                //7 18 25 33 55 77 99
                //0 2  3  5  7  8  10

                if (val == 0)
                    await SendCode(vol7);
                else
                    if (val == 1)
                        await SendCode(vol18);
                    else
                        await SendCode(voldown);
            }
            //int volup = 2;
            //  int voldown = 3;
            //slider2.Value = 4;
        }

        private async void slider3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!IsConnectOK())
                return;

            //7 18 25 33 55 77 99
            //0 2  3  5  7  8  10
            int val = (int)e.NewValue;
            if (val == 4 || !isLoaded)
                return;
            if (e.NewValue > e.OldValue)
            {
                //up             
                if (val == 9)
                    await SendCode(vol77);
                else
                    if (val == 10)
                        await SendCode(vol99);
                    else
                        await SendCode(volup);
            }
            else
            {
                //down
                //7 18 25 33 55 77 99
                //0 2  3  5  7  8  10

                if (val == 0)
                    await SendCode(vol7);
                else
                    if (val == 1)
                        await SendCode(vol18);
                    else
                        await SendCode(voldown);
            }
            //int volup = 2;
            //  int voldown = 3;
            //slider3.Value = 4;
        }

        private void cb_vol_lock_Checked(object sender, RoutedEventArgs e)
        {
            //slider2.IsEnabled =
            //    slider3.IsEnabled =
            //slider.IsEnabled =             false;                        
        }

        private void cb_vol_lock_Unchecked(object sender, RoutedEventArgs e)
        {
           // slider2.IsEnabled =
            //    slider3.IsEnabled =
            //slider.IsEnabled =             true;                        
        }

        private void but_ff_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
                    
        }

        private void but_rew_DoubleTap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            

        }

        private void vol_percent_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            isPlaylist = true;
            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";
           
            NavigationService.Navigate(new Uri(String.Format("/MasterVolume.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}&mac={4}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text, TextBoxMac.Text), UriKind.Relative));
    
        }

        private void but_power2_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;

            SendCode(aKill);

            isPlaylist = true;
            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";
   
            NavigationService.Navigate(new Uri(String.Format("/PowerMenu.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}&mac={4}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text, TextBoxMac.Text), UriKind.Relative));
   
        }

        private void but_power3_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            isPlaylist = true;
            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";
            SendCode(pShowClose);
            NavigationService.Navigate(new Uri(String.Format("/PowerMenu.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}&mac={4}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text, TextBoxMac.Text), UriKind.Relative));
   
        }

        private bool deletedfile = false;

        private void pic_close_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!IsConnectOK())
                return;
            if (MessageBox.Show(translate("deletephoto"), translate("delfile"), MessageBoxButton.OKCancel) == MessageBoxResult.OK)            
            {                 
                deletedfile = true;
                SendCode(pDelete);
                //    MessageBox.Show("pic_close_Hold");
            }
                
        }

        private void but_TVpower_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            isPlaylist = true;
            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";
            if (tv_poweron)
            {
                tv_poweron = false;
            }
            else
            {
                SendCode(killAllApps);
                NavigationService.Navigate(new Uri(String.Format("/PowerMenu.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}&mac={4}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text, TextBoxMac.Text), UriKind.Relative));
            }
        }

      

        private bool tv_poweron = false;
        private string MY_AD_UNIT_ID = "ca-app-pub-2532664048077866/4878134636";
        private string MY_AD_INTERS_UNIT_ID = "ca-app-pub-2532664048077866/6354867830";

        private async void but_TVpower_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
             if (!IsConnectOK())
                return;

             tv_poweron = true;
             await SendCode(BEHOLDERstartApp);
             //Thread.Sleep(15500);
             //SendCode(bfullscreen);
        }


     
        private void but_playlst4_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }

        private void textblockIP_Click(object sender, RoutedEventArgs e)
        {
            pivot.SelectedIndex = 4;
        }

        private void cb_ip_all_Checked(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                ShowIP();
                if ((bool)radiobut_udp.IsChecked)
                {
                    tb_ip.Visibility =
                    TextBoxIp.Visibility = System.Windows.Visibility.Collapsed;

                }               
            }
                
        }

        private void cb_ip_all_Unchecked(object sender, RoutedEventArgs e)
        {
            if (isLoaded)
            {
                ShowIP();               
                    tb_ip.Visibility =
                    TextBoxIp.Visibility = System.Windows.Visibility.Visible;
            }   
        }

        private void but_channel_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;

            string rbftp = "";
            string ipall = "";
            if ((bool)radiobut_ftp.IsChecked)
                rbftp = "1";
            else
                rbftp = "0";

            if ((bool)cb_ip_all.IsChecked)
                ipall = "1";
            else
                ipall = "0";

            NavigationService.Navigate(new Uri(String.Format("/ChMenu.xaml?radiobutftp={0}&ip={1}&ipall={2}&code={3}", rbftp, TextBoxIp.Text, ipall, TextBoxCode.Text), UriKind.Relative));

       
            
        }

        private void Image_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            pivot.SelectedIndex = 0;
        }

        private void but_rew_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(VIDEOrw);
            SendCode(VIDEOrw);
        }

        private void but_ff_Hold(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(VIDEOff);
            SendCode(VIDEOff); 
        }

        private void but_prev_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(VIDEOprev);
        }

        private void but_next_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(VIDEOnext);
        }

        private void but_monitor_Click(object sender, RoutedEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(changemonit);
        }

        private void tb_player_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (!IsConnectOK())
                return;
            SendCode(changeplayer);
        }

            

      
      
        // Пример кода для сборки локализованной панели ApplicationBar
            //private void BuildLocalizedApplicationBar()
            //{
            //    // Установка в качестве ApplicationBar страницы нового экземпляра ApplicationBar.
            //    ApplicationBar = new ApplicationBar();

            //    // Создание новой кнопки и установка текстового значения равным локализованной строке из AppResources.
            //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
            //    appBarButton.Text = AppResources.AppBarButtonText;
            //    ApplicationBar.Buttons.Add(appBarButton);

            //    // Создание нового пункта меню с локализованной строкой из AppResources.
            //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
            //    ApplicationBar.MenuItems.Add(appBarMenuItem);
            //}


        
    }
}