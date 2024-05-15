using System;
using System.Collections;
using System.Collections.Generic;
using BookCurlPro;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManagerPast : MonoBehaviour
{
    public GameObject player;
    public GameObject playerRoot;
    public GameObject reticle;
    public GameObject UI;
    public GameObject particles;
    public GameObject book;

    public GameObject currentLocation;

    public Dictionary<string,GameObject> location;

    public GameObject mosqueSpawner;
    public GameObject churchSpawner;
    public GameObject hospitalSpawner;

    public GameObject mosqueLight, churchLight, hospitalLight;
    

    public TextMeshProUGUI flyerTextBox;

    [FormerlySerializedAs("bookTextBox")] public Image[] leftPage;
    [FormerlySerializedAs("artifactImage")] public Image[] rightPage;
    
    public GameObject PauseMenuCanvas;
    public static bool paused;

    public GameObject continueButton;

    public int buttonPressed;

    public GameObject year;
    
    public GameObject bookScript;
    

   
   
    
    // Start is called before the first frame update
    void Start()
    {

        

        //randomizes every time the game starts
        Random.InitState(System.Environment.TickCount);
        
        
        
        DontDestroyOnLoad(playerRoot);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("PlayerCapsule");
            playerRoot = GameObject.Find("NestedParent_Unpack");
            particles = GameObject.Find("Teleport Particles");
            reticle = GameObject.Find("Reticle");
            particles.SetActive(false);
            
        }

        if (year.activeSelf)
        {
            Invoke("TurnOffYearCanvas",5f);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        //turns on the light in whichever place you are supposed to look at

        if (buttonPressed == 6 && Input.GetKeyDown(KeyCode.Space))
        {
            EndScene();
        }
        
    }
    
    

    public void ChangeScene() //changes scene
    {
        SceneManager.LoadScene("Future End");
        AkSoundEngine.StopAll();
        
    }



    public void ArtifactJournal(Sprite rightPageSprite, Sprite leftPageSprite)
    {
        for (int i = 0; i < leftPage.Length; i++) //goes through loop
        {
            if (leftPage[i].sprite == null) //goes to the next image to change image
            {
                
                leftPage[i].color = new Color(255f,255f,255f,255f); //makes image non transparent
                leftPage[i].sprite = leftPageSprite; //changes it with the artifact of the 
                break;
            }
            else
            {
                continue;
            }
        }

        for (int i = 0; i < rightPage.Length; i++) //goes to next image to change image
        {
            if (rightPage[i].sprite == null)
            {
                rightPage[i].color = new Color(255f,255f,255f,255f);
                rightPage[i].sprite = rightPageSprite;
                break;
            }
            else
            {
                continue;
            }
        }
    }

    public void ChangeFlyer()
    {
        if(buttonPressed >= 6)
        {
            flyerTextBox.text =
                "Good, you have recovered the memories of the lost. Bring this book back to the future and return it where you found it."; //will say this
            continueButton.SetActive(true);
        }

        if (currentLocation == mosqueSpawner)
        {
            mosqueLight.SetActive(false);
        }
        if (currentLocation == churchSpawner)
        {
            churchLight.SetActive(false);
        }
        if (currentLocation == hospitalSpawner)
        {
            hospitalLight.SetActive(false);
        }
        AkSoundEngine.PostEvent("Event_WritingSFX", gameObject);
        Debug.Log(buttonPressed);
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
            UI.SetActive(false);




        }
        else
        {
            print("Game Resumed");
            AkSoundEngine.WakeupFromSuspend();
            PauseMenuCanvas.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            player.SetActive(true);
            UI.SetActive(true);
            
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
    
    public void EndScene() //will turn off all canvases + turn on particles and change scene in 4 seconds
    {
        PlayerOn();
        AkSoundEngine.PostEvent("Event_Teleport2Past", gameObject);
        particles.SetActive(true);
        Invoke("ChangeScene", 4);
    }
    
    public void PlayerOn() //will turn off all the canvases and activate the player
    {
        book.SetActive(false);
        player.SetActive(true);
        reticle.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void PlayerOff() //will turn off all the canvases and activate the player
    {
        book.SetActive(true);
        player.SetActive(false);
        reticle.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void TurnOffYearCanvas()
    {
        year.SetActive(false);
        PlayerOff();
        book.GetComponent<AutoFlip>().StartFlipping(1);
    }
}
