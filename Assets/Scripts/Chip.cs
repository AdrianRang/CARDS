using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chip : MonoBehaviour
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

    public GameObject stackPrefab;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        sr.sprite = chips[(int)chipVal];
    }

    private void OnMouseUp() {
        if(GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Stacks"))) {
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position, LayerMask.GetMask("Stacks"));
            foreach (Collider2D collider in colliders) {
                if (collider.GetComponent<Stack>() != null) {
                    collider.GetComponent<Stack>().AddChip(gameObject);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

    private void OnMouseDown() {
        mouseOffset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    private void OnMouseDrag() {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseOffset;
    }

    private void OnMouseOver(){
        if(Input.GetMouseButtonDown(1)){
            TurnToStack();
        }
    }

    public void TurnToStack()  {
        GameObject stack = Instantiate(stackPrefab);
        stack.transform.position = transform.position;
        stack.GetComponent<Stack>().AddChip(gameObject);
    }

    public uint GetUintValue() {
        switch (chipVal) {
            case Value.One:
                return 1;
            case Value.Five:
                return 5;
            case Value.Ten:
                return 10;
            case Value.TwentyFive:
                return 25;
            case Value.Hundred:
                return 100;
            case Value.FiveHunderd:
                return 500;
            case Value.Thousand:
                return 1000;
            case Value.FiveThousand:
                return 5000;
            default:
                return 0;
        }
    }

    public static uint GetUintValue(Value val) {
        switch (val) {
            case Value.One:
                return 1;
            case Value.Five:
                return 5;
            case Value.Ten:
                return 10;
            case Value.TwentyFive:
                return 25;
            case Value.Hundred:
                return 100;
            case Value.FiveHunderd:
                return 500;
            case Value.Thousand:
                return 1000;
            case Value.FiveThousand:
                return 5000;
            default:
                return 0;
        }
    }
}
