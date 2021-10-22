using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesHandler 
{
    public ResourcesHandler(List<Resource> resources, ITransactionsContainer container)
    {
        _transactionsContainer = container;
        _resources = resources;
        
        SubscribeToTransactionsContainer();
    }

    private List<Resource> _resources;
    private ITransactionsContainer _transactionsContainer;
    
    private void SubscribeToTransactionsContainer()
    {
        _transactionsContainer.AddOnTransactionReceivedListener(OnTransactionReceived);
    }

    private void OnTransactionReceived()
    {
        var transaction = _transactionsContainer.GetTransaction();
        
        ExecuteTransaction(transaction);
    }
    
    private void ExecuteTransaction(Transaction transaction)
    {
        var resource = FindResourceById(transaction.ResourceId);

        if (resource == null)
        {
            Debug.Log(string.Format("Transaction [Id = '{0}'] wasn't executed because can't find resource with [Id = '{1}']", transaction.Id, transaction.ResourceId));
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
        
        Debug.Log(string.Format("Transaction [Id = '{0}'] was successfully executed!\n Message:{1}", transaction.Id, transaction.Message, transaction.TransactionType));
        
        transaction.TransactionResult = Transaction.Result.Success;
        transaction.ExecutedTimeStamp = DateTime.Now.ToString();
        
        return;
    }

    private Resource FindResourceById(string resourceId)
    {
        return _resources.FirstOrDefault(resource => resource.Id == resourceId);
    }
    
    public float GetResourceValue(string resourceId)
    {
        var resource = FindResourceById(resourceId);

        if (resource == null)
        {
            Debug.LogError(string.Format("Can't find resource with [ID = '{0}']", resourceId));
            return 0;
        }

        return resource.Value;
    }

    public void AddResourceOnChangedListener(string resourceId, UnityAction<float> action)
    {
        var resource = FindResourceById(resourceId);

        if (resource == null)
        {
            Debug.Log(string.Format("Can't add listener to resource because there is no resource with [ID = '{0}']", resourceId));
            return;
        }
        
        resource.onChanged.AddListener(action);
    }
    
    public void RemoveResourceOnChangedListener(string resourceId, UnityAction<float> action)
    {
        var resource = FindResourceById(resourceId);

        if (resource == null)
        {
            Debug.Log(string.Format("Can't remove listener from resource because there is no resource with [ID = '{0}']", resourceId));
            return;
        }
        
        resource.onChanged.RemoveListener(action);
    }
}