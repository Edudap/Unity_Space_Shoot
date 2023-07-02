using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    //this text displays the player score
    [SerializeField]
    private Text score_text;

    //this displays the players remaining lives
    [SerializeField]
    private Image livesimage;
    [SerializeField]

    //this holds the array of the sprites to be displayed according to damage
    private Sprite[] livesprites;

    //these display the game over and restart text 
    [SerializeField]
    private Text gameoverText;
    [SerializeField]
    private Text restarttext;

    //reference to the game manager script to help control the states of the game
    private GameManager gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        
        score_text.text = "Score" + 0;
        gameoverText.gameObject.SetActive(false);
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (gamemanager == null)
        {
            Debug.LogError("gamemanager is null");
        }
    }

    // Update is called once per frame

    //this method initializes the score text
    //hides the game over and restart text
    void Update()
    {
        
    }
    //this method updates the score as we destroy more enemy space ships
    public void UpdateScore(int playerScore)
    {
        score_text.text = "Score " + playerScore.ToString();
    }

    //this method manages our lives
    //if we have no lives left, it calls the game over sequence
    public void updatelives(int currentlives)
    {
        livesimage.sprite = livesprites[currentlives];

        if (currentlives == 0)
        {
            GameOverSequence();
        }


        //this activates the game over and restart text 
        void GameOverSequence()
        {
            
            gamemanager.gameover();
            gameoverText.gameObject.SetActive(true);
            restarttext.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
        }
    }

    //this coroutine flickers the game over text every half of a second
    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            gameoverText.text = "Game Over";
            yield return new WaitForSeconds(0.5f);
            gameoverText.text = "";
            yield return new WaitForSeconds(0.5f);

        }
    }
}
