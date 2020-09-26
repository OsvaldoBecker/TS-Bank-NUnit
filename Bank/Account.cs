using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

[BsonIgnoreExtraElements]

class Account
{
    public string OwnerName { get; private set; }
    public string OwnerSSN { get; private set; }
    public float Balance { get; private set; }

    public float TransactionLimitValue { get; private set; }
    public List<AccountTransaction> Transactions { get; private set; }

    public enum AccountStatus { Unlocked, Locked }
    public AccountStatus Status { get; private set; }
    public int Password { get; private set; }

    public Account(string ownerName, string ownerSSN, float transactionLimitValue, int password)
    {
        OwnerName = ownerName;
        OwnerSSN = ownerSSN;
        Balance = 0;

        TransactionLimitValue = transactionLimitValue;
        Transactions = new List<AccountTransaction>();

        Status = AccountStatus.Unlocked;
        Password = password;
    }


    public void SetUnlocked()
    {
        if (Status == AccountStatus.Unlocked)
            throw new InvalidOperationException("Error when unlocking: Account is already unlocked!");
        else
            Status = AccountStatus.Unlocked;
    }

    public void MakeDeposit(float value)
    {
        if (Status == AccountStatus.Locked)
            throw new InvalidOperationException("Error when depositing: Operation not allowed with account inactive!");
        else
        {
            if (value <= 0)
                throw new InvalidOperationException("Error when withdrawing: Invalid value!");
            else if (value > TransactionLimitValue)
            {
                Status = AccountStatus.Locked;
                throw new InvalidOperationException("Error when depositing: Transaction limit value exceeded, account has been blocked!");
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
            throw new InvalidOperationException("Error when withdrawing: Operation not allowed with account inactive!");
        else
        {
            if (value > Balance)
                throw new InvalidOperationException("Error when withdrawing: Insufficient balance!");
            else if (value <= 0)
                throw new InvalidOperationException("Error when withdrawing: Invalid value!");
            else if (value > TransactionLimitValue)
            {
                Status = AccountStatus.Locked;
                throw new InvalidOperationException("Error when withdrawing: Transaction limit value exceeded, account has been blocked!");
            }
            else
            {
                Balance -= value;
                Transactions.Add(new AccountTransaction(AccountTransaction.AccountTransactionType.Withdraw, value, DateTime.Now));
            }
        }
    }
}
