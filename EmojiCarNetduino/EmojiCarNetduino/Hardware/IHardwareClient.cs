using System;

namespace EmojiCarNetduino.Hardware
{
    /// <summary>
    /// Interface to acess the hardware
    /// </summary>
    public interface IHardwareClient
    {
        /// <summary>
        /// Turns on Red LEDs
        /// </summary>
        void TurnOn_Red();

        /// <summary>
        /// Turns on Green LEDs
        /// </summary>
        void TurnOn_Green();

        /// <summary>
        /// Duration of the timer in sec
        /// </summary>
        int TimerDuration { get; set; }
    }
}
