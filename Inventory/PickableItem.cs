using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public Item targetItem;
    public Outline outline;

    GameObject pick;

    private void Start() {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode=Outline.Mode.OutlineVisible;
        outline.OutlineColor=Color.red;
        outline.OutlineWidth=3;
        
        outline.enabled = false;
        pick = Instantiate(Resources.Load<GameObject>("UI/PickItem"), transform);
    }

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);;
        RaycastHit hit;
        
        if(pick.activeSelf)
        {
            pick.transform.position = transform.position + Vector3.up;
            pick.transform.LookAt(Camera.main.transform);
            pick.transform.Rotate(Vector3.up * 180);
        }

        if(Physics.Raycast(ray, out hit, 2))
        {
            if(hit.transform.gameObject == gameObject)
            {
                outline.enabled = true;
                pick.SetActive(true);

                if(Input.GetKey(KeyCode.F))
                {
                    Inventory.Instance().AddItem(targetItem);
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            outline.enabled = false;
            pick.SetActive(false);                
        }
    }
}
