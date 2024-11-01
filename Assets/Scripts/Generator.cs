using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++){
            for(int ii = 0; ii < 13; ii++) {
                GameObject newCard = Instantiate<GameObject>(cardPrefab);
                newCard.GetComponent<Card>().cardValue = (Card.Value) ii;
                newCard.GetComponent<Card>().cardSuit = (Card.Suit) i;
                newCard.GetComponent<Card>().visible = false;
                newCard.transform.position = new Vector2(ii - 7, (float)(i*1.5));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
