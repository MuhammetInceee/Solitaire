using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UserInput : MonoBehaviour
{
    public GameObject _slot1;
    private DeckManager _deckManager;

    private float _timer;
    private float _doubleClickTime = 0.3f;
    private int _clickCount = 0;
    void Start()
    {
        _deckManager = FindObjectOfType<DeckManager>();
        _slot1 = this.gameObject;
    }

    void Update()
    {
        if (_clickCount == 1)
            _timer += Time.deltaTime;
        if(_clickCount == 3)
        {
            _timer = 0;
            _clickCount = 1;
        }
        if(_timer > _doubleClickTime)
        {
            _timer = 0;
            _clickCount = 0;
        }


        GetMouseClick();
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _clickCount++;

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
                    Top(hit.collider.gameObject);
                }
                if (hit.collider.CompareTag("Bottom"))
                {
                    Bottom(hit.collider.gameObject);
                }
            }
        }
    }

    void Deck()
    {
        print("Click Deck");
        _deckManager.DealFromDeck();
        _slot1 = this.gameObject;
    }
    void Card(GameObject selected)
    {
        print("Click Card");

        if (!selected.GetComponent<Selectable>().faceUp)
        {
            if (!Blocked(selected))
            {
                selected.GetComponent<Selectable>().faceUp = true;
                _slot1 = this.gameObject;
            }
        }
        else if (selected.GetComponent<Selectable>()._inDeckPile)
        {
            if (!Blocked(selected))
            {
                if (_slot1 == selected)
                {
                    if (DoubleClick())
                    {
                        AutoStack(selected);
                    }
                }
                else
                {
                    _slot1 = selected;
                }
            }
        }
        else
        {

            if (_slot1 == this.gameObject)
            {
                _slot1 = selected;
            }
            else if (_slot1 != selected)
            {
                if (Stackable(selected))
                {
                    Stack(selected);
                }
                else
                {
                    _slot1 = selected;
                }
            }

            else if(_slot1 == selected)
            {
                if (DoubleClick())
                {
                    AutoStack(selected);
                }
            }
        }
        
    }
    void Top(GameObject selected)
    {
        print("Click Top");

        if (_slot1.CompareTag("Card"))
        {
            if(_slot1.GetComponent<Selectable>()._value == 1)
            {
                Stack(selected);
            }
        }
    }
    void Bottom(GameObject selected)
    {
        print("Click Bottom");
        if (_slot1.CompareTag("Card"))
        {
            if(_slot1.GetComponent<Selectable>()._value == 13)
            {
                Stack(selected);
            }
        }
    }

    bool Stackable(GameObject selected)
    {
        Selectable s1 = _slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();

        if (!s2._inDeckPile)
        {
            if (s2._top)
            {
                if (s1._suit == s2._suit || (s1._value == 1 && s2._suit == null))
                {
                    if (s1._value == s2._value + 1)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (s1._value == s2._value - 1)
                {
                    bool card1Red = true;
                    bool card2Red = true;

                    if (s1._suit == "C" || s1._suit == "S")
                    {
                        card1Red = false;
                    }

                    if (s2._suit == "C" || s2._suit == "S")
                    {
                        card2Red = false;
                    }

                    if (card1Red == card2Red)
                    {
                        print("Not Stackable");
                        return false;
                    }
                    else
                    {
                        print("Stackable");
                        return true;
                    }
                }
            }
        }
        return false;
    }

    void Stack(GameObject selected)
    {
        if(DeckManager._levelSelector == 1)
        {
            Counter._score += 2;
        }
        else if(DeckManager._levelSelector == 2)
        {
            Counter._score += 5;
        }
        else if(DeckManager._levelSelector == 3)
        {
            Counter._score += 10;
        }
        Selectable s1 = _slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();
        float yOffset = 0.3f;

        if(s2._top || (!s2._top && s1._value == 13))
        {
            yOffset = 0;
        }

        _slot1.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - yOffset, selected.transform.position.z - 0.01f);
        _slot1.transform.parent = selected.transform;

        if (s1._inDeckPile)
        {
            _deckManager._tripsOnDisplay.Remove(_slot1.name);
        }
        else if(s1._top && s2._top && s1._value == 1)
        {
            _deckManager._topPos[s1._row].GetComponent<Selectable>()._value = 0;
            _deckManager._topPos[s1._row].GetComponent<Selectable>()._suit = null;
        }
        else if (s1._top)
        {
            _deckManager._topPos[s1._row].GetComponent<Selectable>()._value = s1._value - 1;
        }
        else
        {
            _deckManager._bottoms[s1._row].Remove(_slot1.name);
        }

        s1._inDeckPile = false;
        s1._row = s2._row;

        if (s2._top)
        {
            _deckManager._topPos[s1._row].GetComponent<Selectable>()._value = s1._value;
            _deckManager._topPos[s1._row].GetComponent<Selectable>()._suit = s1._suit;
            s1._top = true;
        }
        else
        {
            s1._top = false;
        }

        _slot1 = this.gameObject;
    }

    bool Blocked(GameObject selected)
    {
        Selectable s2 = selected.GetComponent<Selectable>();
        if(s2._inDeckPile == true)
        {
            if(s2.name == _deckManager._tripsOnDisplay.Last())
            {
                return false;
            }
            else
            {
                print(s2.name + " is blocked by " + _deckManager._tripsOnDisplay.Last());
                return true;
            }
        }
        else
        {
            if(s2.name == _deckManager._bottoms[s2._row].Last())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    bool DoubleClick()
    {
        if(_timer < _doubleClickTime && _clickCount == 2)
        {
            print("Double Click");
            return true;
        }
        else
        {
            return false;
        }
    }
    void AutoStack(GameObject selected)
    {
        for(int i = 0; i < _deckManager._topPos.Length; i++)
        {
            Selectable stack = _deckManager._topPos[i].GetComponent<Selectable>();
            if(selected.GetComponent<Selectable>()._value == 1)
            {
                if (_deckManager._topPos[i].GetComponent<Selectable>()._value == 0)
                {
                    _slot1 = selected;
                    Stack(stack.gameObject);
                    break;
                }
            }
            else
            {
                if((_deckManager._topPos[i].GetComponent<Selectable>()._suit == _slot1.GetComponent<Selectable>()._suit) && (_deckManager._topPos[i].GetComponent<Selectable>()._value == _slot1.GetComponent<Selectable>()._value - 1))
                {
                    if (HasNoChildren(_slot1))
                    {
                        _slot1 = selected;
                        string lastCardName = stack._suit + stack._value.ToString();
                        if(stack._value == 1)
                        {
                            lastCardName = stack._suit + "A";
                        }
                        if (stack._value == 11)
                        {
                            lastCardName = stack._suit + "J";
                        }
                        if (stack._value == 12)
                        {
                            lastCardName = stack._suit + "Q";
                        }
                        if (stack._value == 13)
                        {
                            lastCardName = stack._suit + "K";
                        }
                        GameObject lastCard = GameObject.Find(lastCardName);
                        Stack(lastCard);
                        break;
                    }
                }
            }
        }
    }
    bool HasNoChildren(GameObject card)
    {
        int i = 0;
        foreach (Transform child in card.transform)
        {
            i++;
        }
        if (i == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
