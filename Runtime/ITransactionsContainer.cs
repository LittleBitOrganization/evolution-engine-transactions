using UnityEngine.Events;

public interface ITransactionsContainer
{
    void AddTransaction(Transaction transaction);
    Transaction GetTransaction();
    void AddOnTransactionReceivedListener(UnityAction action);
    void RemoveOnTransactionReceivedListener(UnityAction action);
}