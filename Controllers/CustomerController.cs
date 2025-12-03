using colinCreditUnion.Data;
using colinCreditUnion.Models;
using colinCreditUnion.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Formatting;

namespace colinCreditUnion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private colinCreditUnionDbContext dbcontext;
        public CustomerController(colinCreditUnionDbContext dbContext)
        {
            this.dbcontext = dbContext;

        }
        [Route("AddAccount")]
        [HttpPost]
        public IActionResult AddAccount(AccountDto accountDto)
        {
            var alreadyCustomer = dbcontext.Accounts
               .FirstOrDefault(a => a.CustomerId == accountDto.CustomerId);

            if ( (alreadyCustomer == null && accountDto.AccountTypeId == 1) || accountDto.InitialDeposit < 100) 
            {
                return BadRequest();
            }
            else
            {
                if (alreadyCustomer == null)
                {
                    var customer = new Customer() { CustomerId = accountDto.CustomerId };
                    dbcontext.Add(customer);
                    dbcontext.SaveChanges();
                }

            }
            var customerAccount = new Account()
            {
                CustomerId = accountDto.CustomerId,
                AccountId = accountDto.AccountId,
                AccountTypeId = accountDto.AccountTypeId,
                Balance = accountDto.InitialDeposit,
                IsClosed = false
            };

            dbcontext.Accounts.Add(customerAccount);
            dbcontext.SaveChanges();

            accountDto.SetBalance(customerAccount.Balance);
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(accountDto), "application/json");
        }

        [Route("Deposit")]
        [HttpPut]
        public IActionResult  Deposit(DepositDto depositDto)
        {
            //add null case
            var account = dbcontext.Accounts
               .FirstOrDefault(a => a.AccountId == depositDto.AccountDepositedId && a.CustomerId == depositDto.CustomerId);
            if (account == null)
            {
                return NotFound();
            }
            depositDto.Succeeded = true;
            account.Balance += depositDto.DepositAmount;
            depositDto.Balance = account.Balance;
            dbcontext.SaveChanges();
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(depositDto), "application/json");
        }
    }


}
