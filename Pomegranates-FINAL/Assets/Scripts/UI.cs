using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject reticle;

    public GameObject narrativeTextBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 5)) //shoot ray and if its whichever it will chnage to white and in range
        {
            if (hit.collider.CompareTag("Mound")) //if it mound
            {
                reticle.GetComponent<Image>().color = Color.white;
            }

            if (hit.collider.CompareTag("Vegetable")) //if hit veggie
            {
                reticle.GetComponent<Image>().color = Color.white;
            }

            if (hit.collider.CompareTag("Book")) //if hit book
            {
                reticle.GetComponent<Image>().color = Color.white;
            }
            
            if (hit.collider.CompareTag("Memory")) //if it memory
            {
                reticle.GetComponent<Image>().color = Color.white;
            }

            if (hit.collider.CompareTag("Tree")) //if it tree
            {
                reticle.GetComponent<Image>().color = Color.white;
            }

            if (hit.collider.CompareTag("Father")) //if it father
            {
                reticle.GetComponent<Image>().color = Color.white;
            }
        }
        else //if not in range
        {
            reticle.GetComponent<Image>().color = Color.red; //change red
        }
        
        
        
    }
    
}
