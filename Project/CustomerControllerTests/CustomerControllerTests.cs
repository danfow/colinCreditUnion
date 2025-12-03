using Microsoft.VisualStudio.TestTools.UnitTesting;
using colinCreditUnion.Controllers;
using colinCreditUnion.Data;
using colinCreditUnion.Models;
using colinCreditUnion.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace colinCreditUnion.Tests.Controllers
{
    [TestClass]
    public class CustomerControllerTests
    {
        private colinCreditUnionDbContext GetInMemoryDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<colinCreditUnionDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            return new colinCreditUnionDbContext(options);
        }

        [TestMethod]
        public void AddAccount_ReturnsBadRequest_WhenInitialDepositIsLessThan100()
        {
            using var context = GetInMemoryDbContext(nameof(AddAccount_ReturnsBadRequest_WhenInitialDepositIsLessThan100));
            var controller = new CustomerController(context);
            var dto = new AccountDto { CustomerId = "C1", AccountTypeId = 1, InitialDeposit = 50, AccountId = "A1", IsClosed = false };

            var result = controller.AddAccount(dto);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AddAccount_ReturnsContentResult_WhenAccountIsCreated()
        {
            using var context = GetInMemoryDbContext(nameof(AddAccount_ReturnsContentResult_WhenAccountIsCreated));
            var controller = new CustomerController(context);
            var dto = new AccountDto { CustomerId = "C2", AccountTypeId = 2, InitialDeposit = 200, AccountId = "A2", IsClosed = false };

            var result = controller.AddAccount(dto);

            Assert.IsInstanceOfType(result, typeof(ContentResult));
        }

        [TestMethod]
        public void Deposit_ReturnsNotFound_WhenAccountDoesNotExist()
        {
            using var context = GetInMemoryDbContext(nameof(Deposit_ReturnsNotFound_WhenAccountDoesNotExist));
            var controller = new CustomerController(context);
            var dto = new DepositDto { CustomerId = "C3", AccountDepositedId = "A3", DepositAmount = 100 };

            var result = controller.Deposit(dto);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Deposit_ReturnsBadRequest_WhenDepositAmountIsZeroOrNegative()
        {
            using var context = GetInMemoryDbContext(nameof(Deposit_ReturnsBadRequest_WhenDepositAmountIsZeroOrNegative));
            context.Accounts.Add(new Account { AccountId = "A4", AccountTypeId = 2, CustomerId = "C4", Balance = 100, IsClosed = false });
            context.SaveChanges();
            var controller = new CustomerController(context);
            var dto = new DepositDto { CustomerId = "C4", AccountDepositedId = "A4", DepositAmount = 0 };

            var result = controller.Deposit(dto);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Deposit_ReturnsContentResult_WhenDepositIsSuccessful()
        {
            using var context = GetInMemoryDbContext(nameof(Deposit_ReturnsContentResult_WhenDepositIsSuccessful));
            context.Accounts.Add(new Account { AccountId = "A5", AccountTypeId = 2, CustomerId = "C5", Balance = 100, IsClosed = false });
            context.SaveChanges();
            var controller = new CustomerController(context);
            var dto = new DepositDto { CustomerId = "C5", AccountDepositedId = "A5", DepositAmount = 50 };

            var result = controller.Deposit(dto);

            Assert.IsInstanceOfType(result, typeof(ContentResult));
        }

        [TestMethod]
        public void Withdrawl_ReturnsNotFound_WhenAccountDoesNotExist()
        {
            using var context = GetInMemoryDbContext(nameof(Withdrawl_ReturnsNotFound_WhenAccountDoesNotExist));
            var controller = new CustomerController(context);
            var dto = new WithdrawlDto { CustomerId = "C6", AccountWithdrawledId = "A6", WithdrawlAmount = 10 };

            var result = controller.Withdrawl(dto);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Withdrawl_ReturnsBadRequest_WhenWithdrawlAmountIsZeroOrNegative()
        {
            using var context = GetInMemoryDbContext(nameof(Withdrawl_ReturnsBadRequest_WhenWithdrawlAmountIsZeroOrNegative));
            context.Accounts.Add(new Account { AccountId = "A7", CustomerId = "C7", AccountTypeId = 2, Balance = 100, IsClosed = false });
            context.SaveChanges();
            var controller = new CustomerController(context);
            var dto = new WithdrawlDto { CustomerId = "C7", AccountWithdrawledId = "A7", WithdrawlAmount = 0 };

            var result = controller.Withdrawl(dto);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Withdrawl_ReturnsBadRequest_WhenInsufficientBalance()
        {
            using var context = GetInMemoryDbContext(nameof(Withdrawl_ReturnsBadRequest_WhenInsufficientBalance));
            context.Accounts.Add(new Account { AccountId = "A8", CustomerId = "C8", Balance = 50, AccountTypeId = 2, IsClosed = false });
            context.SaveChanges();
            var controller = new CustomerController(context);
            var dto = new WithdrawlDto { CustomerId = "C8", AccountWithdrawledId = "A8", WithdrawlAmount = 100 };

            var result = controller.Withdrawl(dto);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void Withdrawl_ReturnsContentResult_WhenWithdrawlIsSuccessful()
        {
            using var context = GetInMemoryDbContext(nameof(Withdrawl_ReturnsContentResult_WhenWithdrawlIsSuccessful));
            context.Accounts.Add(new Account { AccountId = "A9", CustomerId = "C9", Balance = 200, AccountTypeId = 2, IsClosed = false });
            context.SaveChanges();
            var controller = new CustomerController(context);
            var dto = new WithdrawlDto { CustomerId = "C9", AccountWithdrawledId = "A9", WithdrawlAmount = 50 };

            var result = controller.Withdrawl(dto);

            Assert.IsInstanceOfType(result, typeof(ContentResult));
        }

        [TestMethod]
        public void DeleteAccount_ReturnsNotFound_WhenAccountDoesNotExist()
        {
            using var context = GetInMemoryDbContext(nameof(DeleteAccount_ReturnsNotFound_WhenAccountDoesNotExist));
            var controller = new CustomerController(context);
            var result = controller.DeleteAccount("C10", "A10");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteAccount_ReturnsBadRequest_WhenBalanceIsNotZero()
        {
            using var context = GetInMemoryDbContext(nameof(DeleteAccount_ReturnsBadRequest_WhenBalanceIsNotZero));
            context.Accounts.Add(new Account { AccountId = "A11", CustomerId = "C11", Balance = 100, AccountTypeId = 2, IsClosed = false });
            context.SaveChanges();
            var controller = new CustomerController(context);
            var result = controller.DeleteAccount("C11", "A11");

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public void DeleteAccount_ReturnsContentResult_WhenAccountIsClosed()
        {
            using var context = GetInMemoryDbContext(nameof(DeleteAccount_ReturnsContentResult_WhenAccountIsClosed));
            context.Accounts.Add(new Account { AccountId = "A12", CustomerId = "C12", Balance = 0, AccountTypeId = 2, IsClosed = false });
            context.SaveChanges();
            var controller = new CustomerController(context);
            var result = controller.DeleteAccount("C12", "A12");

            Assert.IsInstanceOfType(result, typeof(ContentResult));
        }
    }
}