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
        public MainWindow()
        {
            InitializeComponent();
            using (BankContext context = new BankContext())
            {
                CashMachine cashMachine = new CashMachine();
                cashMachine.Money = 10000000;
                cashMachine.Addres = "Московская 25";
                context.CashMachines.Add(cashMachine);

                context.SaveChanges();
            }

            this.Content = new SigInPage(this);
        }
    }
}
