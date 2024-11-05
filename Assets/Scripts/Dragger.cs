using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragger : MonoBehaviour
{
    private Vector2 mouseOffset = new Vector2();
    private bool dragging = false;

    private Color selectColor = Color.cyan;

    // Update is called once per frame
    void Update()
    {
        if(dragging) {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + mouseOffset;
        }

        if(dragging && Input.GetKey(KeyCode.Backspace)) Destroy(gameObject);

        if(Input.GetMouseButtonDown(0)){
            dragging = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void StartDragging() {
        mouseOffset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GetComponent<SpriteRenderer>().color = selectColor;
        dragging = true;
    }
}
