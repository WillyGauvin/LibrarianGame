using System.Collections;
using System.Collections.Generic;
using System.IO.Enumeration;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class ItemSO : ScriptableObject
{
    [Header("Properties")]

    public float cooldown;
    public itemType item_type;
    public Sprite item_sprite;
}

public enum itemType {BookHand, ShushHand};