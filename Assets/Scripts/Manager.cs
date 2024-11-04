using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject deckPrefab;
    public GameObject bgMenu;

    private Vector2 hidden = new Vector2(10, 10);

    void Awake()
    {
        bgMenu.transform.position = hidden;
    }

    // Update is called once per frame
    void Update()
    {

        // Background Menu
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if(Input.GetMouseButtonDown(1) && hit.collider == null){
            GenBackgroundMenu();
        }

        if((Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) && !IsMouseOverBgMenu()) {
            bgMenu.transform.position = hidden;
        }
    }

    public void GenBackgroundMenu() {
        bgMenu.transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool IsMouseOverBgMenu() {
        if (bgMenu == null) return false;
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D bgMenuCollider = bgMenu.GetComponent<Collider2D>();
        
        return bgMenuCollider != null && bgMenuCollider.OverlapPoint(mousePos);
    }

    public void newDeck(Vector2 position) {
        GameObject deck = Instantiate(deckPrefab);
        deck.transform.position = position;

        for(int i = 0; i < 4; i++){
            for(int ii = 0; ii < 13; ii++) {
                GameObject newCard = Instantiate(cardPrefab);
                newCard.GetComponent<Card>().cardValue = (Card.Value) ii;
                newCard.GetComponent<Card>().cardSuit = (Card.Suit) i;
                newCard.GetComponent<Card>().visible = false;
                // newCard.transform.position = new Vector2(ii - 7, (float)(i*1.5));
                deck.GetComponent<Deck>().addCard(newCard);
            }
        }
    }

    public void newDeckOnMouse() {
        newDeck(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public GameObject newDeck() {
        GameObject deck = Instantiate(deckPrefab);

        for(int i = 0; i < 4; i++){
            for(int ii = 0; ii < 13; ii++) {
                GameObject newCard = Instantiate(cardPrefab);
                newCard.GetComponent<Card>().cardValue = (Card.Value) ii;
                newCard.GetComponent<Card>().cardSuit = (Card.Suit) i;
                newCard.GetComponent<Card>().visible = false;
                // newCard.transform.position = new Vector2(ii - 7, (float)(i*1.5));
                deck.GetComponent<Deck>().addCard(newCard);
            }
        }

        return deck;
    }
}
