using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed = 1;
    public int jumpPower = 50;
    public bool grounded;

    private float moveX;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerMove();
	}

    void PlayerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
        //Triggers function when Unity's "Jump" key is pushed
        if (Input.GetButtonDown ("Jump") && grounded == true)
        {
            Jump();
        }

        //animations
        if (moveX != 0 && (grounded == true)) //when x-value is changing
        {
            GetComponent<Animator>().SetBool("isRunning", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
        }

        //player direction
        if (moveX < 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        //movement on x-axis is negative = facing left
        else if (moveX > 0.0f)
        {
            GetComponent<SpriteRenderer>().flipX = false;   
        }

        //physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //Adding to the y-value by 1 * jumpPower
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
        grounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collided with " + collision.collider.name);
        //when player is on an object tagged as "Platform" bool = true
        if (collision.gameObject.tag == "Platform")
            grounded = true;
    }

    //TEMPORARY
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Exit")
            Debug.Log("CONGRATULATIONS!!! YOU REACHED THE END!");
    }
}