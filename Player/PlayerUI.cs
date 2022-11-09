using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    [SerializeField] Camera minimap;
    [SerializeField] Slider Stamina;
    [SerializeField] float size = 40;//Zoom size

    [SerializeField] PlayerController player;

    private void Start() {
        if(player == null)
            player = GetComponent<PlayerController>();
    }
    
    private void Update() {
        if(Stamina != null)
        {
            Stamina.value = player.stamina;
        }
        
        if(minimap != null)
        {
            if(Input.GetKey(KeyCode.Minus))
                size += 15 * Time.deltaTime;

            if(Input.GetKey(KeyCode.Equals))
                size -= 15 * Time.deltaTime;
            
            size = Mathf.Clamp(size, 20, 60);
            minimap.orthographicSize = size;
        }
    }

}