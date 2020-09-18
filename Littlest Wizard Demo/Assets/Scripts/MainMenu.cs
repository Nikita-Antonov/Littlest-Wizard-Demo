using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //This is the simple script that runs the Main Menue of the game

    //This function tells the Scene Manager to load the next scene after the Main Menue
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //This fucntion allows you to quit out of the aplication
    public void Quit()
    {
        Application.Quit();
    }
}
