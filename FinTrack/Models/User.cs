using FinTrackSustav.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<FinancialGoal> FinancialGoals { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}