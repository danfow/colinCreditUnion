namespace colinCreditUnion.Models.Entities
{
    public class Customer
    {
        public required string  CustomerID { get; set; }

        public List<Account>? Accounts { get; set; }

    }
}
