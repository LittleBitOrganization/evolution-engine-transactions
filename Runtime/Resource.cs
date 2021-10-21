using UnityEngine;
using UnityEngine.Events;

public class Resource
{
    public UnityEvent<float> onChanged;
    public Resource(string id)
    {
        onChanged = new UnityEvent<float>();
        Id = id;
    }

    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            if (_value < 0) _value = 0;
            onChanged?.Invoke(_value);
        }
    }

    private float _value;

    public readonly string Id;
}