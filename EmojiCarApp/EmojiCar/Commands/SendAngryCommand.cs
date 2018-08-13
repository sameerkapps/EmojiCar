// ////////////////////////////////////////////////////////////
// // Copyright 2018 Sameer Khandekar                        //
// // License: MIT License                                   //
// ////////////////////////////////////////////////////////////
using System;
using EmojiCar.NetDClient;
using MvvmAtom;

namespace EmojiCar.Commands
{
    /// <summary>
    /// Clas to send angry command.
    /// </summary>
    public class SendAngryCommand : AtomCommandBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:EmojiCar.Commands.SendAngryCommand"/> class.
        /// </summary>
        /// <param name="viewModel">View model.</param>
        public SendAngryCommand(AtomViewModelBase viewModel)
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
            return true;
        }

        /// <summary>
        /// Sends the command to show angry face
        /// </summary>
        /// <param name="parameter">Parameter.</param>
        public override void Execute(object parameter)
        {
            if(CanExecute(null))
            {
                NetduinoClient.Instance.Angry();
            }
        }
    }
}
