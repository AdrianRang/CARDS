using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMenu : MonoBehaviour
{
    private bool mouseIsOver = false;

    private Vector2 hidden = new Vector2(10, 10);

    void Awake(){
        // If you don't see it it doesn't exist
        transform.position = new Vector2(10, 10);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if(Input.GetMouseButtonDown(1) && hit.collider == null){
            transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Show(GameObject.Find("Main"));
            Hide(GameObject.Find("NewMenu"));
        }

        if((Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) && !mouseIsOver) {
            transform.position = hidden;
        }
    }

    public void Hide(GameObject obj) {
        obj.transform.localPosition = hidden;
    }

    public void Show(GameObject obj) {
        obj.transform.localPosition = Vector2.zero;
    }

    private void OnMouseEnter() {
        mouseIsOver = true;
    }

    private void OnMouseExit() {
        mouseIsOver = false;
    }

}