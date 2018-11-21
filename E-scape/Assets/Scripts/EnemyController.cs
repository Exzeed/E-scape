using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public int enemySpeed, xMoveDirection;

	// Update is called once per frame
	void Update () {
        //collision detection
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(xMoveDirection, 0));
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(xMoveDirection, 0) * enemySpeed;
        if (hit.distance <0.7f)
            Flip();
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
