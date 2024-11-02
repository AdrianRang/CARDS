using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryMenu : MonoBehaviour
{
    private bool mouseIsOver = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) && !mouseIsOver) {
            Destroy(gameObject);
        }
    }

    private void OnMouseEnter() {
        mouseIsOver = true;
    }

    private void OnMouseExit() {
        mouseIsOver = false;
    }
}
