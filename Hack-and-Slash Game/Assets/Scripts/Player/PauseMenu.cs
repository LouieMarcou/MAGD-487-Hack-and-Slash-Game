using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    [SerializeField] public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //If game is already paused it will resume game
    //If game is not already paused it will pause the game
    public void checkIfPaused()
    {
        if (gameIsPaused)
        {
            Resume();
        }
        else if (gameIsPaused == false)
        {
            Pause();
        }
    }

    //Disables the pauseMenuUI, resets the timescale, and enables mouselook
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1;
        MouseLook mouse = gameObject.GetComponentInParent(typeof(MouseLook)) as MouseLook;
        mouse.enabled = true;
    }

    //Activates the pauseMenuUI and sets the time scale to 0 so everything stops
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Debug.Log("resume");
    }

    public bool getGameIsPaused()
    {
        return gameIsPaused;
    }

    //Leaves current scene and goes to main menu
    public void ReturnToMainMenu()
    {
        
    }
}
