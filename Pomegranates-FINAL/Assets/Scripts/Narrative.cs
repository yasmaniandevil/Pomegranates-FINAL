using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Narrative : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;
    
    public GameObject narrativeCanvas;
    public GameObject reticle;
    public GameObject temp;

    public GameObject father;
    public GameObject fatherMove;
    int timesTalkedToFather = 0;

    public GameObject paperDrop;

    public Sprite[] dialogue;

    private Sprite currentDialogue;

    public GameObject dialogueBox;

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

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
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
                temp.SetActive(true);
            }
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
    }

    void PaperDrop()
    {
        paperDrop.SetActive(true);
    }

    void DeleteFather()
    {
        Destroy(father);
    }
    
}
