using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject _cardPrefab;
    public GameObject[] _bottomPos;
    public GameObject[] _topPos;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
    public List<string>[] _bottoms;
    public List<string>[] _tops;

    private List<string> _bottom0 = new List<string>();
    private List<string> _bottom1 = new List<string>();
    private List<string> _bottom2 = new List<string>();
    private List<string> _bottom3 = new List<string>();
    private List<string> _bottom4 = new List<string>();
    private List<string> _bottom5 = new List<string>();
    private List<string> _bottom6 = new List<string>();

    public List<string> deck;

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
    }

    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach(string s in suits)
        {
            foreach(string v in values)
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
                newCard.GetComponent<SpriteRenderer>().sortingOrder = (int) zOffset;
                newCard.name = card;
                if(card == _bottoms[i][_bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }
                zOffset += 1;
                yOffset += 0.2f;
            }
        }
    }

    void SolitaireSort()
    {
        for(int i = 0; i < 7; i++)
        {
            for(int j = i; j < 7; j++)
            {
                _bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }
}
