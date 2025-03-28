using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInvetory : MonoBehaviour
{
    [Header("General")]

    public List<itemType> inventoryList;
    public int selectedItem;
    public float playerReach;
    [SerializeField] GameObject throwItem_gameobject;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickItemKey;

    [Space(20)]
    [Header("Item gameobjects")]
    [SerializeField] GameObject Diamand_item;
    [SerializeField] GameObject Tak_item;
    [SerializeField] GameObject Boek_item;
    [SerializeField] GameObject Bloem_item;

    [Space(20)]
    [Header("Item Prefabs")]
    [SerializeField] GameObject Diamand_prefab;
    [SerializeField] GameObject Tak_prefab;
    [SerializeField] GameObject Boek_prefab;
    [SerializeField] GameObject Bloem_prefab;


    [SerializeField] Camera cam;


    private Dictionary<itemType, GameObject> itemSetActive = new Dictionary<itemType, GameObject>() { };
    private Dictionary<itemType, GameObject> itemInstantiate = new Dictionary<itemType, GameObject>() { };

    void Start()
    {
        itemSetActive.Add(itemType.Diamand, Diamand_item);
        itemSetActive.Add(itemType.Tak, Tak_item);
        itemSetActive.Add(itemType.Boek, Boek_item);
        itemSetActive.Add(itemType.Bloem, Bloem_item);

        itemInstantiate.Add(itemType.Diamand, Diamand_prefab);
        itemSetActive.Add(itemType.Tak, Tak_prefab);
        itemSetActive.Add(itemType.Boek, Boek_prefab);
        itemSetActive.Add(itemType.Bloem, Bloem_prefab);

        NewItemSelected();
    }

    void Update()
    {
        // item pickup
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, playerReach) && Input.GetKey(pickItemKey))
        {
           IPickable item = hitInfo.collider.GetComponent<IPickable>();
            if (item != null)
            {
                inventoryList.Add(hitInfo.collider.GetComponent<ItemPickable>().itemScriptableobject.item_type);
                item.PickItem();
            }
        }
        // Items throw
        if (Input.GetKeyDown(throwItemKey) && inventoryList.Count > 1)
        {
            Instantiate(itemInstantiate[inventoryList[selectedItem]], position: throwItem_gameobject.transform.position, new Quaternion());        
            inventoryList.RemoveAt(selectedItem);
            
            if(selectedItem!= 0)
            {
                selectedItem = 1;
            }
            NewItemSelected();
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
        else if (Input.GetKeyDown(KeyCode.Alpha3) && inventoryList.Count > 2)
        {
            selectedItem = 2;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && inventoryList.Count > 3)
        {
            selectedItem = 3;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5) && inventoryList.Count > 4)
        {
            selectedItem = 4;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6) && inventoryList.Count > 5)
        {
            selectedItem = 5;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7) && inventoryList.Count > 6)
        {
            selectedItem = 6;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8) && inventoryList.Count > 7)
        {
            selectedItem = 7;
            NewItemSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9) && inventoryList.Count > 8)
        {
            selectedItem = 8;
            NewItemSelected();
        }
    }

        private void NewItemSelected()
        {
        Diamand_item.SetActive(false);
        Tak_item.SetActive(false);
        Boek_item.SetActive(false);
        Bloem_item.SetActive(false);
        
        GameObject selectedItemGameobject = itemSetActive[inventoryList[selectedItem]];
        selectedItemGameobject.SetActive(true);

        }

}
    public interface IPickable
    {

        void PickItem();
    }