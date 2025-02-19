public class FinancialGoal
{
    public int Id { get; set; }
    public string GoalName { get; set; }
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; }
    public string Status { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}