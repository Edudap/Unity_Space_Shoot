using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
    //this methods just loads the main game
    //bringing the player from the main menu to the actual game 
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
