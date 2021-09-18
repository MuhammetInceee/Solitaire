using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject _slot1;
    private DeckManager _deckManager;

    void Start()
    {
        _deckManager = FindObjectOfType<DeckManager>();
        _slot1 = this.gameObject;
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
                    Card(hit.collider.gameObject);
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
    void Card(GameObject selected)
    {
        print("Click Card");

        if(_slot1 == this.gameObject)
        {
            _slot1 = selected;
        }
        else if(_slot1 != selected)
        {
            _slot1 = selected;
        }
    }
    void Top()
    {
        print("Click Top");
    }
    void Bottom()
    {
        print("Click Bottom");
    }

    bool Stackable(GameObject selected)
    {
        Selectable s1 = _slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();
    }
}
