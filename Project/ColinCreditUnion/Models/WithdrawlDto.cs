namespace colinCreditUnion.Models
{
    public class WithdrawlDto
    {
        public required string CustomerId { get; set; }
        public required string AccountWithdrawledId { get; set; }
        public double WithdrawlAmount { get; set; }
        private double? Balance { get; set; }
        private bool Succeeded { get; set; } = true;
        public void SetBalance(double balance) { Balance = balance; }
        public double? getBalance() { return Balance; }
        public void Fail() { Succeeded = false; }
    }
}
