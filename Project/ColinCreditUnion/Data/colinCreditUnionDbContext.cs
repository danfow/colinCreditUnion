using colinCreditUnion.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace colinCreditUnion.Data
{
    public class colinCreditUnionDbContext : DbContext
    {
        public colinCreditUnionDbContext(DbContextOptions<colinCreditUnionDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
