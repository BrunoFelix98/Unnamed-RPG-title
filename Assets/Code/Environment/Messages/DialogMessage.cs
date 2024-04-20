using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogMessage", menuName = "ScriptableObjects/DialogMessage", order = 0)]
public class DialogMessage : ScriptableObject
{
    [SerializeField]
    private string text;

    public string Text
    {
        get => text;
        set => text = value;
    }

    public DialogMessage(string text)
    {
        Text = text;
    }
}
