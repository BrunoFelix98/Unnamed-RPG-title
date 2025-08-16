using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpBehaviour : MonoBehaviour
{
    public PopUpMessage popUp;
    public TextMeshProUGUI textBox;

    private void Start()
    {
        textBox.text = popUp.Text;
    }

    public void PopUp()
    {
        textBox.text = popUp.Text;

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        this.gameObject.SetActive(false);
    }
}
