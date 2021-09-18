using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUpdate : MonoBehaviour
{
    public Sprite _cardFace;
    public Sprite _cardBack;

    private SpriteRenderer _spriteRenderer;
    private Selectable _selectable;
    private DeckManager _deckManager;
    private UserInput _userInput;

    void Start()
    {
        List<string> deck = DeckManager.GenerateDeck();
        _deckManager = FindObjectOfType<DeckManager>();
        _userInput = FindObjectOfType<UserInput>();

        int i = 0;
        foreach(string card in deck)
        {
            if(this.name == card)
            {
                _cardFace = _deckManager.cardFaces[i];
                break;
            }
            i++;
        }
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _selectable = GetComponent<Selectable>();
    }


    void Update()
    {
        if (_selectable.faceUp == true)
        {
            _spriteRenderer.sprite = _cardFace;
        }
        else if (_selectable.faceUp == false)
        {
            _spriteRenderer.sprite = _cardBack;
        }

        if (_userInput._slot1)
        {
            if (name == _userInput._slot1.name)
            {
                foreach(GameObject bottom in GameObject.FindGameObjectsWithTag("Bottom"))
                {
                    bottom.layer = 0;
                }

                _spriteRenderer.color = Color.yellow;
            }
            else
            {
                foreach (GameObject bottom in GameObject.FindGameObjectsWithTag("Bottom"))
                {
                    bottom.layer = 2;
                }
                _spriteRenderer.color = Color.white;
            }
        }
    }
}
