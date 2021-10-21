using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class TransactionsService : ITransactionsService
{
    private List<Transaction> _transactionsHistory;
    private ITransactionsContainer _transactionsContainer;

    private const string _rndStringGlyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPRSTUVWXYZ0123456789";
    private int _rndStringGlyphsLength;
    private const int _rndStringLength = 12;

    public TransactionsService(ITransactionsContainer container)
    {
        _transactionsContainer = container;

        _transactionsHistory = new List<Transaction>();

        _rndStringGlyphsLength = _rndStringGlyphs.Length;
    }

    public void SendTransaction(string currencyId, float value, Transaction.Type type, string message)
    {
        var transaction = new Transaction(RandomTransactionId(), currencyId, value, type, message);

        if (!CheckTransaction(transaction)) return;

        AddTransactionToTransactionsContainer(transaction);
    }

    public void SendTransaction(string currencyId, float value, Transaction.Type type)
    {
        var transaction = new Transaction(RandomTransactionId(), currencyId, value, type, "");

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

    public string RandomTransactionId()
    {
        var stringBuilder = new StringBuilder("");

        for (int i = 0; i < _rndStringLength; i++)
        {
            stringBuilder.Append(_rndStringGlyphs[Random.Range(0, _rndStringGlyphsLength)]);
        }

        return stringBuilder.ToString();
    }

    private void AddTransactionToTransactionsContainer(Transaction transaction)
    {
        Debug.Log(string.Format("Transaction [Id = '{0}']  was successfully sended!", transaction.Id));

        _transactionsContainer.AddTransaction(transaction);
        _transactionsHistory.Add(transaction);

        transaction.SendedTimeStamp = DateTime.Now.ToString();
    }

    private bool CheckTransaction(Transaction transaction)
    {
        if (string.IsNullOrEmpty(transaction.Id))
        {
            Debug.Log("Transaction Id is empty, can't send it");
            return false;
        }

        return true;
    }
}