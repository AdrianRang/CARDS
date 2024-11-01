using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card[] cards;

    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cards.Length == 0) {
            GetComponent<SpriteRenderer>().enabled = false;
        } else{
            GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void addCard(GameObject card) {
        Card newCard = new GameObject("CardGhost").AddComponent<Card>();
        newCard.enabled = false;
        Card oldCard = card.GetComponent<Card>();
        newCard.backSprite = oldCard.backSprite;
        newCard.cardSprites = oldCard.cardSprites;
        newCard.cardSuit = oldCard.cardSuit;
        newCard.cardValue = oldCard.cardValue;
        cards = appendCard(cards, newCard);
        Destroy(card);
    }

    private void OnMouseDown() {
        GameObject newCard = Instantiate(cardPrefab);
        newCard.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Card outCard = newCard.GetComponent<Card>();
        Card lastCard = cards[cards.Length - 1];
        outCard.cardValue = lastCard.cardValue;
        outCard.cardSuit = lastCard.cardSuit;
        outCard.StartDragging();
        cards = pop(cards);
    }

    private Card[] appendCard(Card[] og, Card card) {
        Card[] cards = new Card[og.Length + 1];
        for(int i = 0; i < og.Length; i++) {
            cards[i] = og[i];
        }
        cards[og.Length] = card;

        return cards;
    }

    private Card[] pop(Card[] cards) {
        Card[] news = new Card[cards.Length-1];
        for(int i = 0; i < cards.Length - 1; i++){
            news[i] = cards[i];
        }
        Destroy(cards[cards.Length-1]);
        return news;
    }
}
