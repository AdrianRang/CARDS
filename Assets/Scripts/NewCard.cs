using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCard : MonoBehaviour
{
    public GameObject cardPrefab;

    private TMPro.TMP_Dropdown suit;
    private TMPro.TMP_Dropdown value;
    private Button get;

    void Start()
    {
        TMPro.TMP_Dropdown[] dropdowns = GetComponentsInChildren<TMPro.TMP_Dropdown>();
        if(dropdowns.Length == 0) throw new System.Exception("No dropdowns");
        suit = dropdowns[0];
        value = dropdowns[1];

        get = GetComponentInChildren<Button>();

        get.onClick.AddListener(() => {
            GameObject newCard = Instantiate(cardPrefab);
            newCard.GetComponent<Card>().cardSuit = (Card.Suit)suit.value;
            newCard.GetComponent<Card>().cardValue = (Card.Value)value.value;
            newCard.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.SetActive(false);
        });
    }
}
