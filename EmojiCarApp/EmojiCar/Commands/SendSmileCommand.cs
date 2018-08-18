// ////////////////////////////////////////////////////////////
// // Copyright 2018 Sameer Khandekar                        //
// // License: MIT License                                   //
// ////////////////////////////////////////////////////////////
using System;
using EmojiCar.NetDClient;
using EmojiCar.ViewModel;
using MvvmAtom;

namespace EmojiCar.Commands
{
    /// <summary>
    /// Send smile command.
    /// </summary>
    public class SendSmileCommand : AtomCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EmojiCar.Commands.SendSmileCommand"/> class.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        public SendSmileCommand(AtomViewModelBase viewModel)
            : base(viewModel)
        {
        }

        /// <summary>
        /// Always true.
        /// To improve in the future -> connectivity check
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        public override bool CanExecute(object parameter)
        {
            return MainPageViewModel.IsConnected;
        }

        /// <summary>
        /// Sends Smile command to netduino
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public async override void Execute(object parameter)
        {
            if (CanExecute(null))
            {
                var message = await NetduinoClient.Instance.Smile();
                MainPageViewModel.Message = message;
            }
        }

        /// <summary>
        /// Evaluates the can execute changed based on the internet connectivity
        /// </summary>
        /// <param name="propName">Property name.</param>
        public override void EvaluateCanExecuteChanged(string propName)
        {
            if (nameof(MainPageViewModel.IsConnected).Equals(propName))
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
