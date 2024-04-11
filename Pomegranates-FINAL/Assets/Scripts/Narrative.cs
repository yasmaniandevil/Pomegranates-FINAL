using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Narrative : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;

    public TextMeshProUGUI narrativeTextBox;
    public GameObject narrativeCanvas;

    public GameObject father;
    int timesTalkedToFather = 0;

    public GameObject paperDrop;

    public GameObject dialogue1;
    public GameObject dialogue2;
    public GameObject dialogue3;

    // Start is called before the first frame update
    void Start()
    {
        BeginningText();
        Invoke("ChangeCamera", 4);
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
    }

    void ChangeCamera()
    { 
        camera.SetActive(false);
    }

    private void BeginningText()
    {
        ChangeText("Child, come here!");
        Invoke("ActivateTextBox", 2);
        Invoke("DeactivateTextBox", 5);
    }

    private void FatherConvo(int amount)
    {
        if (amount == 1)
        {
            player.SetActive(false);
            ChangeText("These radishes to our left are ready to be picked.");
            //disable change text set active dialogue1 button
            //i wired the buttons in inspector so when the player hits space ideally the text is supposed to continue
           
            ActivateTextBox();
            Invoke("FatherContinued", 4);
            Invoke("DeactivateTextBox",8);
            Invoke("MoveFather", 8);
        }
        else if (amount >= 2 && player.GetComponent<Digging>().veggiesCollected < 4)
        {
            player.SetActive(false);
            ChangeText("You need to get all the radishes!");
            ActivateTextBox();
            Invoke("DeactivateTextBox", 5);
        }
        else if (amount >= 2 && player.GetComponent<Digging>().veggiesCollected == 4)
        {
            player.SetActive(false);
            ChangeText("Hurry, we must sell these before...");
            ActivateTextBox();
            Invoke("DeactivateTextBox", 5);
            Invoke("DeleteFather", 5);
            Invoke("PaperDrop",5);
        }
       
    }

    void FatherContinued()
    {
        ChangeText("Dig them up and meet me by the tree at the souq");
    }

    private void ActivateTextBox()
    {
        narrativeCanvas.gameObject.SetActive(true);
        player.SetActive(false);
    }

    private void DeactivateTextBox()
    {
        player.SetActive(true);
        narrativeCanvas.gameObject.SetActive(false);
    }

    private void ChangeText(string text)
    {
        narrativeTextBox.text = text;
    }

    private void MoveFather()
    {
        father.transform.position = new Vector3(-25, 7, 1);
        father.transform.Rotate(0,-90,0);
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
