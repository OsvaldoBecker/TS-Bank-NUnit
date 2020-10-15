using NUnit.Framework;
using System;
using BankUtils;

namespace BankTests
{
    class BankTest
    {
        // In this implementation, if an assert fails after creating an account, the database will not be cleaned

        [Test]
        public void TestCreateDuplicatedAccount()
        {
            Bank bank = new Bank("BNK01");

            bank.CreateAccount("Joao da Silva", "111-11-1111", 5000, "J55");
            Assert.Throws<InvalidOperationException>(() => bank.CreateAccount("Joao da Silva", "111-11-1111", 5000, "J55"));

            bank.DeleteAccount("111-11-1111", "J55", "BNK01"); // Clear database

            Assert.Pass();
        }

        [Test]
        public void TestDeleteNonexistentAccount()
        {
            Bank bank = new Bank("BNK01");

            Assert.Throws<InvalidOperationException>(() => bank.DeleteAccount("111-12-1111", "J55", "BNK01"));

            Assert.Pass();
        }

        [Test]
        public void TestLockAccount()
        {
            Bank bank = new Bank("BNK01");

            bank.CreateAccount("Joao da Silva", "111-13-1111", 5000, "J55");
            Assert.Throws<InvalidOperationException>(() => bank.MakeDeposit("111-13-1111", 5500));
            Assert.True(bank.GetAccountStatus("111-13-1111", "J55") == Account.AccountStatus.Locked);

            bank.DeleteAccount("111-13-1111", "J55", "BNK01"); // Clear database

            Assert.Pass();
        }

        [Test]
        public void TestUnlockAccount()
        {
            Bank bank = new Bank("BNK01");

            bank.CreateAccount("Joao da Silva", "111-14-1111", 5000, "J55");
            Assert.Throws<InvalidOperationException>(() => bank.MakeDeposit("111-14-1111", 5500));
            bank.UnlockAccount("111-14-1111", "BNK01");

            bank.DeleteAccount("111-14-1111", "J55", "BNK01"); // Clear database

            Assert.Pass();
        }

        [Test]
        public void TestAccountPassword()
        {
            Bank bank = new Bank("BNK01");

            bank.CreateAccount("Joao da Silva", "111-15-1111", 5000, "J55");
            Assert.Throws<InvalidOperationException>(() => bank.MakeWithdraw("111-15-1111", "J", 100));

            bank.DeleteAccount("111-15-1111", "J55", "BNK01"); // Clear database

            Assert.Pass();
        }

        [Test]
        public void TestMasterPassword()
        {
            Bank bank = new Bank("BNK01");

            bank.CreateAccount("Joao da Silva", "111-16-1111", 5000, "J55");
            Assert.Throws<InvalidOperationException>(() => bank.DeleteAccount("111-16-1111", "J55", "BNK"));

            bank.DeleteAccount("111-16-1111", "J55", "BNK01"); // Clear database

            Assert.Pass();
        }
    }
}
