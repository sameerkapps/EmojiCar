// ////////////////////////////////////////////////////////////
// // Copyright 2018 Sameer Khandekar                        //
// // License: MIT License                                   //
// ////////////////////////////////////////////////////////////
using System;
using EmojiCar.ViewModel;
using MvvmAtom;
using Xamarin.Forms;

namespace EmojiCar.Commands
{
    /// <summary>
    /// Server IP is set on the client
    /// </summary>
    public class SetServerIPCommand : AtomCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EmojiCar.Commands.SetServerIPCommand"/> class.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        public SetServerIPCommand(AtomViewModelBase viewModel)
            : base(viewModel)
        {
        }

        /// <summary>
        /// Validates if the server IP is empty
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrWhiteSpace(MainPageViewModel.ServerIP);
        }

        /// <summary>
        /// Sets the base address of the netduino
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public override void Execute(object parameter)
        {
            if(CanExecute(null))
            {
                NetDClient.NetduinoClient.Instance.BaseAddress = MainPageViewModel.ServerIP;
            }
        }

        /// <summary>
        /// Check if the changed property affects the execution ability
        /// Currently, it is only IP address
        /// </summary>
        /// <param name="propName">Property name.</param>
        public override void EvaluateCanExecuteChanged(string propName)
        {
            if(propName == nameof(MainPageViewModel.ServerIP))
            {
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Gets the main page view model.
        /// </summary>
        /// <value>The main page view model.</value>
        private MainPageViewModel MainPageViewModel => (MainPageViewModel)ViewModel;
    }
}
