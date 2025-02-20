using FinTrackSustav.Models;
using System;
using System.Linq;
using System.Windows;

namespace FinTrackSustav
{
    public partial class AllocateFundsWindow : Window
    {
        private User _currentUser;
        private FinancialGoal _selectedGoal;

        public AllocateFundsWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            LoadGoals();
        }

        private void LoadGoals()
        {
            using (var context = new FinTrackContext())
            {
                var goals = context.FinancialGoals
                    .Where(g => g.UserId == _currentUser.Id && g.Status != "Završen")
                    .ToList();

                cmbGoals.ItemsSource = goals;
                if (goals.Any())
                {
                    cmbGoals.SelectedIndex = 0; // Postavi prvi cilj kao odabrani
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(txtAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Unesite ispravan iznos!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _selectedGoal = cmbGoals.SelectedItem as FinancialGoal;
            if (_selectedGoal == null)
            {
                MessageBox.Show("Odaberite cilj!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var context = new FinTrackContext())
            {
                // Provjeri raspoloživa sredstva
                var TotalAmount = context.totalAmounts.FirstOrDefault(u => u.UserId == _currentUser.Id);
                decimal allocated = context.FinancialGoals
                    .Where(g => g.UserId == _currentUser.Id && g.Status != "Završen")
                    .AsEnumerable()
                    .Sum(g => g.CurrentAmount);
                decimal available = (TotalAmount?.totalAmount ?? 0) - allocated;

                if (amount > available)
                {
                    MessageBox.Show("Nemate dovoljno raspoloživih sredstava!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Ažuriraj trenutni iznos cilja
                var goal = context.FinancialGoals.Find(_selectedGoal.Id);
                goal.CurrentAmount += amount;

                // Spriječi prekoračenje ciljnog iznosa
                if (goal.CurrentAmount > goal.TargetAmount)
                {
                    MessageBox.Show("Ne možete alocirati više od ciljnog iznosa!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    goal.CurrentAmount = goal.TargetAmount;
                }

                context.SaveChanges();
            }

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
