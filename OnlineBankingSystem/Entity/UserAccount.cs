namespace OnlineBankingSystem.Entity
{
    public class UserAccount
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }

        public string IBAN { get; set; }
        public double Balance { get; set; }
    }

    public class UserAccountActivity
    {
        public int Id { get; set; }
        public int UserAccountId { get; set; }
        public UserAccount UserAccount { get; set; }
        public ActivityType ActivityType { get; set; }
        public double Total { get; set; }
        public double TransactionAfterBalance { get; set; }
        public string TargetIBAN { get; set; }
        public string TargetUserFullName { get; set; }

    }

    public enum ActivityType
    {
        GidenTransfer,
        GelenTransfer,
    }
}
