using NUnit.Framework;
using System;
using BankUtils;

namespace BankTests
{
    class AccountTest
    {
        [Test]
        public void TestInvalidName()
        {
            Account account = null;

            Assert.Throws<InvalidOperationException>(() => account = new Account("Pedro", "111-11-1111", 10000, "L6L"));

            Assert.Pass();
        }

        [Test]
        public void TestInvalidSSN()
        {
            Account account = null;

            Assert.Throws<InvalidOperationException>(() => account = new Account("Pedro da Silva", "363--3636", 10000, "L6L"));

            Assert.Pass();
        }

        [Test]
        public void TestInvalidTransactionLimitValue()
        {
            Account account = null;

            Assert.Throws<InvalidOperationException>(() => account = new Account("Pedro da Silva", "111-11-1111", -50, "L6L"));

            Assert.Pass();
        }

        [Test]
        public void TestInvalidPassword()
        {
            Account account = null;

            Assert.Throws<InvalidOperationException>(() => account = new Account("Pedro da Silva", "111-11-1111", 10000, "6L"));

            Assert.Pass();
        }

        [Test]
        public void TestInsufficientBalanceWithdraw()
        {
            Account account = new Account("Pedro da Silva", "111-11-1111", 10000, "L6L");

            Assert.Throws<InvalidOperationException>(() => account.MakeWithdraw(50));

            Assert.Pass();
        }

        [Test]
        public void TestNegativeValueDeposit()
        {
            Account account = new Account("Pedro da Silva", "111-11-1111", 10000, "L6L");

            Assert.Throws<InvalidOperationException>(() => account.MakeDeposit(-50));

            Assert.Pass();
        }

        [Test]
        public void TestLimitValueTransaction()
        {
            Account account = new Account("Pedro da Silva", "111-11-1111", 10000, "L6L");

            Assert.Throws<InvalidOperationException>(() => account.MakeDeposit(15000));

            Assert.Pass();
        }

        [Test]
        public void TestBalanceConsistency()
        {
            Account account = new Account("Pedro da Silva", "111-11-1111", 10000, "L6L");

            account.MakeDeposit(50);
            account.MakeWithdraw(50);
            Assert.True(account.Balance == 0);

            Assert.Pass();
        }

        [Test]
        public void TestTransactionsListConsistency()
        {
            Account account = new Account("Pedro da Silva", "111-11-1111", 10000, "L6L");

            account.MakeDeposit(50);
            account.MakeWithdraw(50);
            Assert.True(account.Transactions.Count == 2);

            Assert.Pass();
        }
    }
}
