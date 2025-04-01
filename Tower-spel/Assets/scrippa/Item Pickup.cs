using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPick : MonoBehaviour
{
    public Item Item;


    void Pickup()
    {
        InventoryManager.Instance.Add(Item);
        Destroy(gameObject);
    
    
    }

    private void OnMouseDown()
    {
        Pickup();
    }





}
