namespace colinCreditUnion.Models.Entities
{
    public class Account
    {
        public required string  AccountId { get; set; }
        public required double Balance { get; set; }
        public required int AccountTypeId { get; set; }
        public required bool IsClosed { get; set; }


    }
}
