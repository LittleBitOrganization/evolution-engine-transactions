using System.Text;
using UnityEngine;

public class TransactionIDGenerator : ITransactionIDGenerator
{
    private const string _rndStringGlyphs = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPRSTUVWXYZ0123456789";
    private int _rndStringGlyphsLength;
    
    public TransactionIDGenerator()
    {
        _rndStringGlyphsLength = _rndStringGlyphs.Length;
    }

    public string Generate(int length)
    {
        var stringBuilder = new StringBuilder("");

        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(_rndStringGlyphs[Random.Range(0, _rndStringGlyphsLength)]);
        }

        return stringBuilder.ToString();
    }
}