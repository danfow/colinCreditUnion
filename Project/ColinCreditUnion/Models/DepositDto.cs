namespace colinCreditUnion.Models
{
    public class DepositDto
    {
        public required string CustomerId { get; set; }
        public required string AccountDepositedId { get; set; }
        public double DepositAmount { get; set; }
        private double? Balance { get; set; }
        private bool Succeeded { get; set; } = true;
        public void SetBalance(double balance) { Balance = balance; }
        public double? getBalance () { return Balance; }
        public void Fail() { Succeeded = false; }


    }
}
