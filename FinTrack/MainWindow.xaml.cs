using FinTrackSustav.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
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
            DataContext = this; // Omogućava binding za CurrentUser
            LoadData();
        }

        public User CurrentUser => _currentUser;

        private void LoadData()
        {
            using (var context = new FinTrackContext())
            {
                var goals = context.FinancialGoals
                    .Where(g => g.UserId == _currentUser.Id)
                    .ToList();

                var transactions = context.Transactions
                    .Where(t => t.UserId == _currentUser.Id)
                    .ToList();


                dgGoals.ItemsSource = goals;
                dgTransactions.ItemsSource = transactions;
            }
            statusBarText.Content = "Podaci učitani";
            LoadFundsData();
        }

        private void LoadFundsData()
        {
            using (var context = new FinTrackContext())
            {
                var ukupniIznos = context.UkupniIznosi.FirstOrDefault(u => u.UserId == _currentUser.Id);
                decimal total = ukupniIznos?.TotalAmount ?? 0;

                decimal allocated = context.FinancialGoals
                    .Where(g => g.UserId == _currentUser.Id && g.Status != "Završen")
                    .AsEnumerable()
                    .Sum(g => g.CurrentAmount);

                decimal available = total - allocated;

                // Formatiranje iznosa
                CultureInfo euroCulture = CultureInfo.CreateSpecificCulture("fr-FR");
                txtUkupniIznos.Text = total.ToString("C", euroCulture);
                txtRaspolozivo.Text = available.ToString("C", euroCulture);
            }
        }

        private void BtnCompleteGoal_Click(object sender, RoutedEventArgs e)
        {
            var selectedGoal = dgGoals.SelectedItem as FinancialGoal;
            if (selectedGoal == null)
            {
                MessageBox.Show("Odaberite cilj za završetak.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedGoal.Status == "Završen")
            {
                MessageBox.Show("Cilj je već završen!", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedGoal.CurrentAmount < selectedGoal.TargetAmount)
            {
                MessageBox.Show("Cilj nije postignut!", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                "Jeste li sigurni da želite završiti ovaj cilj? Ova radnja je nepovratna.",
                "Potvrda",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question
            );

            if (result == MessageBoxResult.Yes)
            {
                using (var context = new FinTrackContext())
                {
                    // Ažuriraj ukupni iznos i status cilja
                    var goal = context.FinancialGoals.Find(selectedGoal.Id);
                    var ukupniIznos = context.UkupniIznosi.FirstOrDefault(u => u.UserId == _currentUser.Id);

                    if (ukupniIznos != null)
                    {
                        // Skini ciljni iznos iz ukupnog iznosa
                        ukupniIznos.TotalAmount -= goal.TargetAmount;
                        goal.Status = "Završen";
                        context.SaveChanges();
                    }
                }
                LoadData();
            }
        }

        private void BtnAddGoal_Click(object sender, RoutedEventArgs e)
        {
            var addGoalWindow = new AddGoalWindow(_currentUser);
            if (addGoalWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void BtnEditGoal_Click(object sender, RoutedEventArgs e)
        {
            var selectedGoal = dgGoals.SelectedItem as FinancialGoal;
            if (selectedGoal == null)
            {
                MessageBox.Show("Odaberite cilj za uređivanje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editGoalWindow = new EditGoalWindow(selectedGoal);
            if (editGoalWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void BtnDeleteGoal_Click(object sender, RoutedEventArgs e)
        {
            var selectedGoal = dgGoals.SelectedItem as FinancialGoal;
            if (selectedGoal == null)
            {
                MessageBox.Show("Odaberite cilj za brisanje.", "Greška", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Jeste li sigurni da želite obrisati ovaj cilj?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                using (var context = new FinTrackContext())
                {
                    context.FinancialGoals.Remove(selectedGoal);
                    context.SaveChanges();
                }
                LoadData();
            }
        }

        private void BtnAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var addTransactionWindow = new AddTransactionWindow(_currentUser);
            if (addTransactionWindow.ShowDialog() == true)
            {
                LoadData(); // Osvježi podatke nakon dodavanja transakcije
            }
        }

        private void dgGoals_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var selectedGoal = dgGoals.SelectedItem as FinancialGoal;
            if (selectedGoal != null)
            {
                btnCompleteGoal.IsEnabled = (selectedGoal.Status != "Završen");
            }
        }

        // Gumb za ručnu alokaciju sredstava prema cilju
        private void BtnAllocateFunds_Click(object sender, RoutedEventArgs e)
        {
            var allocateFundsWindow = new AllocateFundsWindow(_currentUser);
            if (allocateFundsWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        private void BtnRefreshGoals_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnRefreshTransactions_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }
    }
}
