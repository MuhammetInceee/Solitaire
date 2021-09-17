using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject _cardPrefab;
    public GameObject _deckButton;
    public GameObject[] _bottomPos;
    public GameObject[] _topPos;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<string>[] _bottoms;
    public List<string>[] _tops;
    public List<string> _tripsOnDisplay = new List<string>();
    public List<List<string>> _deckTrips = new List<List<string>>();

    private List<string> _bottom0 = new List<string>();
    private List<string> _bottom1 = new List<string>();
    private List<string> _bottom2 = new List<string>();
    private List<string> _bottom3 = new List<string>();
    private List<string> _bottom4 = new List<string>();
    private List<string> _bottom5 = new List<string>();
    private List<string> _bottom6 = new List<string>();

    public List<string> deck;
    public List<string> _discardPile = new List<string>();
    private int _deckLocation;
    private int _trips;
    private int _tripsRemainder;
    public int _levelSelector = 3; // Just for now 3, When i create start screen, it change for difficulty.

    void Start()
    {
        _bottoms = new List<string>[] { _bottom0, _bottom1, _bottom2, _bottom3, _bottom4, _bottom5, _bottom6 };
        PlayCard();
    }


    void Update()
    {

    }

    public void PlayCard()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        SolitaireSort();
        StartCoroutine(SoltaireDeal());
        SortDeckIntoTrips();
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    }
    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
    IEnumerator SoltaireDeal()
    {
        for (int i = 0; i < 7; i++)
        {
            float yOffset = 0;
            float zOffset = 1;
            foreach (string card in _bottoms[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = Instantiate(_cardPrefab, new Vector3(_bottomPos[i].transform.position.x, _bottomPos[i].transform.position.y - yOffset, _bottomPos[i].transform.position.z), Quaternion.identity, _bottomPos[i].transform);
                newCard.GetComponent<SpriteRenderer>().sortingOrder = (int)zOffset;
                newCard.name = card;
                if (card == _bottoms[i][_bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }
                zOffset += 1;
                yOffset += 0.2f;
                _discardPile.Add(card);
            }
        }

        foreach (string card in _discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        _discardPile.Clear();
    }

    void SolitaireSort()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = i; j < 7; j++)
            {
                _bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }

    public void SortDeckIntoTrips()
    {
        _trips = deck.Count / _levelSelector;
        _tripsRemainder = deck.Count % _levelSelector;
        _deckTrips.Clear();

        int modifier = 0;
        for (int i = 0; i < _trips; i++)
        {
            List<string> myTrips = new List<string>();
            for (int j = 0; j < _levelSelector; j++)
            {
                myTrips.Add(deck[j + modifier]);
            }
            _deckTrips.Add(myTrips);
            modifier = modifier + _levelSelector;
        }
        if (_tripsRemainder != 0)
        {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for (int k = 0; k < _tripsRemainder; k++)
            {
                myRemainders.Add(deck[deck.Count - _tripsRemainder + modifier]);
            }
            _deckTrips.Add(myRemainders);
            _trips++;
        }
        _deckLocation = 0;
    }

    public void DealFromDeck()
    {
        foreach (Transform child in _deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                _discardPile.Add(child.name);
                Destroy(child.gameObject);
            }
        }

        if (_deckLocation < _trips)
        {
            _tripsOnDisplay.Clear();
            float xOffset = 0.7f;
            float zOffset = 1;

            foreach (string card in _deckTrips[_deckLocation])
            {
                GameObject newTopCard = Instantiate(_cardPrefab, new Vector3(_deckButton.transform.position.x + xOffset, _deckButton.transform.position.y, _deckButton.transform.position.z), Quaternion.identity, _deckButton.transform);
                newTopCard.GetComponent<SpriteRenderer>().sortingOrder = (int)zOffset;
                xOffset += 0.2f;
                zOffset += 1;
                newTopCard.name = card;
                _tripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().faceUp = true;
            }
            _deckLocation++;
        }

        else
        {
            RestackTopDeck();
        }
        void RestackTopDeck()
        {
            foreach (string card in _discardPile)
            {
                deck.Add(card);
            }
            _discardPile.Clear();
            SortDeckIntoTrips();
        }
    }
}