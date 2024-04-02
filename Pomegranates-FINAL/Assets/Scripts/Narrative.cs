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
    

    // Start is called before the first frame update
    void Start()
    {
        BeginningText();
        Invoke("ChangeCamera", 4);
    }

    private void Update()
    {
        int timesTalkedToFather = 0;
        if (Input.GetMouseButtonDown(0)) //when left mouse button pressed
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 5)) //shoot ray
            {
                if (hit.collider.CompareTag("Father")) //if hit father
                { 
                    timesTalkedToFather++;
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
            ActivateTextBox();
            Invoke("FatherContinued", 4);
            Invoke("DeactivateTextBox",8);
            Invoke("MoveFather", 8);
        }
        else if (amount == 2)
        {
            
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
    
}
