using NUnit.Framework;
using System;

namespace Test
{
    class BankTest
    {
        [Test]
        public void TestCreateDuplicatedAccount()
        {
            Bank bank = new Bank("BNK01");

            bank.CreateAccount("Joao da Silva", "123-54-9876", 2500, "J55");
            Assert.Throws<InvalidOperationException>(() => bank.CreateAccount("Joao da Silva", "123-54-9876", 2500, "J55"));

            bank.DeleteAccount("123-54-9876", "J55", "BNK01"); // Clear data base

            Assert.Pass();
        }

        [Test]
        public void TestDeleteNonexistentAccount()
        {
            Bank bank = new Bank("BNK01");

            Assert.Throws<InvalidOperationException>(() => bank.DeleteAccount("123-54-9876", "J55", "BNK01"));

            Assert.Pass();
        }

        [Test]
        public void TestAccountPassword()
        {
            Bank bank = new Bank("BNK01");

            bank.CreateAccount("Joao da Silva", "123-54-9876", 2500, "J55");
            bank.MakeDeposit("123-54-9876", 100);
            Assert.Throws<InvalidOperationException>(() => bank.MakeWithdraw("123-54-9876", "J", 100));
            bank.MakeWithdraw("123-54-9876", "J55", 100);

            bank.DeleteAccount("123-54-9876", "J55", "BNK01"); // Clear data base

            Assert.Pass();
        }

        [Test]
        public void TestMasterPassword()
        {
            Bank bank = new Bank("BNK01");

            bank.CreateAccount("Joao da Silva", "123-54-9876", 2500, "J55");
            Assert.Throws<InvalidOperationException>(() => bank.DeleteAccount("123-54-9876", "J55", "BNK"));
            bank.DeleteAccount("123-54-9876", "J55", "BNK01");

            Assert.Pass();
        }
    }
}
