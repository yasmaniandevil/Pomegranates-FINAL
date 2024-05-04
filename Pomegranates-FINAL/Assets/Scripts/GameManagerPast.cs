using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject canvas;
    public GameObject reticle;
    public GameObject UI;
    public GameObject particles;
    public GameObject book;

    private string mosque = "mosque";
    private string church = "church";
    private string hospital = "hospital";

    private GameObject currentLocation;
    public string currentLocationString;

    public Dictionary<string,GameObject> location;

    public GameObject mosqueSpawner;
    public GameObject churchSpawner;
    public GameObject hospitalSpawner;

    public GameObject mosqueLight, churchLight, hospitalLight;

    public int buttonsPressed;

    public TextMeshProUGUI flyerTextBox;

    [FormerlySerializedAs("bookTextBox")] public Image[] leftPage;
    [FormerlySerializedAs("artifactImage")] public Image[] rightPage;
    
    public GameObject PauseMenuCanvas;
    public static bool paused;

    public GameObject continueButton;

   
   
    
    // Start is called before the first frame update
    void Start()
    {
        //randomizes every time the game starts
        Random.InitState(System.Environment.TickCount);
        
        //dictionary to connect name and spawner
        location = new Dictionary<string, GameObject>()
        {
            {mosque,mosqueSpawner},
            {church, churchSpawner},
            {hospital, hospitalSpawner}
        };
        
        Debug.Log(location.Count);
        
        MemoryManager(); //start of the game calls memory manager
        ChangeFlyer(); //changes the flyer
        
        //DontDestroyOnLoad(playerRoot);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("PlayerCapsule");
            playerRoot = GameObject.Find("NestedParent_Unpack");
            //particles = GameObject.Find("Teleport Particles");
            //particles.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        //turns on the light in whichever place you are supposed to look at
        if (currentLocation == churchSpawner)
        {
            churchLight.SetActive(true);
        }
        else
        {
            churchLight.SetActive(false);
        }

        if (currentLocation == hospitalSpawner)
        {
            hospitalLight.SetActive(true);
        }
        else
        {
            hospitalLight.SetActive(false);
        }

        if (currentLocation == mosqueSpawner)
        {
            mosqueLight.SetActive(true);
        }
        else
        {
            mosqueLight.SetActive(false);
        }
        
        
    }
    
    

    public void ChangeScene() //changes scene
    {
        
            SceneManager.LoadScene("Future End");
        
    }

    public void MemoryManager()
    {
        List<string> keysList = new List<string>(location.Keys); //make a list
        
        if (keysList.Count == 0)
        {
            
            Debug.Log("All memories collected. Deleting dictionary...");
            continueButton.SetActive(true);
            currentLocation = null; //makes it null to turn off all lights
            ChangeFlyer();
            return; // Exit the function since there's nothing else to do
        }
        
        //convert dictionary keys to a list
        
        
        // Select a random Key
        string randomKey = keysList[Random.Range(0, keysList.Count)];

        GameObject randomValue = location[randomKey];

        currentLocationString = randomKey;
        currentLocation = randomValue;
        
        location.Remove(randomKey);
        
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

        if (currentLocation == null) //if location is null
        {
            flyerTextBox.text =
                "Good, you have recovered the memories of the lost. Bring this book back to the future and return it where you found it."; //will say this
        }
        else
        {
            currentLocation.SetActive(true); //else it will activate the current location
        }
        if (currentLocation == mosqueSpawner)
        {
            flyerTextBox.text = "You will find what you need where the call takes you.";
        }
        if (currentLocation == churchSpawner)
        {
            flyerTextBox.text = "You will find what you need where you wash away your sins and eat the flesh of god.";
        }
        if (currentLocation == hospitalSpawner)
        {
            flyerTextBox.text = "You will find what you need in the place where man heals man, where life begins and ends. ";
        }


        AkSoundEngine.PostEvent("Event_WritingSFX", gameObject);
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
        //teleport to past should go here
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

}
