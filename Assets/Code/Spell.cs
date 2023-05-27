using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string name;
    [SerializeField]
    private string description;
    [SerializeField]
    private bool unlocked;
    [SerializeField]
    private string code; //The code used to activate it, for example 1100 is the same as clicking "right-right-left-left" with the mouse "1" means right, "0" means left

    public int Id
    {
        get => id;
        set => id = value;
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

    public string Code
    {
        get => code;
        set => code = value;
    }

    public bool Unlocked
    {
        get => unlocked;
        set => unlocked = value;
    }

    public Spell(int id, string name, string description, string code, bool unlocked)
    {
        Id = id;
        Name = name;
        Description = description;
        Code = code;
        Unlocked = unlocked;
    }

    public void Activate(string spellCode)
    {
        switch (spellCode)
        {
            case "0000": //Fireball
                Spells.instance.FireBall();
                Debug.Log("Fireball was activated.");
                break;
            case "0001": //Deflect
                Spells.instance.Deflect();
                Debug.Log("Deflect was activated.");
                break;
            case "0010": //Stun
                Spells.instance.Stun();
                Debug.Log("Stun was activated.");
                break;
            case "0100": //Heal
                Spells.instance.Heal();
                Debug.Log("Heal was activated.");
                break;
            default:
                break;
        }
    }
}