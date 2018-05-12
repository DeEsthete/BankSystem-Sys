using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
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
using СurrencyClassLibr;

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для ExchangeRates.xaml
    /// </summary>
    public partial class ExchangeRates : Page
    {
        private Window _window;
        private Person _person;
        private static ObservableCollection<Currency> _currencies { get; set; }
        private Timer _timer;
        public ExchangeRates(Window window, Person person)
        {
            InitializeComponent();

            _window = window;
            _person = person;

            GetCurrency(null);

            currencyDataGrid.ItemsSource = _currencies;

            _timer = new Timer(new TimerCallback(GetCurrency), null, 0, 5000);
        }

        public static void GetCurrency(object obj)
        {
            string value;
            using (WebClient web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;
                value = web.DownloadString("https://www.cbr-xml-daily.ru/daily_json.js");
            }
            RootObject temp = JsonConvert.DeserializeObject<RootObject>(value);
            _currencies = new ObservableCollection<Currency>
            {
                temp.Valute.KZT,
                temp.Valute.EUR,
                temp.Valute.USD,
                temp.Valute.JPY
            };
            double rubToKzt = (1 / (_currencies[0].Value / _currencies[0].Nominal));
            for (int i = 0; i < _currencies.Count; i++)
            {
                _currencies[i].Value = _currencies[i].Value / _currencies[i].Nominal;
                _currencies[i].Nominal = 1;
                _currencies[i].Value = _currencies[i].Value * rubToKzt;
            }
        }

        private void MainWindowButtonClick(object sender, RoutedEventArgs e)
        {
            _window.Content = new MainPage(_window, _person);
        }
    }
}
