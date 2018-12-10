using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour {

    private float timeLeft = 120;
    public int playerScore = 0;
    public bool pauseTime = false;
    public bool restart = false;
    public bool clear = false;
    public GameObject timeLeftUI;
    public GameObject playerScoreUI;
    public GameObject explosion;
    public Text endText;
    public Text highscoreText;
    public Text restartText;
    public Text menuText;

    void Start()
    {//For testing
        DataManagement.manageData.LoadData();
    }

    // Update is called once per frame
    void Update () {
        //reduce timeLeft by deltaTime (per frame)
        if (pauseTime == false)
            timeLeft -= Time.deltaTime;
        //displays variable values as text
        timeLeftUI.gameObject.GetComponent<Text>().text = ("Time Left: " + (int)timeLeft);
        playerScoreUI.gameObject.GetComponent<Text>().text = ("Score: " + playerScore);


        if (timeLeft < 0.1f){
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            pauseTime = true;
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
        }
        if (clear == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene("MainMenu");
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Exit")
        {
            pauseTime = true;
            endText.text = "Level Cleared!";
            gameObject.GetComponent<PlayerController>().enabled = false;
            SpriteRenderer mySpriteRenderer = GetComponent<SpriteRenderer>();
            mySpriteRenderer.sortingLayerName = "Default";
            CountScore();
            menuText.text = "Press 'Enter' for Main Menu.";
            clear = true;
            //DataManagement.manageData.SaveData();
        }
    }

    void CountScore()
    {
        //Debug.Log("Data says high score is currently " + DataManagement.manageData.highScore);
        playerScore = playerScore + (int)(timeLeft * 10);
        if(playerScore > (int)DataManagement.manageData.highScore)
            DataManagement.manageData.highScore = playerScore;
        highscoreText.text = ("Highscore: " + (int)DataManagement.manageData.highScore);
        //Debug.Log (playerScore);
        DataManagement.manageData.SaveData();
        //Debug.Log("After adding the score to DataManagement, Data says high score is currently " + DataManagement.manageData.highScore);
    }
}
