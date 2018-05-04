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
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        private Window _window;

        public SignUp(Window window)
        {
            InitializeComponent();
            _window = window;
        }

        private void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            List<Person> people;
            using (BankContext context = new BankContext())
            {
                people = context.People.ToList();
            }

            bool isCorrect = true;
            for (int i = 0; i < people.Count; i++)
            {
                if (people[i].Login == loginTextBox.Text)
                {
                    isCorrect = false;
                }
            }
            if (fullNameTextBox.Text == "")
            {
                isCorrect = false;
                if (passwordTextBox.Text == "")
                {
                    isCorrect = false;
                }
            }

            if (isCorrect)
            {
                Person temp = new Person();
                temp.FullName = fullNameTextBox.Text;
                temp.Login = loginTextBox.Text;
                temp.Password = passwordTextBox.Text;
                Purse purse = new Purse();
                purse.Money = 0;

                List<Purse> purses = new List<Purse>();
                using (BankContext context = new BankContext())
                {
                    context.Purses.Add(purse);
                    context.SaveChanges();
                    purses = context.Purses.ToList();
                }
                temp.Purse = purses[purses.Count-1];
                temp.PurseId = temp.Purse.Id;
                using (BankContext context = new BankContext())
                {
                    context.People.Add(temp);
                    context.SaveChanges();
                }

                MessageBox.Show("Регистрация прошла успешно!");
                _window.Content = new SigInPage(_window);
            }
            else
            {
                MessageBox.Show("Не все данные введены верно!");
            }
        }
    }
}
