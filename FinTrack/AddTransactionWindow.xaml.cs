using FinTrackSustav.Models;
using System;
using System.Windows;

namespace FinTrackSustav
{
    public partial class AddTransactionWindow : Window
    {
        private User _currentUser;

        public AddTransactionWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            dpDate.SelectedDate = DateTime.Now; // Postavi zadani datum na danas
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Provjera unosa
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Unesite opis transakcije.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtAmount.Text, out decimal amount))
            {
                MessageBox.Show("Unesite ispravan iznos.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!dpDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Odaberite datum transakcije.", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kreiraj novu transakciju
            Transaction newTransaction = new Transaction
            {
                Description = txtDescription.Text,
                Amount = amount,
                Date = dpDate.SelectedDate.Value,
                UserId = _currentUser.Id
            };

            // Spremi transakciju u bazu
            // Spremi transakciju i ažuriraj ukupni iznos
            using (var context = new FinTrackContext())
            {
                context.Transactions.Add(newTransaction);

                // Ažuriraj ukupni iznos
                var TotalAmount = context.totalAmounts.FirstOrDefault(u => u.UserId == _currentUser.Id);
                if (TotalAmount == null)
                {
                    TotalAmount = new TotalAmount { UserId = _currentUser.Id, totalAmount = 0 };
                    context.totalAmounts.Add(TotalAmount);
                }
                TotalAmount.totalAmount += newTransaction.Amount;
                context.SaveChanges();
            }

            MessageBox.Show("Nova transakcija je uspješno dodana.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
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
