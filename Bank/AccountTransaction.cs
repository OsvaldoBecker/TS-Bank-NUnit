using System;

namespace BankUtils
{
    class AccountTransaction
    {
        public enum AccountTransactionType { Deposit, Withdraw }

        public AccountTransactionType TransactionType { get; private set; }
        public float Value { get; private set; }
        public DateTime Date { get; private set; }

        public AccountTransaction(AccountTransactionType transactionType, float value, DateTime date)
        {
            TransactionType = transactionType;
            Value = value;
            Date = date;
        }
    }
}