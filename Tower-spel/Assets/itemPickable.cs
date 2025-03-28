using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickable : MonoBehaviour, IPickable
{
    public itemSO itemScriptableobject;

    public void PickItem()
    {
        Destroy(gameObject);
    }
}
