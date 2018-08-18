// ////////////////////////////////////////////////////////////
// // Copyright 2018 Sameer Khandekar                        //
// // License: MIT License                                   //
// ////////////////////////////////////////////////////////////
using System;
using System.Windows.Input;
using EmojiCar.Commands;
using MvvmAtom;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EmojiCar.ViewModel
{
    /// <summary>
    /// Main page view model.
    /// </summary>
    public class MainPageViewModel : AtomViewModelBase
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainPageViewModel()
        {
            // init the commands
            SetServerIPCmd = new SetServerIPCommand(this);
            SetDurationCmd = new SetDurationCommand(this);
            SmileCmd = new SendSmileCommand(this);
            AngryCmd = new SendAngryCommand(this);

            // check the internet connection and display appropriate message
            IsConnected = Connectivity.NetworkAccess == NetworkAccess.Internet;
            DisplayConnectivityMessage();
            // handle connectivity changes
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            // get server ip from preferences
            ServerIP = Preferences.Get(ServerIPKey, string.Empty);
            SetServerIPCmd.Execute(null);

            // get duration from preferences
            DurationSec = Preferences.Get(DurationKey, 0);
        }

        /// <summary>
        /// IP Address of the server
        /// </summary>
        private string _serverIP;
        public string ServerIP
        {
            get
            {
                return _serverIP;
            }

            set
            {
                if (_serverIP != value)
                {
                    _serverIP = value;
                    Preferences.Set(ServerIPKey, ServerIP);
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// On Duration
        /// </summary>
        int _duration = 5;
        public int DurationSec
        {
            get
            {
                return _duration;
            }
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    Preferences.Set(DurationKey, DurationSec);
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Result of an operation
        /// </summary>
        string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                if (!string.Equals(_message, value))
                {
                    _message = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// Is connected to internet
        /// </summary>
        bool _isConnected;
        public bool IsConnected
        {
            get
            {
                return _isConnected;
            }
            set
            {
                if (_isConnected != value)
                {
                    _isConnected = value;
                    RaisePropertyChanged();
                    DisplayConnectivityMessage();
                }
            }
        }

        /// <summary>
        /// Server IP Command.
        /// </summary>
        /// <value>The set server IPC md.</value>
        public ICommand SetServerIPCmd
        {
            get;
        }

        /// <summary>
        /// Set duration cmd.
        /// </summary>
        /// <value>The set duration cmd.</value>
        public ICommand SetDurationCmd
        {
            get;
        }

        /// <summary>
        /// Smile cmd.
        /// </summary>
        /// <value>The smile cmd.</value>
        public ICommand SmileCmd
        {
            get;
        }

        /// <summary>
        /// Angry cmd.
        /// </summary>
        /// <value>The angry cmd.</value>
        public ICommand AngryCmd
        {
            get;
        }

        #region private methods
        /// <summary>
        /// Connectivity change handler
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() => IsConnected = (e.NetworkAccess == NetworkAccess.Internet));
        }

        /// <summary>
        /// Displays message as per the connectivity
        /// </summary>
        private void DisplayConnectivityMessage()
        {
            if (!IsConnected)
            {
                Message = "!!! Not connected to the Internet !!!";
            }
            else
            {
                Message = string.Empty;
            }
        }
        #endregion

        #region keys for preference storage
        // key for server ip
        private const string ServerIPKey = "ServerIP";

        // key for duration
        private const string DurationKey = "Duration";
        #endregion

    }
}
