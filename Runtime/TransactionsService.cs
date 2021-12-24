using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

public class TransactionsService : ITransactionsService
{
    private List<Transaction> _transactionsHistory;
    private ITransactionsContainer _transactionsContainer;

    private const int _idLength = 16;
    private TransactionIDGenerator _transactionIdGenerator;

    [Preserve]
    public TransactionsService(ITransactionsContainer container)
    {
        _transactionsContainer = container;

        _transactionsHistory = new List<Transaction>();
        _transactionIdGenerator = new TransactionIDGenerator();
    }


    public void SendTransaction(string resourceID, double value, Transaction.Type type, string message = "")
    {
        var transaction =
            new Transaction(_transactionIdGenerator.Generate(_idLength), resourceID, value, type, message);

        if (!CheckTransaction(transaction)) return;

        AddTransactionToTransactionsContainer(transaction);
    }

    public void SendTransaction(Transaction transaction)
    {
        if (!CheckTransaction(transaction)) return;

        AddTransactionToTransactionsContainer(transaction);
    }

    public List<Transaction> GetTransactionsHistory()
    {
        return _transactionsHistory;
    }

    private void AddTransactionToTransactionsContainer(Transaction transaction)
    {
        // Debug.Log(string.Format("Transaction [Id = '{0}']  was successfully sended!", transaction.Id));

        _transactionsContainer.AddTransaction(transaction);
        _transactionsHistory.Add(transaction);

        transaction.SendedTimeStamp = DateTime.Now.ToString();
    }

    private bool CheckTransaction(Transaction transaction)
    {
        if (string.IsNullOrEmpty(transaction.Id))
        {
            // Debug.Log("Transaction Id is empty, can't send it");
            return false;
        }

        return true;
    }
}