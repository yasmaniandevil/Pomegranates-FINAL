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

    public bool flyerOn;
    public bool bookOn;
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("BeginningText", 2);
        Invoke("ChangeCamera", 3);
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
                   timesTalkedToFather++; 
                   Debug.Log(timesTalkedToFather);
                  FatherConvo(timesTalkedToFather);
                   
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeactivateTextBox();
            if(currentDialogue == dialogue[1])
            {
                ActivateTextBox(2);
                return;
            }

            if (currentDialogue == dialogue[2])
            {
                MoveFather();
            }

            if (currentDialogue == dialogue[4])
            {
                DeleteFather();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                PaperDrop();
                currentDialogue = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && flyerOn == true )
        {
            flyer.SetActive(false);
            flyerOn = false;
            player.SetActive(true);
            reticle.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && bookOn == true )
        {
            book.SetActive(false);
            particles.SetActive(true);
            player.SetActive(true);
            reticle.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Invoke("ChangeScene", 4);
        }
    }

    void ChangeCamera()
    { 
        camera.SetActive(false);
    }

    private void BeginningText()
    {
        ActivateTextBox(0);
    }

    private void FatherConvo(int amount)
    {
        if (amount == 1)
        {
            ActivateTextBox(1);
            father.tag = "Untagged";
        }
        else if (amount >= 2 && player.GetComponent<Digging>().veggiesCollected < 4)
        {
            ActivateTextBox(3);
        }
        else if (amount >= 2 && player.GetComponent<Digging>().veggiesCollected == 4)
        {
            ActivateTextBox(4);
        }
       
    }
    

    private void ActivateTextBox(int dialogueSprite)
    {
        dialogueBox.gameObject.GetComponent<Image>().sprite =dialogue[dialogueSprite];
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
    
    private void MoveFather()
    {
        father.transform.position = fatherMove.transform.position;
        father.transform.rotation = fatherMove.transform.rotation;
        father.tag = "Father";
    }

    void PaperDrop()
    {
        paperDrop.SetActive(true);
        Invoke("SpawnPaper", 4);
    }

    void DeleteFather()
    {
        Destroy(father);
    }

    void SpawnPaper()
    {
        Vector3 paperPos = new Vector3(paperSpawn.transform.position.x, paperSpawn.transform.position.y,
            paperSpawn.transform.position.z);
        Instantiate(paperPrefab, paperPos, transform.rotation);
    }
    
    public void ChangeScene() //changes scene
    {
        SceneManager.LoadScene("Past Final");
    }
    
    
}
