using System;
using System.Threading;
using Microsoft.SPOT;

using Maple;

// bit.ly/WLSoccer
// bit.ly/
// General - developer.wildernesslabs.co/Samples/
// Maple - https://github.com/WildernessLabs/Maple
// More Maple - http://blog.wildernesslabs.co/maple-server-now-supports-automatic-udp-broadcasts/
// COnnected samples - https://github.com/WildernessLabs/Netduino_Samples/tree/master/Connected_CoffeeMaker
// Connected Sample - https://github.com/WildernessLabs/Netduino_Samples/tree/master/Connected_RgbLed
// LEDs using transistor - https://www.petervis.com/Raspberry_PI/Driving_LEDs_with_CMOS_and_TTL_Outputs/Driving_an_LED_Using_Transistors.html

namespace EmojiCarNetduino
{
    public class Program
    {
        public static void Main()
        {
            App app = new App();
            app.Run();

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
