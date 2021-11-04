using LittleBit.Modules.CoreModule;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class Resource : Data
{
    public Resource(string id)
    {
        Id = id;
    }

    public double Value;

    public readonly string Id;

    public Resource()
    {
        
    }
}