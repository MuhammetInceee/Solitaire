using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    public Selectable[] topStacks;

    void Update()
    {
        if (HasWon())
        {

        }
    }
    public bool HasWon()

    {
        int i = 0;
        foreach (Selectable topstack in topStacks)
        {
            i += topstack._value;
        }
        if (i >= 52)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
