using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    private DeckManager _deckManager;

    void Start()
    {
        _deckManager = FindObjectOfType<DeckManager>();
    }

    void Update()
    {
        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Deck"))
                {
                    Deck();
                }
                if (hit.collider.CompareTag("Card"))
                {
                    Card();
                }
                if (hit.collider.CompareTag("Top"))
                {
                    Top();
                }
                if (hit.collider.CompareTag("Bottom"))
                {
                    Bottom();
                }
            }
        }
    }

    void Deck()
    {
        print("Click Deck");
        _deckManager.DealFromDeck();
    }
    void Card()
    {
        print("Click Card");
    }
    void Top()
    {
        print("Click Top");
    }
    void Bottom()
    {
        print("Click Bottom");
    }
}