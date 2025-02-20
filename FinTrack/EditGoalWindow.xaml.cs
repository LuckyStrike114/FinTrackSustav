using FinTrackSustav.Models;
using System;
using System.Windows;
using System.Windows.Controls;

namespace FinTrackSustav
{
    public partial class EditGoalWindow : Window
    {
        private FinancialGoal _goal;

        public EditGoalWindow(FinancialGoal goal)
        {
            InitializeComponent();
            _goal = goal;

            // Popunjavanje polja s postojećim podacima
            txtGoalName.Text = _goal.GoalName;
            txtTargetAmount.Text = _goal.TargetAmount.ToString();
         
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validacija unosa
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


            // Ažuriranje svojstava cilja
            _goal.GoalName = txtGoalName.Text;
            _goal.TargetAmount = targetAmount;


            // Spremanje promjena u bazu podataka
            try
            {
                using (var context = new FinTrackContext())
                {
                    context.FinancialGoals.Update(_goal);
                    context.SaveChanges();
                }
                MessageBox.Show("Cilj je uspješno ažuriran.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dogodila se greška prilikom ažuriranja cilja: {ex.Message}", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
