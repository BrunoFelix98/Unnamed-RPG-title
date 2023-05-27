using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Message
{
    [SerializeField]
    private int id;
    [SerializeField]
    private string text;

    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Text
    {
        get => text;
        set => text = value;
    }

    public Message(int id, string text)
    {
        Id = id;
        Text = text;
    }
}
