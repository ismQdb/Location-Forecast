using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ViewModel;

namespace View {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();
            this.DataContext = mainWindowViewModel;
        }
    }

    public class BoolInverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool)
                return !(bool)value;
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value is bool)
                return !(bool)value;
            return value;
        }
    }

    public class CloudHeightConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double valueAsDouble = System.Convert.ToDouble(value);
            if (valueAsDouble >= 0.0 && valueAsDouble < 25.0)
                return -25;
            else if (valueAsDouble >= 25.0 && valueAsDouble < 50)
                return -12.5;
            else if (valueAsDouble >= 50 && valueAsDouble < 75)
                return 12.5;
            else if (valueAsDouble >= 75 && valueAsDouble <= 100)
                return 25;
            else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class OpacityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double valueAsDouble = System.Convert.ToDouble(value);
            /*if (valueAsDouble > 0.0 && valueAsDouble < 25.0)
                return 1;
            else if (valueAsDouble >= 25.0 && valueAsDouble < 50)
                return 0.75;
            else if (valueAsDouble >= 50 && valueAsDouble < 75)
                return 0.5;
            else if (valueAsDouble >= 75 && valueAsDouble < 100)
                return 0.25;
            else
                return 0;*/
            if (valueAsDouble != 0 && valueAsDouble != 100)
                return (100 - valueAsDouble) / 25;
            else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
