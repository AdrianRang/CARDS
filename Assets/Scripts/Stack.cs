using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stack : MonoBehaviour
{
    [SerializeField]
    private uint amount;

    public GameObject chip;
    public GameObject menu;
    public GameObject takeOutMenu;

    private TextMeshProUGUI display;

    private bool dragging = false;

    void Awake()
    {
        display = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(amount <= 0){
            Destroy(gameObject);
        }

        display.text = "$" + amount;

        transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        if (dragging){
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

    }

    public void AddChip(GameObject chip){
        amount += chip.GetComponent<Chip>().GetUintValue();
        Destroy(chip);
    }

    private void OnMouseDown() {
        if(!dragging) {
            WithdrawStackMenu();
        } else {
            dragging = false;
        }
    }

    private void OnMouseUp() {
        if(GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Stacks"))) {
            Collider2D[] colliders = Physics2D.OverlapPointAll(transform.position, LayerMask.GetMask("Stacks"));
            foreach (Collider2D collider in colliders) {
                if (collider.GetComponent<Stack>() != null && collider.gameObject != gameObject) {
                    collider.gameObject.GetComponent<Stack>().AddStack(gameObject);
                    // Destroy(gameObject);
                    break;
                }
            }
        }
    }

    private void OnMouseOver() {
        // Right Click menu
        if(Input.GetMouseButtonDown(1)){
            GameObject tempMenu = Instantiate(menu, GameObject.Find("Canvas").transform);
            Button[] buttons = tempMenu.GetComponentsInChildren<Button>();
            Button takeOut = buttons[0];
            Button move = buttons[1];

            tempMenu.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

            takeOut.onClick.AddListener(WithdrawChipsMenu);
            move.onClick.AddListener(StartMoving);
        }
    }

    public void WithdrawChipsMenu() {
        GameObject toMenu = Instantiate(takeOutMenu, GameObject.Find("Canvas").transform);
        toMenu.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        toMenu.GetComponentInChildren<Scrollbar>().onValueChanged.AddListener((value)=>{
            toMenu.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "$" + ((int)(value * amount)).ToString();
        });
        Button[] buttons = toMenu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => {WithdrawChips((uint)(toMenu.GetComponentInChildren<Scrollbar>().value * amount));});
        buttons[1].onClick.AddListener(() => {toMenu.GetComponentInChildren<Scrollbar>().value += 1/(float)amount;});
        buttons[2].onClick.AddListener(() => {toMenu.GetComponentInChildren<Scrollbar>().value -= 1/(float)amount;});
    }

    public void WithdrawStackMenu() {
        GameObject toMenu = Instantiate(takeOutMenu, GameObject.Find("Canvas").transform);
        toMenu.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        toMenu.GetComponentInChildren<Scrollbar>().onValueChanged.AddListener((value)=>{
            toMenu.GetComponentsInChildren<TextMeshProUGUI>()[0].text = "$" + ((int)(value * amount)).ToString();
        });
        Button[] buttons = toMenu.GetComponentsInChildren<Button>();
        buttons[0].onClick.AddListener(() => {WithdrawStack((uint)(toMenu.GetComponentInChildren<Scrollbar>().value * amount));});
        buttons[1].onClick.AddListener(() => {toMenu.GetComponentInChildren<Scrollbar>().value += 1/(float)amount;});
        buttons[2].onClick.AddListener(() => {toMenu.GetComponentInChildren<Scrollbar>().value -= 1/(float)amount;});
    }

    public void WithdrawChips(uint amount) {
        this.amount -= amount;

        for(int currChipVal = (int) Chip.Value.FiveThousand; currChipVal >= 0; currChipVal--) {
            Debug.Log((int)((float)amount/Chip.GetUintValue((Chip.Value)currChipVal)) + " chips of " + (Chip.Value)currChipVal);

            uint newAmount = amount;
            for(int i = 0; i < (int)((float)amount/Chip.GetUintValue((Chip.Value)currChipVal)); i++){
                Chip newChip = Instantiate(chip).GetComponent<Chip>();
                newChip.chipVal = (Chip.Value)currChipVal;
                newAmount -= Chip.GetUintValue((Chip.Value)currChipVal);
                newChip.gameObject.transform.position = transform.position + Vector3.up + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
            amount = newAmount;
        }
    }

    public void WithdrawStack(uint amount) {
        this.amount -= amount;

        GameObject newStack = Instantiate(gameObject);
        newStack.GetComponent<Stack>().amount = amount;
        newStack.GetComponent<Stack>().StartMoving();
    }

    public void AddStack(GameObject stack) {
        this.amount += stack.GetComponent<Stack>().amount;
        Destroy(stack);
    }

    public void StartMoving() {
        dragging = true;
    }
}
