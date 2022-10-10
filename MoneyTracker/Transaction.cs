namespace MoneyTracker
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public int Month { get; set; }

        public Transaction(Guid id, string title, decimal amount, int month)
        {
            this.Id = id;
            this.Title = title;
            this.Amount = amount;
            this.Month = month;
        }
    }
}
