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
        public SigInPage(Window window)
        {
            InitializeComponent();
            _window = window;
        }

        private void LogInButtonClick(object sender, RoutedEventArgs e)
        {
            List<Person> people;

            using (BankContext context = new BankContext())
            {
                people = context.People.ToList();
            }

            bool isCorrect = false;
            for (int i = 0; i < people.Count; i ++)
            {
                if (people[i].Login == loginTextBox.Text)
                {
                    if (people[i].Password == passwordTextBox.Text)
                    {
                        isCorrect = true;
                    }
                }
            }

            if (isCorrect)
            {
                _window.Content = new MainPage();
            }
            else
            {
                MessageBox.Show("Пароль или логин введены не правильно!");
            }
        }
    }
}
