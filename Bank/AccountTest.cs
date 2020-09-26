using NUnit.Framework;
using System;

namespace Test
{
    class AccountTest
    {
        [Test]
        public void TestTransactions()
        {
            Account account = new Account("Pedro da Silva", "363-38-3636", 10000, 2255);

            Assert.Throws<InvalidOperationException>(() => account.MakeWithdraw(100));
            Assert.Throws<InvalidOperationException>(() => account.MakeDeposit(-50));

            Assert.True(account.Balance == 0);
            account.MakeDeposit(100);
            Assert.True(account.Balance == 100);
            Assert.False(account.Balance == 50);
            account.MakeWithdraw(50);
            Assert.True(account.Balance == 50);
            Assert.True(account.Transactions.Count == 2);

            Assert.Pass();
        }

        [Test]
        public void TestTransactionLimit()
        {
            Account account = new Account("Pedro da Silva", "363-38-3636", 10000, 2255);

            Assert.True(account.Status == Account.AccountStatus.Unlocked);
            Assert.Throws<InvalidOperationException>(() => account.MakeDeposit(15000));
            Assert.True(account.Status == Account.AccountStatus.Locked);

            Assert.Pass();
        }
    }
}
