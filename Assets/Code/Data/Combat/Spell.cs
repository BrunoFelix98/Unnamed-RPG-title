using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell", order = 2)]
public class Spell : ScriptableObject
{
    [SerializeField]
    private string spellName;
    [SerializeField]
    private string description;
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

    public int Damage
    {
        get => damage;
        set => damage = value;
    }

    public void ActivateEnemy(string spellCode, GameObject target, Transform playerPosition)
    {
        PlayerData playerdata = playerPosition.GetComponent<PlayerData>();
        EnemyBehaviour enemyBehaviour = target.GetComponent<EnemyBehaviour>();

        switch (spellCode)
        {
            case "0000": //Fireball
                //Deal damage to the target
                //UI representation
                GameData.instance.popUpPrefabs[5].SetActive(true);
                GameData.instance.popUpPrefabs[5].GetComponent<PopUpBehaviour>().PopUp(); //Fireball!
                enemyBehaviour.TakeDamage(Damage, target.transform);
                break;
            case "0010": //Stun
                //Stop enemy from being able to do anything for a certain amount of time
                //UI representation
                GameData.instance.popUpPrefabs[7].SetActive(true);
                GameData.instance.popUpPrefabs[7].GetComponent<PopUpBehaviour>().PopUp(); //Get stunned!
                enemyBehaviour.enemyData.EnemyIsStunned = true;
                break;
            default:
                break;
        }
    }

    public void ActivateSelf(string spellCode, Transform playerPosition)
    {
        PlayerData playerdata = playerPosition.GetComponent<PlayerData>();

        switch (spellCode)
        {
            case "0001": //Deflect
                //Stop player from being damaged
                playerdata.playerIsImmune = true;

                //UI representation
                GameData.instance.popUpPrefabs[6].SetActive(true);
                GameData.instance.popUpPrefabs[6].GetComponent<PopUpBehaviour>().PopUp(); //Deflected!
                break;
            case "0100": //Heal
                //Heal the player
                playerdata.Heal(Damage);

                //UI representation
                GameData.instance.popUpPrefabs[8].SetActive(true);
                GameData.instance.popUpPrefabs[8].GetComponent<PopUpBehaviour>().PopUp(); //Healed!
                break;
            default:
                break;
        }
    }
}