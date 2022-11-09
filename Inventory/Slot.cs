using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    public Item item;
    public Image image;

    void Start()
    {
        image.sprite = null;
        image.color = new Color(1, 1, 1, 0);
    }

    void Update()
    {
        if(item != null)
        {
            if(image != null)
            {
                image.color = Color.white;
                image.sprite = item.sprite;
            }
        }
        else
        {
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);
        }
    }

    public void OnPointerDown(PointerEventData e) {
        if(item != null)
        EventManager.CallEvent("setpreview", item);
    }
}
