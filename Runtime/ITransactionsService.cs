using System.Collections.Generic;

public interface ITransactionsService
{
    void SendTransaction(string resourceID, double value, Transaction.Type type, string message);
    void SendTransaction(Transaction transaction);
    List<Transaction> GetTransactionsHistory();
}
