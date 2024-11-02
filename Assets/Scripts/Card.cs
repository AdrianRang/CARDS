using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum Value {
        As,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
    public enum Suit {
        Hearts,
        Diamonds,
        Clubs,
        Spades,
    }

    public Value cardValue;
    public Suit cardSuit;
    public Sprite[] cardSprites;
    public Sprite backSprite;

    private SpriteRenderer sr;

    public bool visible = false;
    private float timeWhenPressed = 0;
    private bool dragging = false;
    private Vector2 mouseOffset = new Vector2();

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
       if(visible) {
            sr.sprite = GetSprite(cardValue, cardSuit);
        } else{
            sr.sprite = backSprite;
        }
        if(dragging){
            if(!Input.GetMouseButton(0)){
                dragging = false;
            }
            transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) - mouseOffset;
        }
    }

    private void OnMouseDown() {
        mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        timeWhenPressed = Time.time;
        sr.sortingOrder = 1;
        if(Input.GetKey(KeyCode.A)){
            Debug.Log("Destroying...");
            GameObject.Find("Deck").GetComponent<Deck>().addCard(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnMouseUp() {
        if(Time.time - timeWhenPressed < 0.15) {
            visible = !visible;
        }
        sr.sortingOrder = 0;
    }

    private void OnMouseDrag() {
        transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y) - mouseOffset;
    }

    // this is used when instantiated from deck to make sure it's grabbed
    public void StartDragging() {
        dragging = true;
    }

    private Sprite GetSprite(Value val, Suit suit) {
        String name = SuitName(suit) + "-" + ValueName(val);
        foreach (Sprite sprite in cardSprites) {
            if(sprite.name == name){
                return sprite;
            }
        }
        throw new Exception("Sprite not found");
    }

    private String SuitName(Suit suit) {
        switch(suit) {
            case Suit.Clubs:
                return "Clubs";
        case Suit.Diamonds:
            return "Diamonds";
        case Suit.Hearts:
            return "Hearts";
        case Suit.Spades:
            return "Spades";
        default:
            return "";
        }
    }

    private String ValueName(Value val) {
        switch(val) {
            case Value.As:
                return "A";
            case Value.Two:
                return "2";
            case Value.Three:
                return "3";
            case Value.Four:
                return "4";
            case Value.Five:
                return "5";
            case Value.Six:
                return "6";
            case Value.Seven:
                return "7";
            case Value.Eight:
                return "8";
            case Value.Nine:
                return "9";
            case Value.Ten:
                return "10";
            case Value.Jack:
                return "J";
            case Value.Queen:
                return "Q";
            case Value.King:
                return "K";
            default:
                return "";
        }
    }
}