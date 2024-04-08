using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject player;
    public GameObject canvas;
    public GameObject reticle;

    public int buttonsPressed;

    public TextMeshProUGUI canvasTextBox;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //when game starts, cursor wont be visible and locked in the center
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //when R pressed, restart game
        {
            Debug.Log("hit R");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }


    public void TurnPlayerOn() //function turns player movement on, reticle on, and cnavas off
    {
        player.SetActive(true);
        canvas.SetActive(false);
        reticle.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCanvas(string text) //turns player off, turns on canvasand calls text from digging script
    {
        canvas.SetActive(true);
        player.SetActive(false);
        reticle.SetActive(false);
        canvasTextBox.text = text;
        buttonsPressed++; // adds to button pressed
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeScene() //changes scene
    {
        if (buttonsPressed == 2) //if the button has been pressed
        {
            SceneManager.LoadScene("Past Final");
        }
        
    }
}
