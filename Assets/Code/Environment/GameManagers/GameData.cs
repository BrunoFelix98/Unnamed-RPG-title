using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    //Current unlocked spells
    public List<Spell> playerSpells;

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
        playerSpells = player.GetComponent<PlayerData>().UnlockedSpells;
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

        //Unlock Fireball Test
        if (Input.GetKeyDown("u"))
        {
            UnlockSpell(0);
        }

        //Unlock Fireball Test
        if (Input.GetKeyDown("l"))
        {
            UnlockSpell(3);
        }

        //Trigger a spell
        if (currentCode.ToCharArray().Length.Equals(4))
        {
            TriggerSpell(currentCode);
        }

        //Overflow Failsafe
        if (currentCode.ToCharArray().Length > 4)
        {
            currentCode = ""; //Reset spell code
        }
    }

    //Trigger a spell
    public void TriggerSpell(string spellCode)
    {
        //If the player does not have any spells unlocked, IE at the start of the game, they shouldn't be able to cast anything (This code will likely never run)
        if (playerSpells.Count < 1)
        {
            //UI representation
            popUpPrefabs[13].SetActive(true);
            popUpPrefabs[13].GetComponent<PopUpBehaviour>().PopUp(); //You dont know any spells yet!

            currentCode = ""; //Reset spell code
            return; //Return here to exit the function. Do this in order for the function not to check if the spell exists again, since the spell "" doesnt exist
        }

        //Check if the spell exists
        Spell spellToCast = null;
        foreach (var spell in allSpells)
        {
            if (spell.Code.Equals(spellCode))
            {
                spellToCast = spell;
                break; //Spell exists
            }
        }

        //If the spell doesn't exist, say it
        if (spellToCast == null)
        {
            //UI representation
            popUpPrefabs[3].SetActive(true);
            popUpPrefabs[3].GetComponent<PopUpBehaviour>().PopUp(); //Spell doesn't exist!

            currentCode = ""; //Reset spell code
            return; //Return here to exit the function. Do this in order for the function not to check if the spell exists again, since the spell "" doesnt exist
        }

        if (!playerSpells.Contains(spellToCast))
        {
            //UI representation
            popUpPrefabs[0].SetActive(true);
            popUpPrefabs[0].GetComponent<PopUpBehaviour>().PopUp(); //You don't know that yet!

            currentCode = ""; //Reset spell code
            return; //Return here to exit the function. Do this in order for the function not to check if the spell exists again, since the spell "" doesnt exist
        }

        if (spellToCast.Equals(allSpells[1]) || spellToCast.Equals(allSpells[3]))
        {
            spellToCast.ActivateSelf(spellCode, player.transform); //Successful cast!

            currentCode = ""; //Reset spell code
            return; //Return here to exit the function. Do this in order for the function not to check if the spell exists again, since the spell "" doesnt exist
        }
        else
        {
            if (targets.Length < 1)
            {
                //UI representation
                popUpPrefabs[11].SetActive(true);
                popUpPrefabs[11].GetComponent<PopUpBehaviour>().PopUp(); //No target present!
            }
            else
            {
                spellToCast.ActivateEnemy(spellCode, GetClosestTarget(), player.transform); //Successful cast!

                currentCode = ""; //Reset spell code
                return; //Return here to exit the function. Do this in order for the function not to check if the spell exists again, since the spell "" doesnt exist
            }
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
                        popUpPrefabs[4].GetComponent<PopUpBehaviour>().PopUp(); //Inventory full!
                    }
                }

                break;
            }
        }
    }

    public void UnlockSpell(int spellInt)
    {
        if (!allSpells[spellInt])
        {
            //UI representation
            popUpPrefabs[3].SetActive(true);
            popUpPrefabs[3].GetComponent<PopUpBehaviour>().PopUp(); //Spell doesn't exist!
        }
        else if (allSpells[spellInt] && playerSpells.Contains(allSpells[spellInt]))
        {
            //UI representation
            popUpPrefabs[2].SetActive(true);
            popUpPrefabs[2].GetComponent<PopUpBehaviour>().PopUp(); //You already know that spell!
        }
        else if (allSpells[spellInt] && !playerSpells.Contains(allSpells[spellInt]))
        {
            playerSpells.Add(allSpells[spellInt]);

            //UI representation
            popUpPrefabs[1].SetActive(true);
            popUpPrefabs[1].GetComponent<PopUpBehaviour>().PopUp(); //Spell unlocked!
        }
    }
}
