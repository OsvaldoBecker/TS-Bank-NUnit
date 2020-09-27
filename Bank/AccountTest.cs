using NUnit.Framework;
using System;

namespace Test
{
    class AccountTest
    {
        [Test]
        public void TestInvalidInfos()
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
        public void TestBalanceConsistence()
        {
            Account account = new Account("Pedro da Silva", "363-38-3636", 10000, "L6L");

            Assert.True(account.Balance == 0);
            account.MakeDeposit(100);
            Assert.True(account.Balance == 100);
            account.MakeWithdraw(50);
            Assert.True(account.Balance == 50);
            Assert.True(account.Transactions.Count == 2);

            Assert.Pass();
        }
    }
}
