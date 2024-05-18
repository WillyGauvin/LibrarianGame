using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("General")]

    public List<itemType> inventoryList;
    public int selectedItem;

    [SerializeField] GameObject throwItem_gameObject;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickItemKey;

    [Space(20)]
    [Header("Item GameObjects")]
    [SerializeField] GameObject BookHand_item;
    [SerializeField] GameObject ShushHand_item;

    [Space(20)]
    [Header("UI")]
    [SerializeField] Image[] inventorySlotImage = new Image[2];
    [SerializeField] Image[] inventoryBackGroundImage = new Image[2];
    [SerializeField] Sprite emptySlotSprite;



    [SerializeField] Camera cam;
    [SerializeField] GameObject pickUpItem_gameObject;

    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };

    private void Start()
    {
        itemSetActive.Add(itemType.BookHand, BookHand_item);
        itemSetActive.Add(itemType.ShushHand, ShushHand_item);

        NewItemSelected();
    }

    void Update()
    {
        ///UI
        for (int i = 0; i < 2; i++)
        {
            if (i < inventoryList.Count)
            {
                inventorySlotImage[i].sprite = itemSetActive[inventoryList[i]].GetComponent<Item>().itemScriptableObject.item_sprite;
            }
            else
            {
                inventorySlotImage[i].sprite = emptySlotSprite;
            }
        }

        int a = 0;
        foreach(Image image in inventoryBackGroundImage)
        {
            if ( a == selectedItem)
            {
                image.color = new Color32(145, 255, 126, 255);
            }
            else
            {
                image.color = new Color32(219, 219, 219, 255);
            }
            a++;
        }


        //Switching inventory
        if (Input.GetKeyDown(KeyCode.Alpha1) && inventoryList.Count > 0)
        {
            selectedItem = 0;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && inventoryList.Count > 1)
        {
            selectedItem = 1;
            NewItemSelected();
        }
    }

    private void NewItemSelected()
    {
        //ShushHand was selected
        if (selectedItem == 1)
        {
            BookHand_item.GetComponent<BookHand>().SwitchedHands();
        }
        if (selectedItem == 0)
        {
            ShushHand_item.GetComponent<ShushHand>().SwitchedHands();
        }
        BookHand_item.SetActive(false);
        ShushHand_item.SetActive(false);


        GameObject selectedItemGameObject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameObject.SetActive(true);
    }
}

