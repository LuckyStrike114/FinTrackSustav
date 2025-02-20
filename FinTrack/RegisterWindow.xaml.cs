using FinTrackSustav.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace FinTrackSustav
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string email = txtEmail.Text;
            string password = pwdPassword.Password;
            string confirmPassword = pwdConfirmPassword.Password;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            {
                MessageBox.Show("Sva polja su obavezna!", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Lozinke se ne podudaraju!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new FinTrackContext())
            {
                // Provjeri postoji li korisnik s istim emailom
                var existingUser = context.Users.FirstOrDefault(u => u.Email == email);
                if (existingUser != null)
                {
                    MessageBox.Show("Korisnik s ovim emailom već postoji!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Dodaj novog korisnika
                var newUser = new User
                {
                    Name = name,
                    Email = email,
                    Password = password
                };

                context.Users.Add(newUser);
                context.SaveChanges();

                MessageBox.Show("Registracija uspješna!", "Uspjeh", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true; // Zatvori prozor s rezultatom "true"
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Zatvori prozor s rezultatom "false"
        }
    }
}