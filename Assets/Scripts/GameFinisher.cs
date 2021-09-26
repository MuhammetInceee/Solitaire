using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinisher : MonoBehaviour
{
    public Selectable[] topStacks;
    private MainUIButtonHandler _mainUIButtonHandler;
    [SerializeField] private GameObject _gameEndedScreen;

    private void Start()
    {
        _mainUIButtonHandler = FindObjectOfType<MainUIButtonHandler>();
    }
    void Update()
    {
        if (HasWon())
        {
            _gameEndedScreen.SetActive(true);
            _mainUIButtonHandler.GameWon();
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
