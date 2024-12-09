namespace EldenBoost.Core.Models.Payment
{
    public class PaymentListViewModel
    {
        public int Id { get; set; }

        public string BoosterName { get; set; } = null!;

        public int BoosterId { get; set; }

        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }

        public string IssueDate { get; set; } = null!;

        public string? CompletionDate { get; set; }

        public List<int> Orders { get; set; } = new List<int>();
    }
}
