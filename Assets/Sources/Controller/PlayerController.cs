using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkspeed;
    public float runningspeed;

    private float initialspeed;

    Animator movement;
    private SpriteRenderer spr;
    bool sideSwitched;
    private Rigidbody2D playerRB2D;

	
	private void Start () {
        initialspeed = walkspeed;

        spr = GetComponent<SpriteRenderer>();
        playerRB2D = GetComponent<Rigidbody2D>();
        movement = GetComponent<Animator>();
	}

    private void Update()
    {
        if (Input.GetKey("left shift")) { walkspeed = runningspeed; } else { walkspeed = initialspeed; }
    }

    private void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

       
      
        playerRB2D.velocity = new Vector2(moveHorizontal* walkspeed, moveVertical* walkspeed);
        if ((playerRB2D.velocity.x != 0f) || (playerRB2D.velocity.y != 0f))
        {
            movement.Play("walk1");
            if (moveHorizontal < 0 && sideSwitched==false) { transform.Rotate(Vector3.up * 180); sideSwitched = true; } // rotate player to walk back
            else if(moveHorizontal>0 && sideSwitched == true) { transform.Rotate(-Vector3.up * 180); sideSwitched = false; }
        }
        else movement.Play("idle");

    }
}
