using NUnit.Framework;
using System;

namespace Test
{
    class BankTest
    {
        [Test]
        public void CreateDuplicatedAccountTest()
        {
            Bank bank = new Bank(02525);

            bank.CreateAccount("Joao", "123-69-7458", 2500, 232);
            Assert.Throws<InvalidOperationException>(() => bank.CreateAccount("Joao", "123-69-7458", 2500, 232));

            Assert.Pass();
        }

        [Test]
        public void DeleteNonexistentAccountTest()
        {
            Bank bank = new Bank(02525);

            Assert.Throws<InvalidOperationException>(() => bank.DeleteAccount("445-22-1258", 789, 02525));

            Assert.Pass();
        }

        [Test]
        public void AccountPasswordTest()
        {
            Bank bank = new Bank(02525);

            bank.CreateAccount("Maria", "652-19-1023", 7500, 455);
            bank.MakeDeposit("652-19-1023", 100);
            bank.MakeWithdraw("652-19-1023", 455, 10);

            Assert.Throws<InvalidOperationException>(() => bank.MakeWithdraw("652-19-1023", 400, 10));

            Assert.Pass();
        }

        [Test]
        public void MasterPasswordTest()
        {
            Bank bank = new Bank(78780);

            bank.CreateAccount("Joana", "023-55-9658", 4000, 632);
            Assert.Throws<InvalidOperationException>(() => bank.DeleteAccount("023-55-9658", 632, 78788));
            bank.DeleteAccount("023-55-9658", 632, 78780);

            Assert.Pass();
        }
    }
}
