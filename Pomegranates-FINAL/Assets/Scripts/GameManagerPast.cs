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
        
        Debug.Log(location.Count);
        
        MemoryManager();
        ChangeFlyer();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
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


    public void TurnPlayerOn() //function turns player movement off, reticle off, and cnavas with text on
    {
        player.SetActive(true);
        canvas.SetActive(false);
        reticle.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        currentLocation.SetActive(true);
    }

    public void ShowCanvas(string text) //does opposite and calls text from digging script
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
        //convert dictionary keys to a list
        List<string> keysList = new List<string>(location.Keys);
        
        // Select a random Key
        string randomKey = keysList[Random.Range(0, keysList.Count)];

        GameObject randomValue = location[randomKey];

        currentLocationString = randomKey;
        currentLocation = randomValue;
        
        location.Remove(randomKey);
        
        Debug.Log(location.Count +"," +  currentLocation +"," +  currentLocationString);
    }

    public void ArtifactJournal()
    {
        for (int i = 0; i < bookTextBox.Length; i++)
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

    public void ChangeFlyer()
    {
        currentLocation.SetActive(true);
        flyerTextBox.text = "Next memory at " + currentLocationString;
        //journalWriting.Play();
    }

    public void SpawnPaper()
    {
        currentLocation.SetActive(true);
    }
}
