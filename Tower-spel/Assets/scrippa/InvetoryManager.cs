using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<Item> items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;


    public Toggle EnableRemove;

    //public InventoryItemController[] InventoryItems;
    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        items.Add(item);
       // ListItems();
    }


    public void Remove(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        //clean content before open.
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }


        foreach (var item in items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<Text>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            ItemIcon.sprite = item.icon; 
            


        }
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
            item.Find("RemoveButton").gameObject.SetActive(true);
            }
        
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
        

        
    }


}
