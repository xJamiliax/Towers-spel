using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPick : MonoBehaviour
{
    public Item item;


    void Pickup()
    {
        InvetoryManager.Instance.Add(item);
        Destroy(gameObject);
    
    
    }

    private void OnMouseDown()
    {
        Pickup();
    }





}
