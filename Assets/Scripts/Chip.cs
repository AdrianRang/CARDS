using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public enum Value {
        One,
        Five,
        Ten,
        TwentyFive,
        Hundred,
        FiveHunderd,
        Thousand,
        FiveThousand
    }

    public Sprite[] chips = new Sprite[8];

    public Value chipVal;

    private Vector2 mouseOffset = new Vector2();
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = chips[(int)chipVal];
    }

    private void OnMouseDown() {
        mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseDrag() {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseOffset;
    }
}
