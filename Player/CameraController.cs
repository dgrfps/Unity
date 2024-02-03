using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Input Axis X are scaled by how much we moved the mouse, should not be scaled by time.deltaTime
        float x = Input.GetAxis("Mouse X") * sensivity_x; 
        float y = Input.GetAxis("Mouse Y") * sensivity_y;

        rotation -= y;
        rotation = Mathf.Clamp(rotation, -80, 80);

        if(p.playerState == PlayerController.PlayerState.Playing)
        {
            transform.localRotation = Quaternion.Euler(rotation, 0, 0);
            body.Rotate(Vector3.up * x);
        }
    }
}