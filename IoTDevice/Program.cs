using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IoTDevice
{
    class Program
    {
        static void Main(string[] args)
        {
            string deviceId = "YOUR-DEVICE-ID".ToLower();
            string deviceKey = "YOUR-DEVICE PK OR SK";
            string hostname = $"YOUR-ENDPOINT-NAMESPACE.azure-devices.net";
            string endpoint = $"{hostname}/devices/{deviceId}";
            var token = GenerateSasToken(endpoint, deviceKey);
            var conn = $"HostName={hostname};CredentialType=SharedAccessSignature;DeviceId={deviceId};SharedAccessSignature={token}";
            var deviceClient = DeviceClient.CreateFromConnectionString(conn, TransportType.Amqp_WebSocket_Only);
            SendEvent(deviceClient).ConfigureAwait(false).GetAwaiter().GetResult();
        }


        static async Task SendEvent(DeviceClient deviceClient)
        {

            var dataBuffer = JsonConvert.SerializeObject(
                new
                {
                    Wind = 10,
                    Humidity = 70,
                    Precipitation = 0
                });
            var eventMessage = new Message(Encoding.UTF8.GetBytes(dataBuffer));
            // Add  key value to each message for routing, key is the controller, value is the function
            eventMessage.Properties.Add("Temp", "AddAsync");

            await deviceClient.SendEventAsync(eventMessage);


        }
        public static string GenerateSasToken(string resourceUri, string key, string policyName = null, int expiryInSeconds = 3600)
        {
            TimeSpan fromEpochStart = DateTime.UtcNow - new DateTime(1970, 1, 1);
            string expiry = Convert.ToString((int)fromEpochStart.TotalSeconds + expiryInSeconds);

            string stringToSign = WebUtility.UrlEncode(resourceUri) + "\n" + expiry;

            HMACSHA256 hmac = new HMACSHA256(Convert.FromBase64String(key));
            string signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(stringToSign)));

            string token = String.Format(CultureInfo.InvariantCulture, "SharedAccessSignature sr={0}&sig={1}&se={2}", WebUtility.UrlEncode(resourceUri), WebUtility.UrlEncode(signature), expiry);

            if (!String.IsNullOrEmpty(policyName))
            {
                token += "&skn=" + policyName;
            }

            return token;
        }
    }
}
