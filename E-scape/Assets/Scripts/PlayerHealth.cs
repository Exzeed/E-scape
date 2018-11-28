using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.y < -7)
        {
            Die();
        }
	}

    void Die ()
    {
        //resets scene
        SceneManager.LoadScene("TestLevel");
        //Debug.Log("Player Fell");
        //yield return new WaitForSeconds(2);
        //Debug.Log("Player Died");
    }
}
