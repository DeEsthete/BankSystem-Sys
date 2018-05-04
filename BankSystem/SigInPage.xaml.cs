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
    /// Логика взаимодействия для SigInPage.xaml
    /// </summary>
    public partial class SigInPage : Page
    {
        private Window _window;
        private Person _person;
        public SigInPage(Window window)
        {
            InitializeComponent();
            _window = window;
        }

        private void LogInButtonClick(object sender, RoutedEventArgs e)
        {
            List<Person> people;
            List<Purse> purses;

            using (BankContext context = new BankContext())
            {
                people = context.People.ToList();
                purses = context.Purses.ToList();
            }

            bool isCorrect = false;
            for (int i = 0; i < people.Count; i ++)
            {
                if (people[i].Login == loginTextBox.Text)
                {
                    if (people[i].Password == passwordTextBox.Text)
                    {
                        isCorrect = true;
                        _person = people[i];
                        for (int j = 0; j < purses.Count; j++)
                        {
                            if (purses[j].Id == _person.PurseId)
                            {
                                _person.Purse = purses[j];
                            }
                        }
                    }
                }
            }

            if (isCorrect)
            {
                _window.Content = new MainPage(_window, _person);
            }
            else
            {
                MessageBox.Show("Пароль или логин введены не правильно!");
            }
        }

        private void SignUpButtonClick(object sender, RoutedEventArgs e)
        {
            _window.Content = new SignUp(_window);
        }
    }
}
