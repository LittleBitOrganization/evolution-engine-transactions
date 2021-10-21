using System.Collections.Generic;

public interface ITransactionsService
{
    void SendTransaction(string currencyId, float value, Transaction.Type type, string message);
    void SendTransaction(string currencyId, float value, Transaction.Type type);
    void SendTransaction(Transaction transaction);
    List<Transaction> GetTransactionsHistory();
    string RandomTransactionId();
}