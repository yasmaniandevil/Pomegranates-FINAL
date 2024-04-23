using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject player;
    public GameObject canvas;
    public GameObject reticle;

    public static bool paused;

    public int buttonsPressed;

    public TextMeshProUGUI canvasTextBox;

    public GameObject PauseMenuCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            //PauseAudio();
            //set active pause menu ui
            //lock camera
            //unlock cursor
        }

    }


    public void TurnPlayerOn() //function turns player movement off, reticle off, and cnavas with text on
    {
        player.SetActive(true);
        canvas.SetActive(false);
        reticle.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCanvas(string text) //does opposite and calls text from digging script
    {
        canvas.SetActive(true);
        player.SetActive(false);
        reticle.SetActive(false);
        canvasTextBox.text = text;
        buttonsPressed++; // adds to button pressed
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void PauseGame()
    {
        paused = !paused;
        Time.timeScale = 1.0f - Time.timeScale;
       

        // pause or unpause the music
        if (paused)
        {
            print("Game Paused");
            AkSoundEngine.Suspend();
            PauseMenuCanvas.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            player.SetActive(false);
            reticle.SetActive(false);
            canvas.SetActive(false);




        }
        else
        {
            print("Game Resumed");
            AkSoundEngine.WakeupFromSuspend();
            PauseMenuCanvas.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.SetActive(true);
            reticle.SetActive(true);
            canvas.SetActive(true);

            //I want the cursor to go away when you unpause idk if this is how it is just in editor or build too, got to make test build
            //and the first person camera got to be turned off
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

   

}
