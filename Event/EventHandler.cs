using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    void Start()
    {
        EventManager.RegisterEvent(new GEvent("spawn_object", (d) => { 
            Debug.Log(d[0].ToString());
            var obj = Resources.Load(d[0].ToString());
            if(obj == null)
            {
                Debug.LogError("Object " + d[0].ToString() + " doesn't exist");
                return;
            }
            Instantiate(obj, (Vector3) d[1], (Quaternion) d[2]);
        }));

        //StartCoroutine(Delay(5)); 
    }
}