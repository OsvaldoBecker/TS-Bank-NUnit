using System;

namespace BankUtils
{
    class Bank
    {
        private DataBase dataBase;
        private string masterPassword;

        public Bank(string masterPassword)
        {
            CheckMasterPassword(masterPassword);

            dataBase = new DataBase();
            this.masterPassword = masterPassword;
        }

        public void CreateAccount(string accountOwnerName, string accountOwnerSSN, float accountTransactionLimitValue, string accountPassword)
        {
            // Parameters will be checked in Account constructor

            Account account = dataBase.FindAccount(accountOwnerSSN);

            if (account == null)
            {
                account = new Account(accountOwnerName, accountOwnerSSN, accountTransactionLimitValue, accountPassword);
                dataBase.InsertAccount(account);
            }
            else
                throw new InvalidOperationException("Creating account error: There is already an account with that SSN!");
        }

        public void DeleteAccount(string accountOwnerSSN, string accountPassword, string masterPassword)
        {
            CheckAccountOwnerSSN(accountOwnerSSN);
            CheckAccountPassword(accountPassword);
            CheckMasterPassword(masterPassword);

            if (this.masterPassword == masterPassword)
            {
                Account account = dataBase.FindAccount(accountOwnerSSN);

                if (account != null)
                {
                    if (accountPassword == account.Password)
                    {
                        if (account.Balance == 0)
                            dataBase.DeleteAccount(account);
                        else
                            throw new InvalidOperationException("Deleting account error: There is balance available in this account!");
                    }
                    else
                        throw new InvalidOperationException("Deleting account error: Incorrect account password!");
                }
                else
                    throw new InvalidOperationException("Deleting account error: Account not found!");
            }
            else
                throw new InvalidOperationException("Deleting account error: Incorrect master password!");
        }

        public void UnlockAccount(string accountOwnerSSN, string masterPassword)
        {
            CheckAccountOwnerSSN(accountOwnerSSN);
            CheckMasterPassword(masterPassword);

            if (this.masterPassword == masterPassword)
            {
                Account account = dataBase.FindAccount(accountOwnerSSN);

                if (account != null)
                {
                    try
                    {
                        account.Unlock();
                    }
                    finally
                    {
                        dataBase.UpdateAccount(account);
                    }
                }
                else
                    throw new InvalidOperationException("Unlocking account error: Account not found!");
            }
            else
                throw new InvalidOperationException("Unlocking account error: Incorrect master password!");
        }

        public Account.AccountStatus GetAccountStatus(string accountOwnerSSN, string accountPassword)
        {
            CheckAccountOwnerSSN(accountOwnerSSN);
            CheckAccountPassword(accountPassword);

            Account account = dataBase.FindAccount(accountOwnerSSN);

            if (account != null)
            {
                if (accountPassword == account.Password)
                    return account.Status;
                else
                    throw new InvalidOperationException("Getting account status error: Incorrect account password!");
            }
            else
                throw new InvalidOperationException("Getting account status error: Account not found!");
        }


        public void MakeDeposit(string accountOwnerSSN, float value)
        {
            CheckAccountOwnerSSN(accountOwnerSSN);

            Account account = dataBase.FindAccount(accountOwnerSSN);

            if (account != null)
            {
                try
                {
                    account.MakeDeposit(value);
                }
                finally
                {
                    dataBase.UpdateAccount(account);
                }
            }
            else
                throw new InvalidOperationException("Depositing: Account not found!");
        }

        public void MakeWithdraw(string accountOwnerSSN, string accountPassword, float value)
        {
            CheckAccountOwnerSSN(accountOwnerSSN);
            CheckAccountPassword(accountPassword);

            Account account = dataBase.FindAccount(accountOwnerSSN);

            if (account != null)
            {
                if (accountPassword == account.Password)
                {
                    try
                    {
                        account.MakeWithdraw(value);
                    }
                    finally
                    {
                        dataBase.UpdateAccount(account);
                    }
                }
                else
                    throw new InvalidOperationException("Withdrawing error: Incorrect account password!");
            }
            else
                throw new InvalidOperationException("Withdrawing error: Account not found!");
        }


        private void CheckAccountOwnerSSN(string SSN)
        {
            if (SSN.Length != 11)
                throw new InvalidOperationException("Withdrawing error: SSN must be 11 characters!");
        }

        private void CheckAccountPassword(string accountPassword)
        {
            if (accountPassword.Length != 3)
                throw new InvalidOperationException("Withdrawing error: Password must be 3 characters!");
        }

        private void CheckMasterPassword(string masterPassword)
        {
            if (masterPassword.Length != 5)
                throw new InvalidOperationException("Unlocking account error: Master password must be 5 characters!");
        }
    }
}

