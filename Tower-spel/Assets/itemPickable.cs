using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickable : MonoBehaviour
{
    public itemSO itemScriptableobject;

    public void PickItem()
    {
        Destroy(gameObject);
    }
}
