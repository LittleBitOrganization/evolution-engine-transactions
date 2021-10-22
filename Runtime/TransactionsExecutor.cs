using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Object = System.Object;

public class TransactionsExecutor
{
    public TransactionsExecutor(ITransactionsContainer container, IDataStorageService dataStorageService)
    {
        _dataStorageService = dataStorageService;
        _transactionsContainer = container;

        SubscribeToTransactionsContainer();
    }

    private ITransactionsContainer _transactionsContainer;
    private IDataStorageService _dataStorageService;

    private void SubscribeToTransactionsContainer()
    {
        _transactionsContainer.AddOnTransactionReceivedListener(OnTransactionReceived);
    }

    private void OnTransactionReceived()
    {
        var transaction = _transactionsContainer.GetTransaction();

        ExecuteTransaction(transaction);
    }

    protected virtual void ExecuteTransaction(Transaction transaction)
    {
        var resource = FindResourceById(transaction.ResourceId);

        if (resource == null)
        {
            Debug.Log(string.Format(
                "Transaction [Id = '{0}'] wasn't executed because can't find resource with [Id = '{1}']",
                transaction.Id, transaction.ResourceId));
            transaction.TransactionResult = Transaction.Result.Fail;
            return;
        }

        switch (transaction.TransactionType)
        {
            case Transaction.Type.Add:
                resource.Value += transaction.Value;
                break;
            case Transaction.Type.Subtract:
                resource.Value -= transaction.Value;
                break;
            case Transaction.Type.Multiply:
                resource.Value *= transaction.Value;
                break;
            case Transaction.Type.Divide:
                resource.Value /= transaction.Value;
                break;
        }

        Debug.Log(string.Format("Transaction [Id = '{0}'] was successfully executed!\n Message:{1}", transaction.Id,
            transaction.Message, transaction.TransactionType));

        transaction.TransactionResult = Transaction.Result.Success;
        transaction.ExecutedTimeStamp = DateTime.Now.ToString();

        return;
    }

    private Resource FindResourceById(string resourceId)
    {
        _dataStorageService.GetData<Resource>(resourceId);
        return _dataStorageService.GetData<Resource>(resourceId);
    }
}

// public interface IDataStorageService : IService
// {
//     public T GetData<T>(string key);
//     public void SetData<T>(string key, T data);
//     public void AddUpdateDataListener(string key, Action onUpdateData);
//     public void RemoveUpdateDataListener(string key, Action onUpdateData);
// }