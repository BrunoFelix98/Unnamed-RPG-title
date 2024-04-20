using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    [SerializeField]
    private Sprite background;
    [SerializeField]
    private Sprite itemImage = null;
    [SerializeField]
    private Item item;

    public Sprite Background
    {
        get => background;
        set => background = value;
    }

    public Sprite ItemImage
    {
        get => itemImage;
        set => itemImage = value;
    }

    public Item Item
    {
        get => item;
        set => item = value;
    }
}