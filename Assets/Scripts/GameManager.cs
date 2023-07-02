using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //this boolean represents the state of the game
    //if it is true the game is over
    //if it is false the game is still going on
    [SerializeField]
    private bool isgameover;

    //this method checks for input from the R key
    //and if game over is true
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isgameover == true)
        {
            Debug.Log("R key was pressed.");
            if (isgameover)
            {
                Debug.Log("Game is over, reloading scene.");
                SceneManager.LoadScene(1);
            }
        }
    }
    //this method changes the game over boolean to true, ending the game
    public void gameover()
 {
        isgameover = true; 
 }

}
