using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

    public bool isPlayerControllable;
    public Vector3 PlayerPosition;
    public Vector3 PlayerVelocity;

    public float walkspeed;
    public float runningspeed;

    private float initialspeed;

    Animator movement;
    private SpriteRenderer spr;
    bool sideSwitched;
    private Rigidbody2D playerRB2D;

    private Collider2D PlayerCollider;

    [HideInInspector]
    public float PlayerExtentsX;
    [HideInInspector]
    public float PlayerExtentsY;

    private float moveHorizontal;
    private float moveVertical;

    private bool isrInDoorTransition;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        playerRB2D = GetComponent<Rigidbody2D>();
        movement = GetComponent<Animator>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();
    }
    private void Start ()
    {
        isPlayerControllable = true;

        initialspeed = walkspeed;

        PlayerExtentsX = PlayerCollider.bounds.extents.x;
        PlayerExtentsY = PlayerCollider.bounds.extents.y;
    }

    private void Update()
    {
        PlayerPosition = transform.position;
        PlayerVelocity = playerRB2D.velocity;

        if (Input.GetKey("left shift")) { walkspeed = runningspeed; } else { walkspeed = initialspeed; }
    }

    private void FixedUpdate () {

        if (isPlayerControllable == true)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            playerRB2D.velocity = new Vector2(moveHorizontal * walkspeed, moveVertical * walkspeed);

            if ((playerRB2D.velocity.x != 0f) || (playerRB2D.velocity.y != 0f))
            {
                movement.Play("walk1");
                if (moveHorizontal < 0 && sideSwitched == false) { transform.Rotate(Vector3.up * 180); sideSwitched = true; } // rotate player to walk back
                else if (moveHorizontal > 0 && sideSwitched == true) { transform.Rotate(-Vector3.up * 180); sideSwitched = false; }
            }
            else movement.Play("idle");
        }
        else
        {
            playerRB2D.velocity = Vector3.zero;
        }

    }
}
