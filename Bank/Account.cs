using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

[BsonIgnoreExtraElements]

class Account
{
    public string OwnerName { get; private set; }
    public string OwnerSSN { get; private set; }
    public float Balance { get; private set; }
    public string Password { get; private set; }

    public float TransactionLimitValue { get; private set; }
    public List<AccountTransaction> Transactions { get; private set; }

    public enum AccountStatus { Unlocked, Locked }
    public AccountStatus Status { get; private set; }


    public Account(string ownerName, string ownerSSN, float transactionLimitValue, string password)
    {
        if (ownerName.Length < 10)
            throw new InvalidOperationException("Creating account error: Name must have more characters!");
        if (ownerSSN.Length != 11)
            throw new InvalidOperationException("Creating account error: SSN must be 11 characters!");
        if (password.Length != 3)
            throw new InvalidOperationException("Creating account error: Password must be 3 characters!");
        if (transactionLimitValue < 100)
            throw new InvalidOperationException("Creating account error: Transaction limit value must be greater than or equal to 100!");

        OwnerName = ownerName;
        OwnerSSN = ownerSSN;
        Balance = 0;
        Password = password;

        TransactionLimitValue = transactionLimitValue;
        Transactions = new List<AccountTransaction>();

        Status = AccountStatus.Unlocked;
    }

    public void Unlock()
    {
        if (Status == AccountStatus.Unlocked)
            throw new InvalidOperationException("Unlocking account error: Account is already unlocked!");
        else
            Status = AccountStatus.Unlocked;
    }

    public void MakeDeposit(float value)
    {
        if (Status == AccountStatus.Locked)
            throw new InvalidOperationException("Depositing error: Operation not allowed with account inactive!");
        else
        {
            if (value <= 0)
                throw new InvalidOperationException("Depositing error: Invalid value!");
            else if (value > TransactionLimitValue)
            {
                Status = AccountStatus.Locked;
                throw new InvalidOperationException("Depositing error: Transaction limit value exceeded, account has been blocked!");
            }
            else
            {
                Balance += value;
                Transactions.Add(new AccountTransaction(AccountTransaction.AccountTransactionType.Deposit, value, DateTime.Now));
            }
        }
    }

    public void MakeWithdraw(float value)
    {
        if (Status == AccountStatus.Locked)
            throw new InvalidOperationException("Withdrawing error: Operation not allowed with account inactive!");
        else
        {
            if (value > Balance)
                throw new InvalidOperationException("Withdrawing error: Insufficient balance!");
            else if (value <= 0)
                throw new InvalidOperationException("Withdrawing error: Invalid value!");
            else if (value > TransactionLimitValue)
            {
                Status = AccountStatus.Locked;
                throw new InvalidOperationException("Withdrawing error: Transaction limit value exceeded, account has been blocked!");
            }
            else
            {
                Balance -= value;
                Transactions.Add(new AccountTransaction(AccountTransaction.AccountTransactionType.Withdraw, value, DateTime.Now));
            }
        }
    }
}