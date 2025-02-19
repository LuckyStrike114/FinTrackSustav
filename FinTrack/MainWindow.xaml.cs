using FinTrackSustav.Models;
using System.Windows;

namespace FinTrackSustav
{
    public partial class MainWindow : Window
    {
        private User _currentUser;

        public MainWindow(User user)
        {
            InitializeComponent();
            _currentUser = user;
            // Inicijalizacija ili učitavanje podataka za korisnika
        }

        // Ako želite, možete zadržati i default konstruktor
        public MainWindow() : this(null) { }

        private void BtnAddGoal_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gumb 'Dodaj cilj' je kliknut!");
        }

        private void BtnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Gumb 'Dodaj transakciju' je kliknut!");
        }
    }
}
