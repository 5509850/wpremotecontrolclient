using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System.IO.IsolatedStorage;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using Windows.Networking;
using WinPhoneFtp.FtpService;
using System.Threading;
using System.Reflection;
using Windows.Networking.Connectivity;

namespace remotecontrolclient
{

    //вынести сюда статический класс и использовать его - при плейлистах не глушить соединение!!!!
   public static class myFTP
    {
       static FtpClient ftpClient = null;
       static string usernameftp = "qwe#sdf6AsdfgzTs";
       static DateTime lastsend = DateTime.Now;

       public static bool isFtpOk()
       { 
                if (ftpClient == null)
                   return false;               
              
                if (ftpClient != null && !ftpClient.IsConnected)
                {
                    return false; ;                                      
                }
                return true;
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

       public static IPAddress GetlocalIP()
       {
           List<string> ipAddresses = new List<string>();

           var hostnames = NetworkInformation.GetHostNames();
           foreach (var hn in hostnames)
           {
               if (hn.IPInformation != null)
               {
                   string ipAddress = hn.DisplayName;
                   ipAddresses.Add(ipAddress);
               }
           }

           IPAddress address = IPAddress.Parse(ipAddresses[0]);
           return address;
       }

       public async static void Disconnect()
       {
           if (ftpClient != null)
           {
               await ftpClient.DisconnectAsync();
               ftpClient = null;
           }
       }

       public async static Task sendFtp(int numcode, String key, String IP, String port, System.Windows.Threading.Dispatcher dispatcher, String versionprogram, String localIP, string code)
       {
           String mess = String.Empty;
           if ((DateTime.Now - lastsend).TotalMinutes > 1) //если больше минуты не было комманд
           {
               ftpClient = null;
           }
           
            try
            {
                if (ftpClient == null)
                    ftpClient = new FtpClient(IP, port, dispatcher);                
                //ftpClient.FtpAuthenticationSucceeded += ftpClient_FtpAuthenticationSucceeded;
                //ftpClient.FtpAuthenticationFailed += ftpClient_FtpAuthenticationFailed;
                //await 
                if (ftpClient != null && !ftpClient.IsConnected)
                {
                    await ftpClient.ConnectAsync();
                    if (!ftpClient.IsConnected)
                    {
                        MessageBox.Show("Ошибка подключения к серверу!", "неверный IP адрес сервера или на ПК не запущен RC SERVER!", MessageBoxButton.OK);
                   //     pivot.SelectedIndex = DEF_SETTINGS_INDEX;
                        ftpClient = null;
                        return;
                    }
                    await ftpClient.AuthenticateAsync(usernameftp, code);
                }
                    
                // SystemTray.ProgressIndicator = new ProgressIndicator() { IsIndeterminate = true, Text = "Authenticating . . ." };
                //await 
                
                if (ftpClient.IsBusy)
                    Thread.Sleep(500);
                await ftpClient.SendCommandAsync(String.Format("{0}|{1}|{2}|{3}|{4}|{5}", numcode, IP, localIP, key, versionprogram, IP));
                //await ftpClient.DisconnectAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           lastsend = DateTime.Now;
       }
    }
}
