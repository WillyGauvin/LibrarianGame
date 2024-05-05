using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [Header("General")]

    public List<itemType> inventoryList;
    public int selectedItem;
    public float playerReach;

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
    [Header("Item prefabs")]
    [SerializeField] GameObject BookHand_prefab;
    [SerializeField] GameObject ShushHand_prefab;


    [Space(20)]
    [Header("UI")]
    [SerializeField] Image[] inventorySlotImage = new Image[2];
    [SerializeField] Image[] inventoryBackGroundImage = new Image[2];
    [SerializeField] Sprite emptySlotSprite;



    [SerializeField] Camera cam;
    [SerializeField] GameObject pickUpItem_gameObject;


    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };
    private Dictionary<itemType, GameObject> itemInstantiate = new Dictionary<itemType, GameObject>() { };

    private void Start()
    {
        itemSetActive.Add(itemType.BookHand, BookHand_item);
        itemSetActive.Add(itemType.ShushHand, ShushHand_item);

        itemInstantiate.Add(itemType.BookHand, BookHand_prefab);
        itemInstantiate.Add(itemType.ShushHand, ShushHand_prefab);

        NewItemSelected();
    }

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, playerReach))
        {
            IPickable item = hitInfo.collider.GetComponent<IPickable>();
            if (item != null)
            {
                pickUpItem_gameObject.SetActive(true);
                if (Input.GetKey(pickItemKey))
                {
                    inventoryList.Add(hitInfo.collider.GetComponent<ItemPickable>().itemScriptableObject.item_type);
                    item.PickItem();
                }   
            }
            else
            {
                pickUpItem_gameObject.SetActive(false);

            }
        }
        else
        {
            pickUpItem_gameObject.SetActive(false);
        }
        //Item Throw
        if (Input.GetKeyDown(throwItemKey) && inventoryList.Count > 1)
        {
            Instantiate(itemInstantiate[inventoryList[selectedItem]], position: throwItem_gameObject.transform.position, new Quaternion());
            inventoryList.RemoveAt(selectedItem);

            if (selectedItem != 0)
            {
                selectedItem -= 1;
            }
            NewItemSelected();
        }

        //UI

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
        BookHand_item.SetActive(false);
        ShushHand_item.SetActive(false);

        GameObject selectedItemGameObject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameObject.SetActive(true);
    }
}

public interface IPickable
{
    void PickItem();
}

