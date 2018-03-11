using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    Animator movement;
    private SpriteRenderer spr;
    bool sideSwitched;
    private Rigidbody2D playerRB2D;

	
	void Start () {

        spr = GetComponent<SpriteRenderer>();
        playerRB2D = GetComponent<Rigidbody2D>();
        movement = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
      
        playerRB2D.velocity = new Vector2(moveHorizontal*speed,moveVertical*speed);
        if ((playerRB2D.velocity.x != 0f) || (playerRB2D.velocity.y != 0f))
        {
            movement.Play("walk1");
            if (moveHorizontal < 0 && sideSwitched==false) {  spr.flipX = true; sideSwitched = true; } // rotate player to walk back
            else if(moveHorizontal>0 && sideSwitched == true) { spr.flipX = false; sideSwitched = false; }
        }
        else movement.Play("idle");
    }
}
