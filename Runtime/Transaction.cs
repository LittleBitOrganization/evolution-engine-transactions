using System;
using System.Collections;

public class Transaction
{
    public readonly string Id;
    public readonly string ResourceId;
    public readonly string Message;
    public readonly Type TransactionType;
    public readonly double Value;
    public string SendedTimeStamp;
    public string ExecutedTimeStamp;
    public Result TransactionResult;

    public enum Type
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public enum Result
    {
        Success,
        Fail
    }
    

    public Transaction(string id, string resourceId, double value, Type type, string message)
    {
        Value = value;
        TransactionType = type;
        Message = message;
        ResourceId = resourceId;
        Id = id;
    }
}