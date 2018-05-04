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
        private static ObservableCollection<Currency> currencies { get; set; }
        public ExchangeRates(Window window)
        {
            InitializeComponent();

            _window = window;

            Thread threadFirst = new Thread(new ThreadStart(GetValueJson));
            threadFirst.Start();
            threadFirst.Join();
            currencyDataGrid.ItemsSource = currencies;
        }

        public static void GetValueJson()
        {
            string valueJson;
            using (var webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                valueJson = webClient.DownloadString("https://www.cbr-xml-daily.ru/daily_json.js");
            }
            RootObject obj = JsonConvert.DeserializeObject<RootObject>(valueJson);
            currencies = new ObservableCollection<Currency>();
            currencies.Add(obj.Valute.EUR);
            currencies.Add(obj.Valute.KZT);
            currencies.Add(obj.Valute.USD);
        }
    }
}
