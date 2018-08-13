using System;
using System.Threading;

using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace EmojiCarNetduino.Hardware
{
    /// <summary>
    /// Client for hardware. This is a singletone pattern
    /// </summary>
    public class HardwareClient : IHardwareClient
    {
        #region singleton pattern constructor and prop
        /// <summary>
        /// Instance of the client
        /// </summary>
        public static IHardwareClient Instance
            {
                get
                {
                    if (_hardware == null)
                    {
                        _hardware = new HardwareClient();
                    }

                    return _hardware;
                }
            }

        /// <summary>
        /// Contstructor that initializes the hardware and the timer
        /// </summary>
        private HardwareClient()
        {
            // TODO - Initialize ports here
            _redPort = new OutputPort(Red_Port, false);
            _greenPort = new OutputPort(Green_Port, false);
            TurnOff();
            _timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
        }
        #endregion

        #region properties
        /// <summary>
        /// Time duration in sec for which emoji remains on
        /// </summary>
        int _duration = 10; // 10 sec is the inital value
        public int TimerDuration
        {
            get
            {
                return _duration;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException();
                }

                _duration = value;
            }
        }
        #endregion

        #region inerface methods
        /// <summary>
        /// Turns on green LEDs (smile) and starts timer
        /// </summary>
        public void TurnOn_Green()
        {
            if (!_isTimerRunning)
            {
                Debug.Print("Green On");
                _greenPort.Write(true);
                StartTimer();
            }
        }

        /// <summary>
        /// Turns on red LEDs (angry) and starts timer
        /// </summary>
        public void TurnOn_Red()
        {
            if (!_isTimerRunning)
            {
                Debug.Print("Red On");
                _redPort.Write(true);
                StartTimer();
            }
        }
        #endregion

        #region Timer methods
        /// <summary>
        /// Start the timer
        /// </summary>
        private void StartTimer()
        {
            if (_isTimerRunning)
            {
                // in the current version, there is no way to stop the timer
                // will do it after upgrade
                return;
            }

            _isTimerRunning = true;
            Debug.Print("Start timer at:" + DateTime.Now.ToString());
            _timer.Change(TimerDuration * 1000, TimerDuration*1000);
        }

        /// <summary>
        /// In the timer callback, stop the timer and turn off all LEDs
        /// </summary>
        /// <param name="state"></param>
        public void TimerCallback(object state)
        {
            StopTimer();
            Debug.Print("timer call back at:" + DateTime.Now.ToString());
            TurnOff();
        }

        /// <summary>
        /// Stop times changes the next tick to infinite
        /// </summary>
        private void StopTimer()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            _isTimerRunning = false;
        }
        #endregion

        #region private methods
        /// <summary>
        /// Turns off both green and red LEDs
        /// </summary>
        private void TurnOff()
        {
            TurnOff_Green();
            TurnOff_Red();
        }

        /// <summary>
        /// Turns off green pin
        /// </summary>
        private void TurnOff_Green()
        {
            Debug.Print("Green Off");
            _greenPort.Write(false);
        }

        /// <summary>
        /// Turns off red pin
        /// </summary>
        private void TurnOff_Red()
        {
            Debug.Print("Red Off");
            _redPort.Write(false);
        }
        #endregion

        #region fields
        // ports for red and green LEDs
        private OutputPort _redPort;
        private OutputPort _greenPort;

        // timer related fields
        private bool _isTimerRunning = false;
        private Timer _timer;

        // singleton client
        private static IHardwareClient _hardware;

        // ports on the hardware
        //private readonly Cpu.Pin Red_Port = Pins.GPIO_PIN_D12;
        private readonly Cpu.Pin Red_Port = Pins.GPIO_PIN_D10;
        private readonly Cpu.Pin Green_Port = Pins.GPIO_PIN_D1;
        #endregion
    }
}
