using FinTrackSustav.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace FinTrackSustav
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = pwdPassword.Password;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Unesite email i lozinku!", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using (var context = new FinTrackContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    // Uspješna prijava: otvori MainWindow i proslijedi prijavljenog korisnika
                    MainWindow mainWindow = new MainWindow(user);
                    mainWindow.Show();

                    // Zatvori LoginWindow
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Neispravni podaci!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            if (registerWindow.ShowDialog() == true)
            {
                MessageBox.Show("Registracija uspješna! Prijavite se s novim podacima.", "Uspjeh", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}