// ////////////////////////////////////////////////////////////
// // Copyright 2018 Sameer Khandekar                        //
// // License: MIT License                                   //
// ////////////////////////////////////////////////////////////
using System;

using System.Net.Http;
using System.Threading.Tasks;

namespace EmojiCar.NetDClient
{
    /// <summary>
    /// Client that communicates with the netduino
    /// Uses Singleton pattern
    /// </summary>
    public class NetduinoClient
    {
        /// <summary>
        /// Instance
        /// </summary>
        /// <value>The instance.</value>
        public static NetduinoClient Instance
        {
            get
            {
                return _client;
            }
        }

        /// <summary>
        /// Sends smile command using POST verb on the REST API
        /// </summary>
        async public Task<string> Smile()
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsync(BaseAddress + "Smile", new StringContent(string.Empty));

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        /// <summary>
        /// Sends angry command using POST verb on the REST API
        /// </summary>
        async public Task<string> Angry()
        {
            try
            {
                HttpResponseMessage response = await Client.PostAsync(BaseAddress + "Angry", new StringContent(string.Empty));

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Sends duration using POST verb on the REST API
        /// </summary>
        async public Task<string> SetDuration(int sec)
        {
            try
            {
                string durationURL = $"{BaseAddress}SetDuration?t={sec}";
                HttpResponseMessage response = await Client.PostAsync(durationURL, new StringContent(string.Empty));

                return response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Base address of netduion. Set with just the address and
        /// Not the schema. returns with schema
        /// </summary>
        private string _baseAddress;
        internal string BaseAddress
        {
            set
            {
                _baseAddress = value;
            }

            get
            {
                return "http://" + _baseAddress + "/";
            }
        }

#region fields
        // singleton instance
        private static NetduinoClient _client = new NetduinoClient();
        // HttpClient to make the calls
        protected HttpClient Client { get; private set; } = new HttpClient();
#endregion
    }
}
