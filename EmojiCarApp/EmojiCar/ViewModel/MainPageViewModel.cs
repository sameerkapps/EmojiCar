// ////////////////////////////////////////////////////////////
// // Copyright 2018 Sameer Khandekar                        //
// // License: MIT License                                   //
// ////////////////////////////////////////////////////////////
using System;
using System.Windows.Input;
using EmojiCar.Commands;
using MvvmAtom;

// Furure - This is my hobby/weekend project. App is not robust to handle potential errors. 
// Disable when not connected to internet
// Error handling
// Settings to store the last IP address

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

            // In future, Get the sever IP from the settings
            ServerIP = "10.11.98.11";
            SetServerIPCmd.Execute(null);
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
                if(_serverIP != value)
                {
                    _serverIP = value;
                    RaisePropertyChanged();
                }
            }
        }

        /// <summary>
        /// On Duration
        /// </summary>
        int _duration = 10;
        public int DurationSec
        {
            get
            {
                return _duration;
            }
            set
            {
                if(_duration != value)
                {
                    _duration = value;
                    RaisePropertyChanged();
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
    }
}
