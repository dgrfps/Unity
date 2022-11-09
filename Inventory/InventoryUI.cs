using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField]  int InvSize = 35;
    [SerializeField]  GameObject Slot, SlotArea, InvPanel;
    bool active = false;

    [SerializeField] PlayerController player;

    [SerializeField] Image p_image;
    [SerializeField] TMPro.TextMeshProUGUI p_name;
    [SerializeField] TMPro.TextMeshProUGUI p_desc;
    
    Item preview;

    private void Start() {
        if(player == null) player = GetComponent<PlayerController>();
        InvPanel.SetActive(false);

        EventManager.RegisterEvent(new GEvent("updateInv", (d) => { 
            foreach(var slot in Slots)
            {
                slot.item = null;
            }

            var items = Inventory.Instance().GetItems();
            for(int i = 0; i < items.Length; i ++)
            {
                Slots[i].item = items[i];
            }

            if(Inventory.Instance().HasItem(preview) == false)
            {
                p_image.sprite = null;
                p_image.color = new Color(1, 1, 1, 0);
                p_desc.text = "";
                p_name.text = "";
            }
        }));

        EventManager.RegisterEvent(new GEvent("setpreview", (d) => {
            preview = (Item) d[0];
            p_image.sprite = preview.sprite;
            p_image.color = new Color(1, 1, 1, 1);
            p_desc.text = string.Join("\n", preview.description);
            p_name.text = preview.name;
            EventManager.CallEvent("updateinv");
        }));

        MakeInv();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            active = !active;
            InvPanel.SetActive(active);

            if(active)
            {
                player.playerState = PlayerController.PlayerState.Inventory;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {             
                player.playerState = PlayerController.PlayerState.Playing;   
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    List<Slot> Slots = new List<Slot>();

    void MakeInv()
    {
        for(int i = 0; i < InvSize; i ++)
        {
            var ns = Instantiate(Slot, SlotArea.transform);
            var s = ns.GetComponent<Slot>();

            Slots.Add(s);
        }
    }

    public void Use()
    {
        if(preview == null)
        return;

        if(preview.id == 2)
        {
            EventManager.CallEvent("spawn_object", "Entities/Wise", new Vector3(28, 2.2f, 18), Quaternion.identity);     
            Destroy();
        }
    }

    void Drop()
    {
        if(preview == null)
        return;
    }

    public void Destroy()
    {
        if(preview == null)
        return;

        Inventory.Instance().RemoveItem(preview);
    }
}