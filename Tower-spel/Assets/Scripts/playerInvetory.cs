using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInvetory : MonoBehaviour
{
    [Header("General")]

    public List<itemType> inventoryList;
    public int selectedItem;

    //[Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickItemKey;

    //[Space(20)]
    //[Header("Item gameobjects")]

    void Update()
    {

        
    }
}
