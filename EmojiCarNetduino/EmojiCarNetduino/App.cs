
using System;
using System.Threading;

using Microsoft.SPOT;

using Maple;
using Netduino.Foundation.Network;

namespace EmojiCarNetduino
{
    /// <summary>
    /// Class to initialize the network and get the server running
    /// </summary>
    public class App
    {
        /// <summary>
        /// Get the app running
        /// </summary>
        internal void Run()
        {
            bool isConnected = false;

            try
            {
                // attempt to connect
                isConnected = InitNetwork();
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }

            // if it is connected, take the other initialize actions
            if (isConnected)
            {
                // start REST API server
                MapleServer server = new MapleServer();
                server.AddHandler(new RequestHandler());
                server.Start(ServerName, Initializer.CurrentNetworkInterface.IPAddress);
            }
        }

        /// <summary>
        /// Start connecting to the network
        /// </summary>
        /// <returns></returns>
        protected bool InitNetwork()
        {
            Initializer.NetworkConnected += Initializer_NetworkConnected;
            Initializer.InitializeNetwork();

            Debug.Print("InitializeNetwork()");
            var result = _netConnected.WaitOne(NetworkConnectionTimeoutMilliSec, true);

            return result;
        }

        /// <summary>
        /// Event occurs when network is connected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Initializer_NetworkConnected(object sender, EventArgs e)
        {
            Debug.Print("Network connected");
            _netConnected.Set();
        }

        // event for indicating connection established
        ManualResetEvent _netConnected = new ManualResetEvent(false);

        #region constants
        // const for network connection timeout
        private const int NetworkConnectionTimeoutMilliSec = 20 * 1000;
        // name of the server
        private const string ServerName = "emojicar";
        #endregion
    }
}
