using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting;

public class TransactionsContainer : ITransactionsContainer
{
    [Preserve]
    public TransactionsContainer()
    {
        _transactions = new Queue<Transaction>();
        _onTransactionReceived = new UnityEvent();
    }

    private UnityEvent _onTransactionReceived;

    private Queue<Transaction> _transactions;

    public void AddTransaction(Transaction transaction)
    {
        _transactions.Enqueue(transaction);
        _onTransactionReceived?.Invoke();
    }

    public Transaction GetTransaction()
    {
        return _transactions.Dequeue();
    }

    public void AddOnTransactionReceivedListener(UnityAction action)
    {
        _onTransactionReceived.AddListener(action);
    }
    
    public void RemoveOnTransactionReceivedListener(UnityAction action)
    {
        _onTransactionReceived.RemoveListener(action);
    }
}