using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool playerIsStunned;
    public bool playerIsImmune;

    // Start is called before the first frame update
    void Start()
    {
        playerIsImmune = false;
        playerIsStunned = false;
    }

    public void PickUpItem(GameObject itemObj)
    {
        GameData.instance.AddItem(itemObj.GetComponent<ItemData>().item);
        Destroy(itemObj);
    }

    public void TakeDamage(int amount, Transform playerPos)
    {
        if (currentHealth - amount > 0)
        {
            currentHealth -= amount;
            GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().textBox.text = "-" + amount + "!";
            GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().textBox.color = Color.red;
            GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().PopUp(playerPos);
        }
        else
        {
            //Kill the player and trigger lost screen
        }
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth)
        {
            if (currentHealth + amount <= maxHealth)
            {
                currentHealth += amount; //Add the amount
            }
            else
            {
                currentHealth = maxHealth; //The value of the healing exceeds max health, so just set it as such
            }
        }
    }
}
