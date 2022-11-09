using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Inventory : MonoBehaviour
{
    static Inventory instance;

    public static Inventory Instance()
    {
        return instance;
    }

    [SerializeField] List<Item> items = new List<Item>();


    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            name = "Inventory DB";
        }else
        {
            Destroy(gameObject);
            return;
        }
    }

    public Item[] GetItems()
    {
        return items.ToArray();
    }

    public Item GetItem(int id)
    {
        return items.Find(item => item.id == id);
    }

    public Item GetItem(string name)
    {
        return items.Find(item => item.title.ToLower() == name.ToLower());
    }

    public void AddItem(Item i)
    {
        items.Add(i);
        EventManager.CallEvent("updateInv");     
    }

    public void RemoveItem(Item i)
    {
        if(HasItem(i))
        {
            items.Remove(i);
            EventManager.CallEvent("updateInv");     
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }

    public bool HasItem(int id)
    {
        bool found = false;

        items.ForEach((item) => {
            if(!found) 
                found = (item.id == id);

            if(found) return;
        });

        return found;
    }
}