using FinTrackSustav.Models;
using System.Windows;

namespace FinTrackSustav
{
    public partial class AddGoalWindow : Window
    {
        private User _currentUser;

        public AddGoalWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Provjera unosa
            if (string.IsNullOrWhiteSpace(txtGoalName.Text))
            {
                MessageBox.Show("Unesite naziv cilja.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtTargetAmount.Text, out decimal targetAmount))
            {
                MessageBox.Show("Unesite ispravan iznos za ciljni iznos.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kreiraj novi cilj
            FinancialGoal newGoal = new FinancialGoal
            {
                GoalName = txtGoalName.Text,
                TargetAmount = targetAmount,
                CurrentAmount = 0,         // Početni iznos je 0
                Status = "Aktivan",        // Možete prilagoditi početni status po potrebi
                UserId = _currentUser.Id
            };

            // Spremi cilj u bazu
            using (var context = new FinTrackContext())
            {
                context.FinancialGoals.Add(newGoal);
                context.SaveChanges();
            }

            MessageBox.Show("Novi cilj je uspješno dodan.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            this.DialogResult = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
