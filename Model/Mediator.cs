using System;
using System.Net;
using System.Text;
using System.Xml;
using System.Text.Json;
using System.IO;

namespace Model {
    public class Mediator {
        public static WeatherPlace DeserializeJson(Coordinates coordinates) {
            string requestUrl = string.Format
                ("https://api.met.no/weatherapi/locationforecast/2.0/complete?lat={0}&lon={1}", coordinates.Latitude, coordinates.Longitude);
            var json = "";

            using (WebClient webClient = new WebClient()) {
                webClient.Headers.Add("user-agent", "Only a test.");
                json = webClient.DownloadString(requestUrl);
            }

            
        }
        public static Coordinates GetCoordinatesFromString(string region) {
            StringBuilder formatedRegion = new StringBuilder();
            string htmlResponse = string.Empty;

            string[] vs = region.Split(' ');

            for (int i = 0; i < vs.Length; ++i)
                if (i == vs.Length - 1)
                    formatedRegion.Append(vs[i]);
                else
                    formatedRegion.Append($"{vs[i]}+");

            string searchApiString =
                $"https://nominatim.openstreetmap.org/search?q={formatedRegion}&format=json&limit=1";

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(searchApiString);
            webRequest.AutomaticDecompression = DecompressionMethods.GZip;
            webRequest.UserAgent = ".NET Framework Test Client!";

            using (HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
                htmlResponse = reader.ReadToEnd();

            Coordinates coordinates = JsonSerializer.Deserialize<Coordinates>(htmlResponse);

            return coordinates;
        }
    }

    public struct Coordinates {
        private double lat;
        private double lng;

        public double Latitude {
            get { return lat; }
            set { lat = value; }
        }

        public double Longitude {
            get { return lng; }
            set { lng = value; }
        }

        public Coordinates(double latitude, double longitude) {
            lat = latitude;
            lng = longitude;
        }
    }
}
