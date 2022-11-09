using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Item")]
public class Item : ScriptableObject
{
    public int id;
    public string title;
    public string[] description;
    public Sprite sprite;
    public Dictionary<string, string> dataHolder = new Dictionary<string, string>();

    public Item(int id, string icon, string title, string[] desc, Dictionary<string, string> dh)
    {
        this.id = id;
        this.title = title;
        this.description = desc;
        this.sprite = Resources.Load<Sprite>(icon);
        this.dataHolder = dh;
    }
}