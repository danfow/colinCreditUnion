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

            if ((alreadyCustomer == null && accountDto.AccountTypeId == 1) || accountDto.InitialDeposit < 100)
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
        public IActionResult Deposit(DepositDto depositDto)
        {
            //add null case
            var account = dbcontext.Accounts
               .FirstOrDefault(a => a.AccountId == depositDto.AccountDepositedId && a.CustomerId == depositDto.CustomerId);
            if (account == null)
            {
                return NotFound();
            }
            if (depositDto.DepositAmount <= 0)
            {
                return BadRequest();
            }
            account.Balance += depositDto.DepositAmount;
            depositDto.SetBalance(account.Balance);
            dbcontext.SaveChanges();

            var depositPayload = new
            {
                customerId = depositDto.CustomerId,
                accountId = depositDto.AccountDepositedId,
                balance = depositDto.getBalance(),
                succeeded = true
            };
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(depositPayload), "application/json");
        }

        [Route("Withdrawl")]
        [HttpPut]
        public IActionResult Withdrawl(WithdrawlDto withdrawlDto)
        {
            //add null case
            var account = dbcontext.Accounts
               .FirstOrDefault(a => a.AccountId == withdrawlDto.AccountWithdrawledId && a.CustomerId == withdrawlDto.CustomerId);
            if (account == null)
            {
                return NotFound();
            }
            if (withdrawlDto.WithdrawlAmount <= 0)
            {
                return BadRequest();
            }
            if ((account.Balance - withdrawlDto.WithdrawlAmount) < 0)
            {
                return BadRequest();
            }
            account.Balance -= withdrawlDto.WithdrawlAmount;
            withdrawlDto.SetBalance(account.Balance);
            dbcontext.SaveChanges();

            var depositPayload = new
            {
                customerId = withdrawlDto.CustomerId,
                accountId = withdrawlDto.AccountWithdrawledId,
                balance = withdrawlDto.getBalance(),
                succeeded = true
            };
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(depositPayload), "application/json");
        }
        [Route("DeleteAccount")]
        [HttpDelete]
        public IActionResult DeleteAccount(string customerId, string accountId)
        {
            var account = dbcontext.Accounts
               .FirstOrDefault(a => a.AccountId == accountId && a.CustomerId == customerId);
            if (account == null)
            {
                return NotFound();
            }
            if (account.Balance != 0)
            {
                return BadRequest();
            }
            account.IsClosed = true;
            dbcontext.SaveChanges();
            var DeleteAccountPayload = new { customerId = customerId,
                accountId = accountId,isClosed = account.IsClosed,Succeeded = true};
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(DeleteAccountPayload), "application/json");
        }
    }
}







