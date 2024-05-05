using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject player;
    public GameObject playerRoot;
    public GameObject canvas;
    public GameObject reticle;
    public GameObject particles;

    public static bool paused;

    public GameObject PauseMenuCanvas;

    public GameObject birdEyeViewCam;
    public GameObject treeCam;
    
    public GameObject UI;
    
    public DissolveEffect[] dissolveObjects;

    public GameObject leaves;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Invoke("CameraOff", 3f);
        
        dissolveObjects = FindObjectsOfType<DissolveEffect>(); //finds all the game objects with the dissolve effect and puts them in an array
        foreach(DissolveEffect obj in dissolveObjects)
        {
            Debug.Log(obj.gameObject.name); //shows all thw game objects in the inspector
        }
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
            particles.SetActive(false);
        }

        if (PauseMenuCanvas == null)
        {
            PauseMenuCanvas = GameObject.Find("PauseMenuCanvas");

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

    }


    public void TurnPlayerOn() //function turns player movement off, reticle off, and cnavas with text on
    {
        player.SetActive(true);
        canvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    public void CameraOff()
    {
        birdEyeViewCam.SetActive(false);
    }

    public void TreeCamOn()
    {
        treeCam.SetActive(true);
        player.SetActive(false);
        Invoke("EndCameraOn", 4f);
    }

    public void EndCameraOn()
    {
        foreach (DissolveEffect obj in dissolveObjects) //people and buildings will dissolve here
        {
            obj.gameObject.GetComponent<DissolveEffect>().startRessolveTrigger();
        }
        birdEyeViewCam.SetActive(true);
    }
   

}
