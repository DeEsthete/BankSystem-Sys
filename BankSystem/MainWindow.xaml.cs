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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Bank> Banks;
        public Operation operation;
        public MainWindow()
        {
            InitializeComponent();
            using (BankContext context = new BankContext())
            {
                context.Banks.ToList();
                Banks = context.Banks.ToList();
            }

            operation = new Operation();
            operation.RegisterHandler(AddMoneyMethod, WithdrawMethod);
        }

        #region Methods
        public string AddMoneyMethod(CashMachine machine, Person person, int money)
        {
            try
            {
                person.Purse.Money += money;
                return "Транзакция успешно завершена";
            }
            catch
            {
                return "Во время транзакции возникла ошибка";
            }
        }

        public string WithdrawMethod(CashMachine machine, Person person, int money)
        {
            if (person.Purse.Money >= money)
            {
                if (machine.Money >= money)
                {
                    person.Purse.Money -= money;
                    machine.Money -= money;
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
    }
}
