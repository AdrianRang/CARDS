using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class SelectionSquare : MonoBehaviour
{
    private Vector2 startPos;

    private bool selecting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = GetMousePos();
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if(Input.GetMouseButtonDown(0) && hit.collider == null){
            startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetComponent<SpriteRenderer>().enabled = true;
            selecting = true;
        }

        transform.position = startPos - (startPos-GetMousePos())/2;

        transform.localScale = new Vector2(
            startPos.x - GetMousePos().x, 
            startPos.y - GetMousePos().y
        );

        if(Input.GetMouseButtonUp(0) && selecting == true) {
            GetComponent<SpriteRenderer>().enabled = false;
            selecting = false;

            GetComponent<BoxCollider2D>().enabled = true;

            ContactFilter2D filter = new ContactFilter2D();
            Collider2D[] results = new Collider2D[10];
            GetComponent<BoxCollider2D>().Overlap(filter, results);

            foreach (Collider2D result in results) {
                try {
                    result.gameObject.GetComponent<Dragger>().StartDragging();
                } catch(Exception) {
                    continue;
                }
            }

            GetComponent<BoxCollider2D>().enabled = false;            
        }
    }

    private Vector2 GetMousePos() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
