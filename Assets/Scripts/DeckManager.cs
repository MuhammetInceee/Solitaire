using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject _cardPrefab;

    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "A", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

    public List<string> deck;

    void Start()
    {
        PlayCard();
    }


    void Update()
    {
        
    }

    public void PlayCard()
    {
        deck = GenerateDeck();
        Shuffle(deck);

        foreach(string card in deck)
        {
            print(card);
        }
        SoltaireDeal();
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
    void SoltaireDeal()
    {
        float yOffset = 0;
        float zOffset = 0.03f;
        foreach(string card in deck)
        {
            GameObject newCard = Instantiate(_cardPrefab, new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z - zOffset), Quaternion.identity);
            newCard.name = card;

            yOffset = yOffset + 0.1f;
            zOffset = zOffset + 0.03f;
        }
    }
}
