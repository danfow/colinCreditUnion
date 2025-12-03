namespace colinCreditUnion.Models
{
    public class AccountDto
    {
        public required string AccountId { get; set; }
        public required string CustomerId { get; set; }
        public required double InitialDeposit { get; set; }
        public required int AccountTypeId { get; set; }
        public required bool IsClosed { get; set; } = false;

        public bool succeeded = true;

        private double Balance { get; set; }

        public void SetBalance (double balance){ Balance = balance; }
    }
}
