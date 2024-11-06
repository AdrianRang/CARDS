using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject text;
    private TextMeshProUGUI[] texts;
    public GameObject manager;

    public GameObject button;

    private int step = 0;

    // Start is called before the first frame update
    void Start()
    {
        texts = text.GetComponentsInChildren<TextMeshProUGUI>();
        Debug.Log(texts.Length);
        manager.GetComponent<Manager>().newDeck(new Vector2(0, -3));
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled = false;
        bool other = true;
        if(step == 2) {
            other = GameObject.Find("Card(Clone)") != null;
        } else if(step == 4) {
            other = GameObject.Find("Card(Clone)") == null;
        } else if(step == 5) {
            GameObject.Find("Square").GetComponent<SpriteRenderer>().enabled = true;
            other = GameObject.Find("Deck(Clone)").transform.position.x >= -7.5 &&
                    GameObject.Find("Deck(Clone)").transform.position.x <= -4.5 &&
                    GameObject.Find("Deck(Clone)").transform.position.y <= 0.5 &&
                    GameObject.Find("Deck(Clone)").transform.position.y >= -2.5;
        } else if(step == 6) {
            try {
                Destroy(GameObject.Find("Deck(Clone)"));
            } catch(Exception) {}
        } else if(step == 7) {
            other = GameObject.Find("Deck(Clone)")  != null &&
                    GameObject.Find("Stack(Clone)") != null;
        } else if(step == 8) {
            other = GameObject.Find("Deck(Clone)")  != null &&
                    GameObject.Find("Stack(Clone)") != null &&
                    GameObject.Find("Stack(Clone)(Clone)") != null;   
        } else if (step == 10) {
            button.SetActive(true);
        }
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        if(Input.GetMouseButtonDown(0) && hit.collider == null && other) step++;
        if(step>0) texts[step-1].enabled = false;
        texts[step].enabled = true;
    }
}
