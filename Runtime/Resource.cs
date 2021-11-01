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

    public double Value
    {
        get => _value;
        set
        {
            _value = value;
            if (_value < 0) _value = 0;
        }
    }

    private double _value;

    public readonly string Id;

    public Resource()
    {
        
    }
}