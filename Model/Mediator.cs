using Newtonsoft.Json;
using System;
using System.Net;
using System.Xml;

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

            XmlDocument xmlDocument = JsonConvert.DeserializeXmlNode(json, "type");
            string xpath = "type/properties/timeseries/data/instant/details/dew_point_temperature";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            double dew = Convert.ToDouble(xmlNode.InnerText);

            xpath = "type/properties/timeseries/data/instant/details/relative_humidity";
            xmlNode = xmlDocument.SelectSingleNode(xpath);
            double humidity = Convert.ToDouble(xmlNode.InnerText);

            xpath = "type/properties/timeseries/data/instant/details/air_temperature";
            xmlNode = xmlDocument.SelectSingleNode(xpath);
            double temp = Convert.ToDouble(xmlNode.InnerText);

            xpath = "type/properties/timeseries/data/instant/details/cloud_area_fraction";
            xmlNode = xmlDocument.SelectSingleNode(xpath);
            double cloudAreaFraction = Convert.ToDouble(xmlNode.InnerText);

            return new WeatherPlace(coordinates, dew, humidity, temp, cloudAreaFraction);
        }
        public static Coordinates GetCoordinatesFromString(string region) {
            // API key dobijen od Google naloga
            string apiKey = "AIzaSyD1sorRREGD-MvHcYQtKo28UDhAHsuBFNA";
            string requestUri = string.Format
                ("https://maps.googleapis.com/maps/api/geocode/xml?key={1}&address={0}&sensor=false", Uri.EscapeDataString(region), apiKey);
            string xmlFromWebPage = "";

            using (WebClient webClient = new WebClient()) {
                webClient.Headers.Add("user-agent", "Only a test.");
                xmlFromWebPage = webClient.DownloadString(requestUri);
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlFromWebPage);

            string xpath = "GeocodeResponse/result/geometry/location/lat";
            XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
            string lat = xmlNode.InnerText;

            xpath = "GeocodeResponse/result/geometry/location/lng";
            xmlNode = xmlDocument.SelectSingleNode(xpath);
            string lng = xmlNode.InnerText;

            Coordinates coordinates = new Coordinates(Convert.ToDouble(lat), Convert.ToDouble(lng));

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
