using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] //Default slots
    private List<Slot> baseSlots = new List<Slot>();
    [SerializeField] //Slots objects
    private List<NewSlot> activeSlots = new List<NewSlot>();
    [SerializeField] //List of items that exist
    private List<Item> existingItems = new List<Item>();

    //Amount of slots that exist
    public int inventorySlotAmount = 32;

    //Background image of each slot
    public Sprite slotBackground;

    //Slot prefab
    public GameObject slotPrefab;

    //Canvas "inventory" content
    public GameObject content;

    //Ease of access
    public static InventoryManager instance;

    [System.Serializable] //What is a slot?
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

    [System.Serializable] //What is an item?
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
        //Instantiation
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Get the content in the canvas for the slots to be instantiated into
        content = GameObject.FindGameObjectWithTag("Content");

        //Create all the empty slots
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

        //Populate all of the items that exist onto a list
        PopulateItems();
    }

    //Item population
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

    //Add specific item (ONCE)
    public void AddItem(Item item)
    {
        for (int i = 0; i < activeSlots.Count; i++)
        {
            if (activeSlots[i].itemImage == null && activeSlots[i].item == null)
            {
                activeSlots[i].itemImage = item.Image;
                activeSlots[i].item = item;

                break;
            }
        }
    }

    //Testing purposes (adding random items ONCE)
    public void AddRandomItem()
    {
        for (int i = 0; i < activeSlots.Count; i++)
        {
            if (activeSlots[i].itemImage == null && activeSlots[i].item == null)
            {
                int rand = Random.Range(0, existingItems.Count);

                activeSlots[i].itemImage = existingItems[rand].Image;
                activeSlots[i].item = existingItems[rand];

                print("Added item: " + existingItems[rand].Id + " at slot number: " + activeSlots[i].id + " named: " + existingItems[rand].Name);

                break;
            }
        }
    }
}
