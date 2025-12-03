namespace colinCreditUnion.Models.Entities
{
    public class Customer
    {
        public required string  CustomerId { get; set; }

        public List<Account>? Accounts { get; set; }

    }
}
