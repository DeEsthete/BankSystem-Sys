using Domain;
using System;
using System.Collections.Generic;
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

namespace BankSystem
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Operation _operation;
        private Window _window;
        private Person _person;
        private CashMachine _cashMachine;

        public MainPage(Window window, Person person)
        {
            InitializeComponent();

            _window = window;
            _person = person;

            int cashMachineId = 1;

            using (BankContext context = new BankContext())
            {
                _cashMachine = context.CashMachines.Find(cashMachineId);
            }

            cashTextBlock.Text = _person.Purse.Money.ToString();

            _operation = new Operation();
            _operation.RegisterHandler(AddMoneyMethod, WithdrawMethod);
        }

        #region Methods
        public static string AddMoneyMethod(CashMachine machine, Person person, int money)
        {
            try
            {
                person.Purse.Money += money;
                
                using (BankContext context = new BankContext())
                {
                    context.Purses.Find(person.PurseId).Money += money;
                    context.CashMachines.Find(machine.Id).Money += money;

                    History history = new History();
                    history.CashMachine = machine;
                    history.Person = person;
                    history.Money = money;
                    history.Time = DateTime.Now;
                    context.Histories.Add(history);

                    context.SaveChanges();
                }
                return "Транзакция успешно завершена";
            }
            catch
            {
                return "Во время транзакции возникла ошибка";
            }
        }

        public static string WithdrawMethod(CashMachine machine, Person person, int money)
        {
            if (person.Purse.Money >= money)
            {
                if (machine.Money >= money)
                {
                    person.Purse.Money -= money;
                    machine.Money -= money;

                    using (BankContext context = new BankContext())
                    {
                        context.Purses.Find(person.PurseId).Money -= money;
                        context.CashMachines.Find(machine.Id).Money -= money;

                        History history = new History();
                        history.CashMachine = machine;
                        history.Person = person;
                        history.Money = money;
                        history.Time = DateTime.Now;
                        context.Histories.Add(history);

                        context.SaveChanges();
                    }
                    return "Транзакция успешно завершена";
                }
                else
                {
                    return "К сожалению в банкомате не достаточно средств";
                }
            }
            else
            {
                return "К сожалению у вас не достаточно средств";
            }
        }
        #endregion

        #region ButtonClick
        private void WithdrawButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int money = int.Parse(sumTextBlock.Text);
                _operation.WithDraw(_cashMachine, _person, money);
                int temp = int.Parse(cashTextBlock.Text);
                int currentMoney = temp;
                temp = temp - money;
                if (temp < 0)
                {
                    temp = currentMoney;
                }
                cashTextBlock.Text = temp.ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void AddMoneyButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int money = int.Parse(sumTextBlock.Text);
                _operation.AddMoney(_cashMachine, _person, money);
                int currentMoney = int.Parse(cashTextBlock.Text);
                cashTextBlock.Text = (currentMoney + money).ToString();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void CurrencyButtonClick(object sender, RoutedEventArgs e)
        {
            _window.Content = new ExchangeRates(_window, _person);
        }
        #endregion
    }
}
