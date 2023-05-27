using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spells : MonoBehaviour
{
    //This script handles everything related to spells, their creation, casting, activation and learning

    public static Spells instance;

    public bool inCombat;
    public string currentCode;

    [SerializeField]
    private List<Spell> spells = new List<Spell>();

    public GameObject player;
    public GameObject target;
    public GameObject[] targets;

    private void Awake()
    {
        instance = this;
        inCombat = false;
        currentCode = "";
    }

    void Start()
    {
        PopulateSpells();

        player = GameObject.FindGameObjectWithTag("Player");
        targets = GameObject.FindGameObjectsWithTag("Enemy");
    }

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

    public void PopulateSpells()
    {
        spells.Add(new Spell(0, "Fireball", "Shoot out a fireball, hopefully you wont cause a forest fire...", "0000", false));
        spells.Add(new Spell(1, "Deflect", "Whatever it is that they threw at you, it's now deflected!", "0001", false));
        spells.Add(new Spell(2, "Stun", "Your target is now stunned, they cannot move or act, they are at your mercy.", "0010", false));
        spells.Add(new Spell(3, "Heal", "Cover some of your wounds.", "0100", false));
    }

    public void TriggerSpell(string spellCode)
    {
        for (int i = 0; i < spells.Count; i++)
        {
            if (spells[i].Code.Equals(spellCode))
            {
                if (spells[i].Unlocked)
                {
                    spells[i].Activate(spellCode); //Successful cast!
                }
                else
                {
                    print("Spell not unlocked yet."); //The player hasn't unlocked this spell yet
                }

                currentCode = ""; //Reset spell code
                return; //Return here to exit the function. Do this in order for the function not to check if the spell exists again, since the spell "" doesnt exist
            }
        }

        //This is here for when we dont use the 4 digit combination that the player inputted
        foreach (var spell in spells)
        {
            if (spell.Code != spellCode)
            {
                print("Spell doesn't exist"); //This spell doesn't exist
            }

            currentCode = ""; //Reset spell code
            return; //Return here to exit the function
        }
    }

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

    public void FireBall()
    {
        target = GetClosestTarget();
    }

    public void Deflect()
    {
        target = player;
        //player.immune = true;
    }

    public void Stun()
    {
        target = GetClosestTarget();
    }

    public void Heal()
    {
        target = player;
        //player.currentHealth += 5
    }
}
