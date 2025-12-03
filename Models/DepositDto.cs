namespace colinCreditUnion.Models
{
    public class DepositDto
    {
        public required string CustomerId { get; set; }
        public required string AccountDepositedId { get; set; }
        public double DepositAmount { get; set; }
        public double? Balance{ get; set; }
        public bool Succeeded { get; set; }


    }
}
