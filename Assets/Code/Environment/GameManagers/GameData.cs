using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    //Singleton
    public static GameData instance;

    //Database
    public DialogMessage[] allDialogMessages;
    public PopUpMessage[] allPopUpMessages;
    public Spell[] allSpells;
    public Item[] allItems;
    public Slot[] allSlots;
    public GameObject[] allPhysicalSlots;
    public List<GameObject> itemPrefabs = new List<GameObject>();
    public List<GameObject> popUpPrefabs = new List<GameObject>();

    //Current inventory items
    public List<Item> inventoryItems = new List<Item>();

    //Default values
    public List<Slot> slots = new List<Slot>();
    public Sprite slotBackground;
    public GameObject itemPrefab;
    public GameObject slotPrefab;
    public GameObject popUpPrefab;

    //Combat related
    public bool inCombat;
    public string currentCode;
    public GameObject player;
    public GameObject target;
    public GameObject[] targets;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        inCombat = false;
        currentCode = "";

        for (int i = 0; i < allItems.Length; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            newItem.GetComponent<ItemData>().item = allItems[i];
            itemPrefabs.Add(newItem);
            newItem.SetActive(false);
        }

        for (int i = 0; i < allPopUpMessages.Length; i++)
        {
            GameObject newPopUp = Instantiate(popUpPrefab, transform.position, Quaternion.identity);
            newPopUp.GetComponent<PopUpBehaviour>().popUp = allPopUpMessages[i];
            popUpPrefabs.Add(newPopUp);
            newPopUp.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        targets = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (inCombat)
        {
            //If the player is in combat they will be able to cast spells
            //Each spell has a 4 digit code of 1s and 0s
            //The player presses the left or right mouse buttons to create the spell code
            //If it is valid and unlocked, the spell will cast

            if (Input.GetMouseButtonDown(0))
            {
                currentCode = currentCode + "0";
            }
            else if (Input.GetMouseButtonDown(1))
            {
                currentCode = currentCode + "1";
            }
        }

        if (currentCode.ToCharArray().Length.Equals(4))
        {
            TriggerSpell(currentCode);
        }


    }

    //Trigger a spell
    public void TriggerSpell(string spellCode)
    {
        for (int i = 0; i < allSpells.Length; i++)
        {
            if (allSpells[i].Code.Equals(spellCode))
            {
                if (allSpells[i].Unlocked)
                {
                    allSpells[i].Activate(spellCode, GetClosestTarget(), player.transform); //Successful cast!
                }
                else
                {
                    popUpPrefabs[0].GetComponent<PopUpBehaviour>().PopUp(player.transform); //You don't know that yet!
                }

                currentCode = ""; //Reset spell code
                return; //Return here to exit the function. Do this in order for the function not to check if the spell exists again, since the spell "" doesnt exist
            }
        }

        //This is here for when we dont use the 4 digit combination that the player inputted
        foreach (var spell in allSpells)
        {
            if (spell.Code != spellCode)
            {
                popUpPrefabs[3].GetComponent<PopUpBehaviour>().PopUp(player.transform); //Spell doesn't exist!
            }

            currentCode = ""; //Reset spell code
            return; //Return here to exit the function
        }
    }

    //Get the closest target to the player
    public GameObject GetClosestTarget()
    {
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        for (int i = 0; i < targets.Length; i++)
        {
            float distance = Vector2.Distance(player.transform.position, targets[i].transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = targets[i];
            }
        }

        return closestEnemy;
    }

    //Add specific item (ONCE)
    public void AddItem(Item item)
    {
        for (int i = 0; i < allSlots.Length; i++)
        {
            if (allSlots[i].ItemImage == null && allSlots[i].Item == null)
            {
                allSlots[i].ItemImage = item.Image;
                allSlots[i].Item = item;

                print("Added item: " + item + " at slot number: " + i + " named: " + item.ItemName);

                inventoryItems.Add(item);

                for (int j = 0; j < allPhysicalSlots.Length; j++)
                {
                    Sprite sprite = allPhysicalSlots[j].transform.GetChild(1).GetComponent<Image>().sprite;

                    if (sprite.Equals(null))
                    {
                        sprite = item.Image;
                    }
                    else
                    {
                        popUpPrefabs[4].GetComponent<PopUpBehaviour>().PopUp(player.transform); //Inventory full!
                    }
                }

                break;
            }
        }
    }

    public void UnlockSpell(string spellName)
    {
        foreach (var spell in allSpells)
        {
            if (!spell.SpellName.Equals(spellName))
            {
                popUpPrefabs[3].GetComponent<PopUpBehaviour>().PopUp(player.transform); //Spell doesn't exist!
            }
            else if (spell.SpellName.Equals(spellName) && spell.Unlocked.Equals(true))
            {
                popUpPrefabs[2].GetComponent<PopUpBehaviour>().PopUp(player.transform); //You already know that spell!
            }
            else if (spell.SpellName.Equals(spellName) && spell.Unlocked.Equals(false))
            {
                spell.Unlocked = true;
                popUpPrefabs[1].GetComponent<PopUpBehaviour>().PopUp(player.transform); //Spell unlocked!
            }

            currentCode = ""; //Reset spell code
            return; //Return here to exit the function
        }
    }
}
