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

    public GameObject father;
    public GameObject fatherMove;
    public GameObject tree;
    int timesTalkedToFather = 0;

    public GameObject paperDrop;

    public Sprite[] dialogue;

    private Sprite currentDialogue;

    public GameObject dialogueBox;
    
    public GameObject flyer;
    public GameObject book;
    public  GameObject particles;

    public DissolveEffect[] dissolveObjects;

    public GameObject leaves;

    private bool flyerOn;
    
    

    // Start is called before the first frame update
    void Start()
    {
       
        Invoke("BeginningText", 7); //calls beginning text
        Invoke("ChangeCamera", 8); //changes camera

        dissolveObjects = FindObjectsOfType<DissolveEffect>(); //finds all the game objects with the dissolve effect and puts them in an array
        foreach(DissolveEffect obj in dissolveObjects)
        {
            Debug.Log(obj.gameObject.name); //shows all thw game objects in the inspector
        }
        
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.Find("PlayerCapsule");
        }
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
            if (!book.activeSelf)
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
                    //dissolve dad
                    foreach (DissolveEffect obj in dissolveObjects) //people and buildings will dissolve here
                    {
                        obj.gameObject.GetComponent<DissolveEffect>().startDissolveTrigger();
                    }
                    father.tag = "Untagged";
                    //turn off people talking around here 
                    leaves.GetComponent<LeavesShrink>().TransparentLeaves();
                    Invoke("PaperDrop", 15); //call this function
                    currentDialogue = null;
                } 
            }
            
            
        }

        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     foreach (DissolveEffect obj in dissolveObjects) //people and buildings will dissolve here
        //     {
        //         obj.gameObject.GetComponent<DissolveEffect>().startDissolveTrigger();
        //     }
        //     Invoke("PaperDrop", 15); //call this function
        //     leaves.GetComponent<LeavesShrink>().LeafMaterialShrink();
        //     currentDialogue = null;
        // }
        
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
        else if (amount >= 2 && player.GetComponent<Digging>().veggiesCollected < 3) //if its more than 2 but not all veggies are collected
        {
            ActivateTextBox(3); //activate the 3rd one
        }
        else if (amount >= 2 && player.GetComponent<Digging>().veggiesCollected >= 3) //if all veggies collected
        {
            ActivateTextBox(4); //activate 4th
        }
       
    }
    

    private void ActivateTextBox(int dialogueSprite)
    {
        dialogueBox.gameObject.GetComponent<Image>().sprite =dialogue[dialogueSprite]; //changes dialogue box to whatever the next dialogue is
        narrativeCanvas.SetActive(true);
        player.SetActive(false);
        currentDialogue = dialogue[dialogueSprite];
        Debug.Log(currentDialogue);
    }

    private void DeactivateTextBox() 
    {
        narrativeCanvas.SetActive(false);
        flyer.SetActive(false);
        player.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        
        AkSoundEngine.PostEvent("Event_Teleport2Past", gameObject);
        Debug.Log("TeleportSound");

        //RTCP for fading out all audio
        Invoke("SpawnPaper", 4);
        //sound for papers dropping should go here. quick teleport sound
    }
    

    void SpawnPaper()//will spawn the floating flyer for the player
    {
        flyer.SetActive(true); //turn on canvas
        player.SetActive(false);
        tree.tag = "Tree Future 1";
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void ChangeScene() //changes scene
    {
        SceneManager.LoadScene("Past Final");
    }

    public void PlayerOn() //will turn off all the canvases and activate the player
    {
        book.SetActive(false);
        book.SetActive(false);
        flyer.SetActive(false);
        player.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EndScene() //will turn off all canvases + turn on particles and change scene in 4 seconds
    {
        PlayerOn();
        //teleport to past should go here
        particles.SetActive(true);
        Invoke("ChangeScene", 4);
    }
}
