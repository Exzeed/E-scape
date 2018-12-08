using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    private float timeLeft = 120;
    public int playerScore = 0;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;
    public Text clearText;

    void Start()
    {//For testing
        DataManagement.manageData.LoadData();
    }

    // Update is called once per frame
    void Update () {
        //reduce timeLeft by deltaTime (per frame)
        timeLeft -= Time.deltaTime;
        //displays variable values as text
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);


        if (timeLeft < 0.1f){
            SceneManager.LoadScene("TestLevel");
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Exit")
        {
            CountScore();
            clearText.text = "Level Cleared!";
            gameObject.GetComponent<PlayerController>().enabled = false;
            SpriteRenderer mySpriteRenderer = GetComponent<SpriteRenderer>();
            mySpriteRenderer.sortingLayerName = "Default";

            //DataManagement.manageData.SaveData();
        }

        /*if (trigger.gameObject.tag == "Enemy")
        {
            playerScore += 30;
        }*/
    }

    void CountScore()
    {
        //Debug.Log("Data says high score is currently " + DataManagement.manageData.highScore);
        playerScore = playerScore + (int)(timeLeft * 10);
        DataManagement.manageData.highScore = playerScore + (int)(timeLeft * 10);
        //Debug.Log (playerScore);
        DataManagement.manageData.SaveData();
        //Debug.Log("After adding the score to DataManagement, Data says high score is currently " + DataManagement.manageData.highScore);
    }
}
