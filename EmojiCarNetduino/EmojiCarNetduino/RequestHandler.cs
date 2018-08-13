using System;
using System.Net;

using Microsoft.SPOT;

using Maple;

using EmojiCarNetduino.Hardware;

namespace EmojiCarNetduino
{
    /// <summary>
    /// Request handler for Maple server.
    /// It currently supports only GET and POST verbs
    /// POST is used to Turn on LEDs and set timer
    /// </summary>
    public class RequestHandler : RequestHandlerBase
    {
        #region handler methods
        /// <summary>
        /// Smile request is handled by this method
        /// </summary>
        public void postSmile()
        {
            Debug.Print("Smile");
            // Turn green LED on
            HardwareClient.Instance.TurnOn_Green();
            // send response
            SendResponse();
        }

        /// <summary>
        /// Angry request is handled by this method
        /// </summary>
        public void postAngry()
        {
            Debug.Print("Angry");
            // Turn red LED on
            HardwareClient.Instance.TurnOn_Red();
            // send response
            SendResponse();
        }

        /// <summary>
        /// Set duration is handled here
        /// </summary>
        public void postSetDuration()
        {
            // obtain the duration value from the query string
            var strSec = QueryString["t"]?.ToString();
            Debug.Print(strSec);
            int sec = 0;
            // no try-parse in this flavor of .net
            // so attempt to parse and catch exception
            try
            {
                // parse the value to get int
                sec = int.Parse(strSec);
                // if value is greater than zero, set it
                if (sec > 0)
                {
                    HardwareClient.Instance.TimerDuration = sec;
                    SendResponse();
                }
                else
                {
                    // value less than or equal to zero is a bad request
                    SendResponse(HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                // this means non int value was sent in the parameter
                // send bad request response
                Debug.Print(ex.Message);
                SendResponse(HttpStatusCode.BadRequest);
            }
        }
        #endregion

        #region private method
        /// <summary>
        /// send response with HttpCode
        /// </summary>
        /// <param name="statusCode"></param>
        private void SendResponse(HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Context.Response.ContentType = "application/json";
            Context.Response.StatusCode = (int)statusCode;
            Send();
        }
        #endregion
    }
}
