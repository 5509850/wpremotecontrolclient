using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.Networking;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

//UDP!!!!!!!!!!!!!!!!!!!!!!!!!

namespace remotecontrolclient
{
    class myNetworkInterface
    {
         private DatagramSocket _socket = null;
         private String mess = "";

        

         public myNetworkInterface()
        {
             
            _socket = new DatagramSocket();
             
        }

       

        public String GetMess
         {
             get { return mess; }
             set { mess = value; }
         }
       
        private async void _socket_MessageReceived(DatagramSocket sender, DatagramSocketMessageReceivedEventArgs args)
        {

            var result = args.GetDataStream();
            var resultStream = result.AsStreamForRead(1024);

            using (var reader = new StreamReader(resultStream))
            {
                var text = await reader.ReadToEndAsync();
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    // Do what you need to with the resulting text
                    // Doesn't have to be a messagebox
                    mess = text;
                });
            }
            //TODO - подучить IP адрес сервера и MAC
        }

        //send text by UDP
        public async Task SendMessage(string message, HostName remoteHostName, string port)
        {
            if (_socket == null)
                _socket = new DatagramSocket();

         //   _socket.MessageReceived += _socket_MessageReceived;

            using (var stream = await _socket.GetOutputStreamAsync(remoteHostName, port))
            {
                using (var writer = new DataWriter(stream))
                {
                    var data = Encoding.UTF8.GetBytes(message);

                    writer.WriteBytes(data);
                    await writer.StoreAsync();
                }
            }
        }

        //poweron for byte[]
        public async Task SendMessage(byte[] data, HostName remoteHostName, string port)
        {
            if (_socket == null)
                _socket = new DatagramSocket();

          //  _socket.MessageReceived += _socket_MessageReceived;

            using (var stream = await _socket.GetOutputStreamAsync(remoteHostName, port))
            {
                using (var writer = new DataWriter(stream))
                {
                    writer.WriteBytes(data);
                    await writer.StoreAsync();
                }
            }
        }

        #region old version
        /*
         * 
         *  //old
        public async void Connect(HostName remoteHostName, string remoteServiceNameOrPort)
        {
            await _socket.ConnectAsync(remoteHostName, remoteServiceNameOrPort);
           // _socket.MessageReceived += _socket_MessageReceived;                       
        }
         * 
        public async void SendMessage(string message)
        {
            var stream = _socket.OutputStream;
            var writer = new DataWriter(stream);
            writer.WriteString(message);
            await writer.StoreAsync();
        }

        //old
        public async void SendMessage(byte[] message)
        {
            var stream = _socket.OutputStream;
            var writer = new DataWriter(stream);
            writer.WriteBytes(message);
            await writer.StoreAsync();
        }
         * */
        #endregion
    }
}