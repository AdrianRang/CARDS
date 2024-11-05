using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour
{


    public Card[] cards;

    public GameObject cardPrefab;
    public GameObject menu;

    public Sprite horSprite;
    public Sprite vecSprite;

    private bool dragging = false;
    private bool horizontal = false;

    private SpriteRenderer sr;
    private BoxCollider2D bc;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cards.Length == 0) {
            // sr.enabled = false;
            Destroy(gameObject);
        } else{
            sr.enabled = true;
        }

        if (dragging){
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
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

    public void AddDeck(GameObject otherDeck) {
        Deck other = otherDeck.GetComponent<Deck>();
        Card[] newCards = new Card[cards.Length + other.cards.Length];
        for(int i = 0; i < cards.Length; i++) newCards[i] = cards[i];
        for(int i = cards.Length; i < cards.Length + other.cards.Length; i++) newCards[i] = other.cards[i-cards.Length];
        cards = newCards;
        Destroy(otherDeck);
    }

    private void OnMouseUp() {
        if(GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Decks"))) {
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position, LayerMask.GetMask("Decks"));
            foreach (Collider2D collider in colliders) {
                if (collider.GetComponent<Deck>() != null && collider.gameObject != gameObject) {
                    collider.gameObject.GetComponent<Deck>().AddDeck(gameObject);
                    // Destroy(gameObject);
                    break;
                }
            }
        }
    }

    private void OnMouseDown() {
        if(!dragging){
            TakeOutCard();
        } else {
            dragging = false;
        }
    }

    public void TakeOutCard() {
        if(cards.Length == 0) return;
        GameObject newCard = Instantiate(cardPrefab);
        newCard.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Card outCard = newCard.GetComponent<Card>();
        Card lastCard = cards[cards.Length - 1];
        outCard.cardValue = lastCard.cardValue;
        outCard.cardSuit = lastCard.cardSuit;
        outCard.StartDragging();
        cards = pop(cards);
    }

    // https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net

    public void Shuffle() {
        System.Random rng = new System.Random();

        int n = cards.Length;
        while (n > 1) 
        {
            int k = rng.Next(n--);
            Card temp = cards[n];
            cards[n] = cards[k];
            cards[k] = temp;
        }
    }

    public void StartMoving() {
        dragging = true;
    }

    public void Rotate() {
        horizontal = !horizontal;

        if(horizontal){
            sr.sprite = horSprite;
        } else {
            sr.sprite = vecSprite;
        }

        // MeshRenderer renderer = GetComponent<MeshRenderer>();
        // bc.offset = sr.bounds.center;
        bc.size = sr.bounds.size;
    }

    private void OnMouseOver() {
        // Right Click menu
        if(Input.GetMouseButtonDown(1)){
            GameObject tempMenu = Instantiate(menu, GameObject.Find("Canvas").transform);
            Button[] buttons = tempMenu.GetComponentsInChildren<Button>();
            Button takeOut = buttons[0];
            Button shuffle = buttons[1];
            Button move = buttons[2];
            Button rotate = buttons[3];
            Button delete = buttons[4];
            tempMenu.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            takeOut.onClick.AddListener(()=>{TakeOutCard(); Destroy(tempMenu);});
            shuffle.onClick.AddListener(Shuffle);
            move.onClick.AddListener(()=>{StartMoving(); Destroy(tempMenu);});
            rotate.onClick.AddListener(Rotate);
            delete.onClick.AddListener(() => {Destroy(gameObject);});

            tempMenu.GetComponentInChildren<TextMeshProUGUI>().text = "Deck (" + cards.Length + ")";
        }
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
