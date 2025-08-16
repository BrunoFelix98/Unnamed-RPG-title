using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool playerIsStunned;
    public bool playerIsImmune;
    public List<Spell> UnlockedSpells = new List<Spell>();

    // Start is called before the first frame update
    void Start()
    {
        playerIsImmune = false;
        playerIsStunned = false;
    }

    void Update()
    {
        if (playerIsImmune)
        {
            StartCoroutine(ImmuneTimer());
        }

        if (playerIsStunned)
        {
            StartCoroutine(StunTimer());
        }
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
            GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().PopUp();
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

                //UI representation
                GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().popUp.Text = "+" + amount;
                GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().textBox.color = Color.green;
                GameData.instance.popUpPrefabs[9].SetActive(true);
                GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().PopUp();
            }
            else
            {
                currentHealth = maxHealth; //The value of the healing exceeds max health, so just set it as such

                //UI representation
                GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().popUp.Text = "+" + amount;
                GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().textBox.color = Color.green;
                GameData.instance.popUpPrefabs[9].SetActive(true);
                GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().PopUp();
            }
        }
        else
        {
            //UI representation
            GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().popUp.Text = "+" + 0;
            GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().textBox.color = Color.green;
            GameData.instance.popUpPrefabs[9].SetActive(true);
            GameData.instance.popUpPrefabs[9].GetComponent<PopUpBehaviour>().PopUp();
        }
    }

    IEnumerator ImmuneTimer()
    {
        yield return new WaitForSeconds(2);
        playerIsImmune = false;
    }

    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(2);
        playerIsStunned = false;
    }
}
