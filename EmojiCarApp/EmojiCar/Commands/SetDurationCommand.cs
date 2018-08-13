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
    /// Set duration command.
    /// </summary>
    public class SetDurationCommand: AtomCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EmojiCar.Commands.SetDurationCommand"/> class.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        public SetDurationCommand(AtomViewModelBase viewModel)
            : base(viewModel)
        {
        }

        /// <summary>
        /// Validates that the entered duration is greater than zero
        /// Future check connectivity
        /// </summary>
        /// <returns><c>true</c>, if execute was caned, <c>false</c> otherwise.</returns>
        /// <param name="parameter">Parameter.</param>
        public override bool CanExecute(object parameter)
        {
            return (MainPageViewModel.DurationSec > 0);
        }

        /// <summary>
        /// Execute the command to set on duration
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public override void Execute(object parameter)
        {
            if(CanExecute(null))
            {
                NetduinoClient.Instance.SetDuration(MainPageViewModel.DurationSec);
            }
        }

        /// <summary>
        /// Check if the changed property affects the execution ability
        /// Currently, it is only duration
        /// </summary>
        /// <param name="propName">Property name.</param>
        public override void EvaluateCanExecuteChanged(string propName)
        {
            if(propName == nameof(MainPageViewModel.DurationSec))
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
