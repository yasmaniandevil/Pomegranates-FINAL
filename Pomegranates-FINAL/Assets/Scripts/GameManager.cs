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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("hit R");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void ChangeScene() //changes scene
    {
        if (buttonsPressed == 2) //if the button has been pressed
        {
            SceneManager.LoadScene("Past Final");
        }
        
    }
}
