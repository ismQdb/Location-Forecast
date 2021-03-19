using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ViewModel;

namespace ViewModel {
    public class MainWindowViewModel : INotifyPropertyChanged {
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private string northCoordinate;

        public string NorthCoordinate {
            get { return northCoordinate; }
            set {
                if (northCoordinate == value) return;
                northCoordinate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NorthCoordinate"));
            }
        }

        private string eastCoordinate;

        public string EastCoordinate {
            get { return eastCoordinate; }
            set {
                if (eastCoordinate == value) return;
                eastCoordinate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("EastCoordinate"));
            }
        }

        private bool isFirstRadioButtonChecked = true;

        public bool IsFirstRadioButtonChecked {
            get { return isFirstRadioButtonChecked; }
            set {
                if (isFirstRadioButtonChecked == value) return;
                isFirstRadioButtonChecked = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsFirstRadioButtonChecked"));
            }
        }

        private bool areDataForDeparture = true;

        public bool AreDataForDeparture {
            get { return areDataForDeparture; }
            set {
                if (areDataForDeparture == value) return;
                areDataForDeparture = value;
                OnPropertyChanged(new PropertyChangedEventArgs("AreDataForDeparture"));
            }
        }

        private string region;

        public string Region {
            get { return region; }
            set {
                if (region == value) return;
                region = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Region"));
            }
        }

        private Coordinates coordinates;

        public Coordinates Coordinates {
            get { return coordinates; }
            set {
                coordinates = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Coordinates"));
            }
        }

        private WeatherPlace departure;

        public WeatherPlace Departure {
            get { return departure; }
            set {
                if (departure == value) return;
                departure = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Departure"));
            }
        }

        private WeatherPlace destination;

        public WeatherPlace Destination {
            get { return destination; }
            set {
                if (destination == value) return;
                destination = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Destination"));
            }
        }

        public MainWindowViewModel() {
            Departure = new WeatherPlace();
            Destination = new WeatherPlace();
            SubmitCommand = new CommandTemplate(SubmitExecute, CanSubmit);
            Coordinates = new Coordinates();
        }

        private ICommand submitCommand;

        public ICommand SubmitCommand {
            get {
                return submitCommand;
            }
            set {
                if (submitCommand == value) return;
                submitCommand = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SubmitCommand"));
            }
        }

        public void SubmitExecute(object parameter) {
            if (!IsFirstRadioButtonChecked) {
                if (AreDataForDeparture)
                    Departure = Mediator.DeserializeJson(Mediator.GetCoordinatesFromString(Region));
                else
                    Destination = Mediator.DeserializeJson(Mediator.GetCoordinatesFromString(Region));
            }
            else {
                if (AreDataForDeparture)
                    Departure = Mediator.DeserializeJson(new Coordinates(Convert.ToDouble(NorthCoordinate), Convert.ToDouble(EastCoordinate)));
                else
                    Destination = Mediator.DeserializeJson(new Coordinates(Convert.ToDouble(NorthCoordinate), Convert.ToDouble(EastCoordinate)));
            }

        }

        public bool CanSubmit(object parameter) {
            //if (((Coordinates.Latitude as object) != null && (Coordinates.Longitude as object) != null) ^ Region != null)
                return true;
            //else
              //  return false;
        }
    }
}
