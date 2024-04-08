using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManagerPast : MonoBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public GameObject reticle;

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

    public TextMeshProUGUI[] bookTextBox;

    //public AudioSource journalWriting;
    
    
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
        
        MemoryManager();
        ChangeFlyer();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) //restarts game
        {
            Debug.Log("hit R");
            //MemoryManager();
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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


    public void TurnPlayerOn() //function turns player movement on, reticle on, and cnavas off
    {
        player.SetActive(true);
        canvas.SetActive(false);
        reticle.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentLocation.SetActive(true);
    }

    public void ShowCanvas(string text) //turns player off, turns on canvasand calls text from digging script
    {
        canvas.SetActive(true);
        player.SetActive(false);
        reticle.SetActive(false);
        flyerTextBox.text = text;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeScene() //changes scene
    {
        if (buttonsPressed == 3) //if the button has been pressed
        {
            SceneManager.LoadScene("Future End");
        }
        
    }

    public void MemoryManager() 
    {
        //convert dictionary keys to a list //makes a list out of locations
        List<string> keysList = new List<string>(location.Keys);
        
        // Select a random Key
        string randomKey = keysList[Random.Range(0, keysList.Count)];
        
        //randomvalue will equal the location in the array that was randomly picked
        GameObject randomValue = location[randomKey];

        currentLocationString = randomKey; //string will be the random key
        currentLocation = randomValue; //gameobject will be the value
        
        location.Remove(randomKey);
        
        Debug.Log(location.Count +"," +  currentLocation +"," +  currentLocationString);
    }

    public void ArtifactJournal() //memory text will change
    {
        for (int i = 0; i < bookTextBox.Length; i++) //will go down the text boxes to add them in order
        {
            if (bookTextBox[i].text == "New Text")
            {
                bookTextBox[i].text = "Memory Collected";
                return;
            }
            else
            {
                continue;
            }
        }
    }

    public void ChangeFlyer()//chnage flyer text with current location
    {
        currentLocation.SetActive(true);
        flyerTextBox.text = "Next memory at " + currentLocationString;
        //journalWriting.Play();
    }

 
}
