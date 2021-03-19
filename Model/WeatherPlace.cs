using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model {
    public class WeatherPlace : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private Coordinates coordinates;
        private double dewPoint;
        private double humidity;
        private double temperature;
        private double cloudAreaFraction;

        public Coordinates Coordinates {
            get {
                return coordinates;
            }
            set {
                coordinates = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Coordinates"));
            }
        }

        public double DewPoint {
            get {
                return dewPoint;
            }
            set {
                if (dewPoint == value) return;
                dewPoint = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DewPoint"));
            }
        }

        public double Humidity {
            get {
                return humidity;
            }
            set {
                if (humidity == value) return;
                humidity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Humidity"));
            }
        }

        public double Temperature {
            get {
                return temperature;
            }
            set {
                if (temperature == value) return;
                temperature = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Temperature"));
            }
        }

        public double CloudAreaFraction {
            get {
                return cloudAreaFraction;
            }
            set {
                if (cloudAreaFraction == value) return;
                cloudAreaFraction = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CloudAreaFraction"));
            }
        }

        public WeatherPlace
            (Coordinates coords, double dewPoint, double humidity, double temp, double cloudAreaFraction) {
            this.Coordinates = coords;
            this.DewPoint = dewPoint;
            this.Humidity = humidity;
            this.Temperature = temp;
            this.CloudAreaFraction = cloudAreaFraction;
        }

        public WeatherPlace() {
            this.CloudAreaFraction = 0;
        }
    }
}
