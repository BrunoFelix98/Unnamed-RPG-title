using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpBehaviour : MonoBehaviour
{
    public PopUpMessage popUp;
    public TextMeshProUGUI textBox;

    public void PopUp(Transform transform)
    {
        GameObject newPopUp = Instantiate(GameData.instance.popUpPrefab, transform.position, Quaternion.identity);
        System.Threading.Thread.Sleep(2000); //Wait for 2 seconds
        Destroy(newPopUp);
    }
}
