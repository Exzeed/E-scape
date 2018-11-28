using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {

    public int enemySpeed, xMoveDirection;
    public float detectionSensor;

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
                SceneManager.LoadScene("TestLevel");
                //Destroy(hit.collider.gameObject);
            }
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
