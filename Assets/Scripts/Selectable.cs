using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool _top = false;
    public string _suit;
    public int _value;
    public int _row;
    public bool faceUp = false;
    public bool _inDeckPile = false;

    private string _valueString;
    void Start()
    {
        if (CompareTag("Card"))
        {
            _suit = transform.name[0].ToString();

            for(int i = 1; i < transform.name.Length; i++)
            {
                char c = transform.name[i];
                _valueString += c.ToString();
            }

            if(_valueString == "A")
            {
                _value = 1;
            }
            if (_valueString == "2")
            {
                _value = 2;
            }
            if (_valueString == "3")
            {
                _value = 3;
            }
            if (_valueString == "4")
            {
                _value = 4;
            }
            if (_valueString == "5")
            {
                _value = 5;
            }
            if (_valueString == "6")
            {
                _value = 6;
            }
            if (_valueString == "7")
            {
                _value = 7;
            }
            if (_valueString == "8")
            {
                _value = 8;
            }
            if (_valueString == "9")
            {
                _value = 9;
            }
            if (_valueString == "10")
            {
                _value = 10;
            }
            if (_valueString == "J")
            {
                _value = 11;
            }
            if (_valueString == "Q")
            {
                _value = 12;
            }
            if (_valueString == "K")
            {
                _value = 13;
            }
        }
    }
}
