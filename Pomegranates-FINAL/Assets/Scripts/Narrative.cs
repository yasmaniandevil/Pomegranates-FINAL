using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Narrative : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    
    public GameObject narrativeCanvas;
    public GameObject reticle;

    public GameObject father;
    public GameObject fatherMove;
    int timesTalkedToFather = 0;

    public GameObject paperDrop;

    public Sprite[] dialogue;

    private Sprite currentDialogue;

    public GameObject dialogueBox;

    public GameObject paperPrefab;
    public GameObject paperSpawn;
    
    public GameObject flyer;
    public GameObject book;
    public  GameObject particles;
    
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BeginningText", 7);
        Invoke("ChangeCamera", 8);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //when left mouse button pressed
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5)) //shoot ray
            {
                if (hit.collider.CompareTag("Father")) //if hit father
                { 
                    Debug.Log(timesTalkedToFather);
                   timesTalkedToFather++; //add to times talked 
                   Debug.Log(timesTalkedToFather);
                  FatherConvo(timesTalkedToFather); //activate father combo with the amount of times talked
                   
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Space)) //when pressing space
        {
            DeactivateTextBox(); //deactivate box
            if(currentDialogue == dialogue[1]) //if its the first dialogue
            {
                ActivateTextBox(2);//activate with the second box
                return;
            }

            if (currentDialogue == dialogue[2]) //if its the second
            {
                MoveFather(); //move the dad
            }

            if (currentDialogue == dialogue[4]) //if its the 4th
            {
                DeleteFather(); //delete dad
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PaperDrop(); //call this function
                currentDialogue = null;
            }
        }
        
    }

    void ChangeCamera() //turns of first cam to switch perspective
    { 
        camera.SetActive(false);
    }

    private void BeginningText() //puts first text box
    {
        ActivateTextBox(0);
    }

    private void FatherConvo(int amount)
    {
        if (amount == 1) //if its the first convo, then activates first box
        {
            ActivateTextBox(1);
            father.tag = "Untagged";
        }
        else if (amount >= 2 && player.GetComponent<Digging>().veggiesCollected < 7) //if its more than 2 but not all veggies are collected
        {
            ActivateTextBox(3); //activate the 3rd one
        }
        else if (amount >= 2 && player.GetComponent<Digging>().veggiesCollected == 7) //if all veggies collected
        {
            ActivateTextBox(4); //activate 4th
        }
       
    }
    

    private void ActivateTextBox(int dialogueSprite)
    {
        dialogueBox.gameObject.GetComponent<Image>().sprite =dialogue[dialogueSprite]; //changes dialogue box to whatever the next dialogue is
        narrativeCanvas.SetActive(true);
        player.SetActive(false);
        reticle.SetActive(false);
        currentDialogue = dialogue[dialogueSprite];
        Debug.Log(currentDialogue);
    }

    private void DeactivateTextBox() 
    {
        reticle.SetActive(true);
        narrativeCanvas.SetActive(false);
        player.SetActive(true);
    }
    
    private void MoveFather() //moves father
    {
        father.transform.position = fatherMove.transform.position;
        father.transform.rotation = fatherMove.transform.rotation;
        father.tag = "Father";
    }

    void PaperDrop() 
    {
        paperDrop.SetActive(true); //will activate flyer particles
        //sounds of flyers falling and being teleported
        //code for all the people disappearing goes here which includes turning the sound off
        Invoke("SpawnPaper", 4);
    }

    void DeleteFather() //will delete the father
    {
        Destroy(father);
    }

    void SpawnPaper()//will spawn the floating flyer for the player
    {
        Vector3 paperPos = new Vector3(paperSpawn.transform.position.x, paperSpawn.transform.position.y,
            paperSpawn.transform.position.z);
        Instantiate(paperPrefab, paperPos, transform.rotation);
    }
    
    public void ChangeScene() //changes scene
    {
        SceneManager.LoadScene("Past Final");
    }

    public void PlayerOn() //will turn off all the canvases and activate the player
    {
        book.SetActive(false);
        book.SetActive(false);
        flyer.SetActive(true);
        player.SetActive(true);
        reticle.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EndScene() //will turn off all canvases + turn on particles and change scene in 4 seconds
    {
        PlayerOn();
        particles.SetActive(true);
        Invoke("ChangeScene", 4);
    }
}
