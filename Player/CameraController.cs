using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// SCRIPT BY Brackeys LOCATED AT YOUTUBE: FIRST PERSON MOVEMENT in Unity - FPS Controller

public class CameraController : MonoBehaviour
{
    [SerializeField] float sensivity_x = 1;
    [SerializeField] float sensivity_y = 1;
    
    [SerializeField] Transform body;
    float rotation = 0;
    
    PlayerController p;

    private void Start() {
        p = body.GetComponent<PlayerController>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() {
        float x = Input.GetAxis("Mouse X") * sensivity_x * 100 * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * sensivity_y * 100 * Time.deltaTime;
        
        rotation -= y;
        rotation = Mathf.Clamp(rotation, -80, 80);

        if(p.playerState == PlayerController.PlayerState.Playing)
        {
            transform.localRotation = Quaternion.Euler(rotation, 0, 0);
            body.Rotate(Vector3.up * x);
        }
    }
}
