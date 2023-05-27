using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private List<Slot> baseSlots = new List<Slot>();
    [SerializeField]
    private List<NewSlot> activeSlots = new List<NewSlot>();
    [SerializeField]
    private List<Item> existingItems = new List<Item>();

    public int inventorySlotAmount = 32;

    public Sprite slotBackground;

    public GameObject slotPrefab;
    public GameObject content;

    public static InventoryManager instance;

    [System.Serializable]
    public class Slot
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private Sprite background;
        [SerializeField]
        private Sprite itemImage = null;
        [SerializeField]
        private Item item;

        public int Id
        {
            get => id;
            set => id = value;
        }

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

        public Slot(int id, Sprite background, Sprite itemImage, Item item)
        {
            Id = id;
            Background = background;
            ItemImage = itemImage;
            Item = item;
        }
    }

    [System.Serializable]
    public class Item
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private Sprite image;
        [SerializeField]
        private string name;
        [SerializeField]
        private string description;

        public int Id
        {
            get => id;
            set => id = value;
        }

        public Sprite Image
        {
            get => image;
            set => image = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public Item(int id, Sprite image, string name, string description)
        {
            Id = id;
            Image = image;
            Name = name;
            Description = description;
        }
    }

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        content = GameObject.FindGameObjectWithTag("Content");

        for (int i = 0; i < inventorySlotAmount; i++)
        {
            //Create an empty slot as base
            Slot tempSlot = new Slot(i, slotBackground, null, null);
            baseSlots.Add(tempSlot);

            //Instantiate a new slot
            GameObject newSlot = Instantiate(slotPrefab, content.transform);

            //Assign its tag
            newSlot.tag = "Slot";

            //Add the slot component
            newSlot.AddComponent<NewSlot>();
            NewSlot slotComp = newSlot.GetComponent<NewSlot>();

            //Base the values
            slotComp.id = tempSlot.Id;
            slotComp.background = tempSlot.Background;
            slotComp.itemImage = tempSlot.ItemImage;

            //Add it to the active slot list
            activeSlots.Add(slotComp);
        }

        PopulateItems();
    }

    public void PopulateItems()
    {
        existingItems.Add(new Item(0, null, "Leather hat.", "A cool hat your dad gave you as a kid."));
        existingItems.Add(new Item(1, null, "Wooden sword.", "The sword you used to play with your friend Billy when you were kids."));
        existingItems.Add(new Item(2, null, "Baseball.", "The baseball you used to play catch with your dad."));
        existingItems.Add(new Item(3, null, "Rusty key.", "This key used to unlock something, not sure if it does anymore..."));
        existingItems.Add(new Item(4, null, "Gold tooth.", "You remember your dad had one of these."));
        existingItems.Add(new Item(5, null, "Family photo.", "You, your dad, your mom, your sister and someone you don't remember...?"));
        existingItems.Add(new Item(6, null, "Bow.", "Just a regular bow, you wonder how it got here."));
        existingItems.Add(new Item(7, null, "Broken arrow.", "A broken arrow. A shame you can't use it to play with that bow, its very cool though!"));
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < activeSlots.Count; i++)
        {
            if (activeSlots[i].itemImage == null && activeSlots[i].item == null)
            {
                activeSlots[i].itemImage = item.Image;
                activeSlots[i].item = item;
            }
        }
    }
}
