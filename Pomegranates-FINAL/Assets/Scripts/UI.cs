using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject reticle;
    
    public Sprite open;
    public Sprite closed; 

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

        if (Physics.Raycast(ray, out hit, 5)) //shoot ray
        {
            if (hit.collider.CompareTag("Mound")) //if it mound
            {
                reticle.GetComponent<Image>().sprite = open;
            }

            if (hit.collider.CompareTag("Vegetable")) //if hit veggie
            {
                reticle.GetComponent<Image>().sprite = open;
            }

            if (hit.collider.CompareTag("Book")) //if hit book
            {
                reticle.GetComponent<Image>().sprite = open;
            }
            
            if (hit.collider.CompareTag("Memory"))
            {
                reticle.GetComponent<Image>().sprite = open;
            }

            if (hit.collider.CompareTag("Tree"))
            {
                reticle.GetComponent<Image>().sprite = open;
            }

            if (hit.collider.CompareTag("Father"))
            {
                reticle.GetComponent<Image>().sprite = open;
            }
            if (hit.collider.CompareTag("Flyer")) 
            {
                reticle.GetComponent<Image>().sprite = open;
            }

        }
        else
        {
            reticle.GetComponent<Image>().sprite = closed;
        }
        
        
        
    }
    
}
