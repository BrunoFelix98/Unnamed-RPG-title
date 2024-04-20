using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "PopUpMessage", menuName = "ScriptableObjects/PopUpMessage", order = 1)]
public class PopUpMessage : ScriptableObject
{
    [SerializeField]
    private string text;

    public string Text
    {
        get => text;
        set => text = value;
    }

    public PopUpMessage(string text)
    {
        Text = text;
    }
}
