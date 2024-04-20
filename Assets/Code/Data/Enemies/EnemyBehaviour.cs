using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public Enemy enemyData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage(GameObject target)
    {
        PlayerData player = target.GetComponent<PlayerData>();

        if (!enemyData.EnemyIsStunned)
        {
            if (!player.playerIsImmune)
            {
                player.TakeDamage(enemyData.EnemyDamage, player.transform);
            }
        }
    }

    public void TakeDamage(int amount, Transform enemyPosition)
    {
        if (enemyData.EnemyCurrentHitpoints - amount > 0)
        {
            enemyData.EnemyCurrentHitpoints -= amount;
            GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().textBox.text = "-" + amount + "!";
            GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().textBox.color = Color.red;
            GameData.instance.popUpPrefabs[GameData.instance.allPopUpMessages.Length].GetComponent<PopUpBehaviour>().PopUp(enemyPosition);
        }
        else
        {
            //Kill this enemy
        }
    }

    public void DropItems(Transform enemyPosition)
    {
        if (enemyData.EnemyInventory.Length > 0)
        {
            for (int i = 0; i < enemyData.EnemyInventory.Length; i++)
            {
                for (int j = 0; j < GameData.instance.itemPrefabs.Count; j++)
                {
                    if (enemyData.EnemyInventory[i] = GameData.instance.itemPrefabs[j].GetComponent<ItemData>().item)
                    {
                        GameObject droppedItem = Instantiate(GameData.instance.itemPrefabs[j], enemyPosition.position, Quaternion.identity);
                        droppedItem.GetComponent<ItemData>().child.GetComponent<Image>().sprite = droppedItem.GetComponent<ItemData>().item.Image;
                        droppedItem.SetActive(true);
                    }
                }
            }
        }
    }
}
