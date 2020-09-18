using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;

    //Gets refrenses for the Menue and Cursor
    public GameObject pauseMenue;
    public GameObject gameCursor;

    //Refrenses for player scripts so that they can be disabled during pause
    public PlayerController playerController;
    public PlayerLogic playerLogic;
    public CameraController cameraController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                ReturnToGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        //Disables cursor, enables Menu
        pauseMenue.SetActive(true);
        gameCursor.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;

        //Disables the Player Inputs
        playerController.enabled = false;
        playerLogic.enabled = false;
        cameraController.enabled = false;

        Cursor.lockState = CursorLockMode.None;
    }

    public void ReturnToGame()
    {
        //Enables cursor, disables Menu
        pauseMenue.SetActive(false);
        gameCursor.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;

        //Reenables Player Inputs
        playerController.enabled = true;
        playerLogic.enabled = true;
        cameraController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);         //Resets Current Level
    }

    public void QuitGame()
    {
        Application.Quit();         //Quits the Game
    }

}
