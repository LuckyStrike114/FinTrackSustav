using FinTrackSustav.Models; // Provjerite da namespace odgovara
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
            using (var context = new FinTrackContext())
            {
                string email = txtEmail.Text;
                string password = pwdPassword.Password;

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
                    MessageBox.Show("Neispravni podaci!");
                }
            }
        }
    }
}
