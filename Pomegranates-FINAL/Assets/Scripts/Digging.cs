using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Digging : MonoBehaviour
{

    public int veggiesCollected;
    public GameObject gameManager;
    public GameObject book;

    public GameObject flyerCanvas;
    public GameObject reticleCanvas;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) //when left mouse button pressed
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,5)) //shoot ray
            {
                if (hit.collider.CompareTag("Mound")) //if it mound
                {
                    hit.collider.gameObject.GetComponent<Mound>().SpawnArtifact(); //spawn artifact
                    Debug.Log("D I G");
                }

                if (hit.collider.CompareTag("FarmMound")) //if one of the farm mounds
                {
                    hit.collider.gameObject.GetComponent<Mound>().RaiseRadish();
                }

                if(hit.collider.CompareTag("Vegetable")) //if hit veggie
                {
                    Destroy(hit.collider.gameObject); //destroy gameobject
                    veggiesCollected++; //add to veggies collected
                }

                if (hit.collider.CompareTag("Book")) //if hit book
                {
                    Destroy(hit.collider.gameObject);
                    book.SetActive(true);
                    //book.GetComponent<AutoFlip>().Invoke("FlipRightPage", 2);
                    Invoke("TurnBookOn", 3);
                    player.SetActive(false);
                    reticleCanvas.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                
                if (hit.collider.CompareTag("Memory") && gameManager.GetComponent<GameManagerPast>().location.Count > 0)
                {
                    Destroy(hit.collider.gameObject);
                    gameManager.GetComponent<GameManagerPast>().SpawnPaper();
                    gameManager.GetComponent<GameManagerPast>().MemoryManager();
                    gameManager.GetComponent<GameManagerPast>().ChangeFlyer();
                    gameManager.GetComponent<GameManagerPast>().ArtifactJournal();
                    gameManager.GetComponent<GameManagerPast>().buttonsPressed++;
                    gameManager.GetComponent<GameManagerPast>().ChangeScene();
                }
                else if (hit.collider.CompareTag("Memory") && gameManager.GetComponent<GameManagerPast>().location.Count == 0)
                {
                    Destroy(hit.collider.gameObject);
                    gameManager.GetComponent<GameManagerPast>().ShowCanvas(
                        "It's time to go back to where you came from and save everyone. Bury the book in the town center");
                    gameManager.GetComponent<GameManagerPast>().buttonsPressed++;
                }

                if (hit.collider.CompareTag("Tree"))
                {
                    hit.collider.gameObject.GetComponent<Renderer>().material = null;
                    gameManager.GetComponent<GameManager>().ShowCanvas(
                        "As you bury the book full of memories of the past, you see as people and buildings comeback \n The End");
                }

                if (hit.collider.CompareTag("Flyer"))
                {
                    Destroy(hit.collider.gameObject);
                    flyerCanvas.SetActive(true);
                    gameManager.GetComponent<Narrative>().flyerOn = true;
                    player.SetActive(false);
                    reticleCanvas.SetActive(false);

                }
                
            }
        }

     
    }

    void TurnBookOn()
    {
        gameManager.GetComponent<Narrative>().bookOn = true;
    }
    
}
