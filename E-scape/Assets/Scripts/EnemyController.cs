using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

    public int enemySpeed, xMoveDirection;
    public float detectionSensor;
    public bool restart = false;
    public Text endText;
    public Text restartText;
    public GameObject explosion;

	// Update is called once per frame
	void Update () {
        //collision detection
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
        if (hit.distance < detectionSensor)
        {
            Flip();
            if (hit.collider.tag == "Player")
            {
                GameObject other = hit.collider.gameObject;
                Instantiate(explosion, other.transform.position, other.transform.rotation);
                other.GetComponent<PlayerScore>().pauseTime = true;
                endText.color = Color.red;
                endText.text = "Game Over";
                other.GetComponent<PlayerController>().enabled = false;
                other.GetComponent<BoxCollider2D>().enabled = false;
                SpriteRenderer mySpriteRenderer = hit.collider.GetComponent<SpriteRenderer>();
                mySpriteRenderer.sortingLayerName = "Default";
                restart = true;
                restartText.text = "Press 'R' to restart.";

                //Destroy(hit.collider.gameObject);
            }
        }
        if (restart == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("TestLevel");
        }
	}

    void Flip ()
    {
        if (xMoveDirection > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            xMoveDirection = -1;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            xMoveDirection = 1;
        }
    }
}
