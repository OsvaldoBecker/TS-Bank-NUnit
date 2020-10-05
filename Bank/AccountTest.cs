﻿using NUnit.Framework;
using System;
using BankUtils;

namespace BankTests
{
    class AccountTest
    {
        [Test]
        public void TestBalanceConsistency()
        {
            Account account = new Account("Pedro da Silva", "363-38-3636", 10000, "L6L");

            account.MakeDeposit(100);
            account.MakeWithdraw(50);
            Assert.True(account.Balance == 50);

            Assert.Pass();
        }

        [Test]
        public void TestInvalidParameters()
        {
            Account account = null;

            Assert.Throws<InvalidOperationException>(() => account = new Account("Pedro", "363-38-3636", 10000, "L6L"));
            Assert.Throws<InvalidOperationException>(() => account = new Account("Pedro da Silva", "363--3636", 10000, "L6L"));
            Assert.Throws<InvalidOperationException>(() => account = new Account("Pedro da Silva", "363-38-3636", -50, "L6L"));
            Assert.Throws<InvalidOperationException>(() => account = new Account("Pedro da Silva", "363-38-3636", 10000, "6L"));

            Assert.Pass();
        }

        [Test]
        public void TestInvalidTransactions()
        {
            Account account = new Account("Pedro da Silva", "363-38-3636", 10000, "L6L");

            Assert.Throws<InvalidOperationException>(() => account.MakeWithdraw(50));
            Assert.Throws<InvalidOperationException>(() => account.MakeDeposit(-50));

            Assert.Pass();
        }

        [Test]
        public void TestLimitValueTransaction()
        {
            Account account = new Account("Pedro da Silva", "363-38-3636", 10000, "L6L");

            Assert.Throws<InvalidOperationException>(() => account.MakeDeposit(15000));
            Assert.True(account.Status == Account.AccountStatus.Locked);

            Assert.Pass();
        }

        [Test]
        public void TestTransactionsListConsistency()
        {
            Account account = new Account("Pedro da Silva", "363-38-3636", 10000, "L6L");

            Assert.True(account.Transactions.Count == 0);
            account.MakeDeposit(50);
            account.MakeDeposit(100);
            account.MakeWithdraw(50);
            Assert.True(account.Transactions.Count == 3);

            int deposits = 0;
            int withdraws = 0;

            foreach (var transaction in account.Transactions)
            {
                if (transaction.TransactionType == AccountTransaction.AccountTransactionType.Deposit)
                    deposits++;
                else if (transaction.TransactionType == AccountTransaction.AccountTransactionType.Withdraw)
                    withdraws++;
            }

            Assert.True(deposits == 2 && withdraws == 1);

            Assert.Pass();
        }
    }
}
