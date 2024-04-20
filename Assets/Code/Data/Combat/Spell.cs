using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell", order = 2)]
public class Spell : ScriptableObject
{
    [SerializeField]
    private string spellName;
    [SerializeField]
    private string description;
    [SerializeField]
    private bool unlocked;
    [SerializeField]
    private string code; //The code used to activate it, for example 1100 is the same as clicking "right-right-left-left" with the mouse "1" means right, "0" means left
    [SerializeField]
    private int damage;

    public string SpellName
    {
        get => spellName;
        set => spellName = value;
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

    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    public void Activate(string spellCode, GameObject target, Transform playerPosition)
    {
        PlayerData playerdata = playerPosition.GetComponent<PlayerData>();
        EnemyBehaviour enemyBehaviour = target.GetComponent<EnemyBehaviour>();

        switch (spellCode)
        {
            case "0000": //Fireball
                //Deal damage to the target
                GameData.instance.popUpPrefabs[5].GetComponent<PopUpBehaviour>().PopUp(playerPosition); //Fireball!
                System.Threading.Thread.Sleep(200); //Wait for 0.2 seconds
                enemyBehaviour.TakeDamage(Damage, target.transform);
                break;
            case "0001": //Deflect
                //Stop player from being damaged
                playerdata.playerIsImmune = true;
                GameData.instance.popUpPrefabs[6].GetComponent<PopUpBehaviour>().PopUp(playerPosition); //Deflected!
                System.Threading.Thread.Sleep(1000); //Wait for 1 second
                playerdata.playerIsImmune = false;
                break;
            case "0010": //Stun
                //Stop enemy from being able to do anything for a certain amount of time
                GameData.instance.popUpPrefabs[7].GetComponent<PopUpBehaviour>().PopUp(playerPosition); //Get stunned!
                System.Threading.Thread.Sleep(200); //Wait for 0.2 seconds
                enemyBehaviour.enemyData.EnemyIsStunned = true;
                System.Threading.Thread.Sleep(1000); //Wait for 1 second
                enemyBehaviour.enemyData.EnemyIsStunned = false;
                break;
            case "0100": //Heal
                //Heal the player
                playerdata.Heal(Damage);
                GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().textBox.text = "+" + Damage + "!";
                GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().textBox.color = Color.green;
                GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().PopUp(playerPosition);
                System.Threading.Thread.Sleep(200); //Wait for 0.2 seconds
                GameData.instance.popUpPrefabs[8].GetComponent<PopUpBehaviour>().PopUp(playerPosition); //Healed!
                break;
            default:
                break;
        }
    }
}