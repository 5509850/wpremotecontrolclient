using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Net;

//http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202864(v=vs.105).aspx


//http://stackoverflow.com/questions/19374628/windows-phone-8-send-data-to-a-remoteserver
//http://msdn.microsoft.com/en-us/library/windowsphone/develop/hh202864(v=vs.105).aspx
#region USE CLASS
/*
  // Constants
        const int ECHO_PORT = 7;  // The Echo protocol uses port 7 in this sample
        const int QOTD_PORT = 17; // The Quote of the Day (QOTD) protocol uses port 17 in this sample
 * 
 * 
 * /// <summary>
        /// Handle the btnEcho_Click event by sending text to the echo server and outputting the response
        /// </summary>
        private void btnEcho_Click(object sender, RoutedEventArgs e)
        {
            // Clear the log
             ClearLog();
            // Make sure we can perform this action with valid data
            if (ValidateRemoteHost() && ValidateInput())
            {
                // Instantiate the SocketClient
                SocketClient client = new SocketClient();
                                // Attempt to send our message to be echoed to the echo server
                Log(String.Format("Sending '{0}' to server ...", txtInput.Text), true);
                string result = client.Send(txtRemoteHost.Text, ECHO_PORT,txtInput.Text);
                Log(result, false);
                // Receive a response from the echo server
                Log("Requesting Receive ...", true);
                result = client.Receive(ECHO_PORT);
                Log(result, false);
                // Close the socket connection explicitly
                client.Close();
            }
        }

        /// <summary>
        /// Handle the btnGetQuote_Click event by receiving text from the Quote of the Day (QOTD) server and outputting the response
        /// </summary>
        private void btnGetQuote_Click(object sender, RoutedEventArgs e)
        {
            // Clear the log
             ClearLog();
            // Make sure we can perform this action with valid data
            if (ValidateRemoteHost())
            {
                // Instantiate the SocketClient object
                SocketClient client = new SocketClient();
                // Quote of the Day (QOTD) sends a short message (selected by the server’s administrator) to a client device.
                 // For UDP, the message is sent for each incoming UDP message, so here we send a "dummy" message to solicit
                 // a response. The message cannot be empty, so the message below consists of one whitespace.
                Log(String.Format("Requesting a quote from server '{0}' ...", txtInput.Text), true);
                string dummyMessage = " ";
                string result = client.Send(txtRemoteHost.Text, QOTD_PORT, dummyMessage);
                Log(result, false);
                // Receive response from the QOTD server
                Log("Requesting Receive ...", true);
                result = client.Receive(QOTD_PORT);
                Log(result, false);
                // Close the socket connection explicitly
                client.Close();
            }
        }

        #region UI Validation
        /// <summary>
        /// Validates the txtInput TextBox
        /// </summary>
        /// <returns>True if the txtInput TextBox contains valid data, False otherwise</returns>
        private bool ValidateInput()
        {
            // txtInput must contain some text
            if (String.IsNullOrWhiteSpace(txtInput.Text))
            {
                MessageBox.Show("Please enter some text to echo");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Validates the txtRemoteHost TextBox
        /// </summary>
        /// <returns>True if the txtRemoteHost contains valid data, False otherwise</returns>
        private bool ValidateRemoteHost()
        {
            // The txtRemoteHost must contain some text
            if (String.IsNullOrWhiteSpace(txtRemoteHost.Text))
            {
                MessageBox.Show("Please enter a host name");
                return false;
            }
            return true;
        }
        #endregion

        #region Logging
        /// <summary>
        /// Log text to the txtOutput TextBox
        /// </summary>
        /// <param name="message">The message to write to the txtOutput TextBox</param>
        /// <param name="isOutgoing">True if the message is an outgoing (client to server) message, False otherwise</param>
        /// <remarks>We differentiate between a message from the client and server
         /// by prepending each line  with ">>" and "<<" respectively.</remarks>
        private void Log(string message, bool isOutgoing)
        {
            string direction = (isOutgoing) ? ">> " : "<< ";
            txtOutput.Text += Environment.NewLine + direction + message;
        }

        /// <summary>
        /// Clears the txtOutput TextBox
        /// </summary>
        private void ClearLog()
        {
            txtOutput.Text = String.Empty;
        }
        #endregion
 */
#endregion

//not USED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
namespace remotecontrolclient
{
    
        class MyUDP
        {
            // Cached Socket object that will be used by each call for the lifetime of this class
             Socket  _socket = null;
             // Signaling object used to notify when an asynchronous operation is completed
             static ManualResetEvent _clientDone = new ManualResetEvent(false);
             // Define a timeout in milliseconds for each asynchronous call. If a response is not received within this
             // timeout period, the call is aborted.
             const int TIMEOUT_MILLISECONDS = 5000;
             // The maximum size of the data buffer to use with the asynchronous socket methods
             const int MAX_BUFFER_SIZE = 2048;


             public MyUDP()
            {
                // The following creates a socket with the following properties:
                // AddressFamily.InterNetwork - the socket will use the IP version 4 addressing scheme to resolve an address
                // SocketType.Dgram - a socket that supports datagram (message) packets
                // PrototcolType.Udp - the User Datagram Protocol (UDP)
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }

             /// <summary>
             /// Send the given data to the server using the established connection
             /// </summary>
             /// <param name="serverName">The name of the server</param>
             /// <param name="portNumber">The number of the port over which to send the data</param>
             /// <param name="data">The data to send to the server</param>
             /// <returns>The result of the Send request</returns>
             public string Send(string serverName, int portNumber, string data)
             {
                 string response = "Operation Timeout";
                 // We are re-using the _socket object that was initialized in the Connect method
                 if (_socket != null)
                 {
                     // Create SocketAsyncEventArgs context object
                     SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                     // Set properties on context object
                     socketEventArg.RemoteEndPoint = new DnsEndPoint(serverName, portNumber);
                     // Inline event handler for the Completed event.
                     // Note: This event handler was implemented inline in order to make this method self-contained.
                     socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                     {
                         response = e.SocketError.ToString();
                         // Unblock the UI thread
                         _clientDone.Set();
                     });
                     // Add the data to be sent into the buffer
                     byte[] payload = Encoding.UTF8.GetBytes(data);
                     socketEventArg.SetBuffer(payload, 0, payload.Length);
                     // Sets the state of the event to nonsignaled, causing threads to block
                     _clientDone.Reset();
                     // Make an asynchronous Send request over the socket
                     _socket.SendToAsync(socketEventArg);
                     // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
                     // If no response comes back within this time then proceed
                     _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
                 }
                 else
                 {
                     response = "Socket is not initialized";
                 }
                 return response;
             }

             /// <summary>
             /// Receive data from the server
             /// </summary>
             /// <param name="portNumber">The port on which to receive data</param>
             /// <returns>The data received from the server</returns>
             public string Receive(int portNumber)
             {
                 string response = "Operation Timeout";
                 // We are receiving over an established socket connection
                 if (_socket != null)
                 {
                     // Create SocketAsyncEventArgs context object
                     SocketAsyncEventArgs socketEventArg = new SocketAsyncEventArgs();
                     socketEventArg.RemoteEndPoint = new IPEndPoint(IPAddress.Any, portNumber);
                     // Setup the buffer to receive the data
                     socketEventArg.SetBuffer(new Byte[MAX_BUFFER_SIZE], 0, MAX_BUFFER_SIZE);
                     // Inline event handler for the Completed event.
                     // Note: This even handler was implemented inline in order to make this method self-contained.
                     socketEventArg.Completed += new EventHandler<SocketAsyncEventArgs>(delegate(object s, SocketAsyncEventArgs e)
                     {
                         if (e.SocketError == System.Net.Sockets.SocketError.Success)
                         {
                             // Retrieve the data from the buffer
                             response = Encoding.UTF8.GetString(e.Buffer, e.Offset, e.BytesTransferred);
                             response = response.Trim('\0');
                         }
                         else
                         {
                             response = e.SocketError.ToString();
                         }
                         _clientDone.Set();
                     });
                     // Sets the state of the event to nonsignaled, causing threads to block
                     _clientDone.Reset();
                     // Make an asynchronous Receive request over the socket
                     _socket.ReceiveFromAsync(socketEventArg);
                     // Block the UI thread for a maximum of TIMEOUT_MILLISECONDS milliseconds.
                     // If no response comes back within this time then proceed
                     _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
                 }
                 else
                 {
                     response = "Socket is not initialized";
                 }
                 return response;
             }

             /// <summary>
             /// Closes the Socket connection and releases all associated resources
             /// </summary>
             public void Close()
             {
                 if (_socket != null)
                 {
                     _socket.Close();
                 }
             }
        }
}
