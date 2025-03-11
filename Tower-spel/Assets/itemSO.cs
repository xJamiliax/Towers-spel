using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "Scriptable Object/Items")]
public class itemSO : ScriptableObject
{
    [Header("properties")]
    public float cooldown;
    public itemType item_type;
    public Sprite item_sprite;
}

public enum itemType { Diamand, Tak, Boek ,Bloem};
