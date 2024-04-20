using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 3)]
public class Item : ScriptableObject
{
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string description;

    public Sprite Image
    {
        get => image;
        set => image = value;
    }

    public string ItemName
    {
        get => itemName;
        set => itemName = value;
    }

    public string Description
    {
        get => description;
        set => description = value;
    }
}
