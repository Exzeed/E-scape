using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed = 1;
    public int jumpPower = 50;
    public float distToPlayerBot = 1.25f;
    public bool grounded;
    public GameObject explosion;

    private float moveX;
    private bool jump;

	// Update is called once per frame
	void Update ()
    {
        PlayerMove();
        PlayerRaycast();

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}

    void PlayerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
        jump = Input.GetButtonDown("Jump");
        //Triggers function when Unity's "Jump" key is pushed
        if (Input.GetButtonDown ("Jump") && grounded == true)
        {
            Jump();
        }

        //animations
        if (moveX != 0 && jump == false && (grounded == true)) //when x-value is changing
        {
            GetComponent<Animator>().SetBool("isRunning", true);
            GetComponent<Animator>().SetBool("isJumping", false);
        }

        else if (jump == true)
        {
            GetComponent<Animator>().SetBool("isJumping", true);
            GetComponent<Animator>().SetBool("isRunning", false);
        }

        else
        {
            GetComponent<Animator>().SetBool("isRunning", false);
            GetComponent<Animator>().SetBool("isJumping", false);
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

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Collided with " + collision.collider.name);
        //when player is on an object tagged as "Platform" bool = true
       if (other.gameObject.tag == "Platform")
            grounded = true;
    }

    void PlayerRaycast()
    {
        //shoots a ray downwards
        RaycastHit2D rayDown = Physics2D.Raycast(transform.position, Vector2.down);
        RaycastHit2D rayDownRight = Physics2D.Raycast(transform.position + (transform.right * 0.5f), Vector2.down);
        RaycastHit2D rayDownLeft = Physics2D.Raycast(transform.position - (transform.right * 0.5f), Vector2.down);


        if (rayDown.collider != null && rayDown.distance < distToPlayerBot && rayDown.collider.tag == "Enemy")
        {
            GameObject other = rayDown.collider.gameObject;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 150);
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 50);
            other.GetComponent<Rigidbody2D>().gravityScale = 3;
            other.GetComponent<Rigidbody2D>().freezeRotation = false;
            other.GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<EnemyController>().enabled = false;
            GetComponent<PlayerScore>().playerScore += 50;
            //Destroy (hit.collider.gameObject);
        }

        if (rayDownRight.collider != null && rayDownRight.distance < distToPlayerBot && rayDownRight.collider.tag == "Enemy")
        {
            GameObject other = rayDownRight.collider.gameObject;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 50);
            other.GetComponent<Rigidbody2D>().gravityScale = 3;
            other.GetComponent<Rigidbody2D>().freezeRotation = false;
            other.GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<EnemyController>().enabled = false;
            GetComponent<PlayerScore>().playerScore += 50;
        }

        if (rayDownLeft.collider != null && rayDownLeft.distance < distToPlayerBot && rayDownLeft.collider.tag == "Enemy")
        {
            GameObject other = rayDownLeft.collider.gameObject;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 100);
            Instantiate(explosion, other.transform.position, other.transform.rotation);
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 50);
            other.GetComponent<Rigidbody2D>().gravityScale = 3;
            other.GetComponent<Rigidbody2D>().freezeRotation = false;
            other.GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<EnemyController>().enabled = false;
            GetComponent<PlayerScore>().playerScore += 50;
        }

        /* if (rayDown.collider != null && rayDown.distance < distanceToBottomofPlayer && rayDown.collider.tag == "Platform")
         {
             grounded = true;
         } */
    }
}