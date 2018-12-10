using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public bool restart = false;
    public Text endText;
    public Text restartText;
    public GameObject explosion;

	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < -7)
        {
            Die();
        }
	}

    void Die ()
    {
        gameObject.GetComponent<PlayerScore>().pauseTime = true;
        endText.color = Color.red;
        endText.text = "Game Over";
        gameObject.GetComponent<PlayerController>().enabled = false;
        SpriteRenderer mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sortingLayerName = "Default";
        restart = true;
        restartText.text = "Press 'R' to restart.";
        if (restart == true)
        {
            //restarts scene
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene("TestLevel");
        }
        //Debug.Log("Player Fell");
        //yield return new WaitForSeconds(2);
        //Debug.Log("Player Died");
    }
}
