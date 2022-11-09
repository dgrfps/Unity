using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public enum PlayerState {
        Playing,
        Inventory
    };

    [Header("Objects")]
        [SerializeField] CharacterController controller;
        [SerializeField] LayerMask groundMask;
        [SerializeField] Transform groundCheck;
        [SerializeField] GameObject[] HideFromCam;

        [SerializeField] AudioClip footstep;

    [Header("Physics")]
        [SerializeField] float walk_speed = 5;
        [SerializeField] float run_speed = 10;
        public float stamina = 100;
        [SerializeField] float jumpHeight = 2;
        [SerializeField] float gravity = 10;

    [Header("Information")]
        public PlayerState playerState;
        [SerializeField] bool isGrounded = false;
        [SerializeField] float groundRadius;
        [SerializeField] Vector3 velocity;
        [SerializeField] Vector3 move;

    float Horizontal, Vertical;
    bool tSprint, isSpriting, canSprint;
    AudioSource source;

    void Start()
    {
        bool viewIsMine = true; //PHOTON 2 INTEGRATION

        source = GetComponent<AudioSource>();
        if(source != null)
        {
            source.loop = true;
            source.clip = footstep;
        }

        playerState = PlayerState.Playing;

        if(viewIsMine)
        {
            foreach(GameObject o in HideFromCam)
                o.SetActive(false);
        }
    }

    void Update()
    {
        CastInputs();
        Stamina();
        CheckGround();
        ApplyGravity();
        Jump();
        Move();
    }
    
    void CastInputs()
    {
        tSprint = Input.GetKey(KeyCode.LeftShift);
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        move = transform.forward * Vertical + transform.right * Horizontal;
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);
    }

    void ApplyGravity()
    {
        velocity.y += -gravity * Time.deltaTime;
        if(isGrounded) velocity.y = -gravity;

        velocity.y = Mathf.Clamp(velocity.y, -100, 100);    
    }

    void Jump()
    {
        if(Input.GetButton("Jump") && isGrounded && playerState == PlayerState.Playing)
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
    }

    void Move()
    {
        if(source != null)
        {
            if(move.magnitude > 0.1)
            {
                if(source.isPlaying == false)
                    source.Play();
            }
            else  if(source.isPlaying) source.Stop();
        }

        if(playerState == PlayerState.Playing)
            controller.Move(move.normalized * (isSpriting ? run_speed : walk_speed) * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    void Stamina()
    {
        if(!tSprint && isSpriting) //Stop sprinting on release
            isSpriting = false;
        
        
        if(!isSpriting && stamina < 100) //Add stamina when not sprinting
        {
            stamina += Random.Range(0.5f, 1) * Time.deltaTime;
        }


        if(tSprint && !isSpriting) //Trying to sprint after recovering;s
        {
            if(!canSprint && stamina >= 100)
            {
                canSprint = true;
            }
        }

        if(canSprint && tSprint && !isSpriting && playerState == PlayerState.Playing) isSpriting = true;

        if(isSpriting && move.magnitude >= 0.2f)
        {
            stamina -= 6.65f * Time.deltaTime;
        }
        
        if(stamina <= 0)
        {
            canSprint = false;
            isSpriting = false;
        }

        if(stamina < 0) stamina = 0;
    }

    [SerializeField] float pushPower = 2.0F;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
    }
}
