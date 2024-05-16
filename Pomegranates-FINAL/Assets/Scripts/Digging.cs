using System;
using System.Collections;
using System.Collections.Generic;
using BookCurlPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Digging : MonoBehaviour
{

    public int veggiesCollected;
    public GameObject gameManager;
    public GameObject book;
    //public BookCurlPro.AutoFlip autoFlip;
    public GameObject flyerCanvas;
    public GameObject player;

    private int bookPage = 1;
    private GameManagerPast script;

    

    private void Start()
    {
        script = gameManager.GetComponent<GameManagerPast>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager");
            book = GameObject.Find("BookPro");
            script = gameManager.GetComponent<GameManagerPast>();
        }
        if (Input.GetMouseButtonDown(0)) //when left mouse button pressed
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit,5)) //shoot ray
            {
                if (hit.collider.CompareTag("Mound")) //if it mound
                {
                    AkSoundEngine.PostEvent("Event_ArtifactUp2", gameObject);
                    Debug.Log("Artifact dig sound");
             
                    hit.collider.gameObject.GetComponent<Mound>().SpawnArtifact(); //spawn artifact
                    script.currentLocation = hit.collider.gameObject;
                    hit.collider.gameObject.tag = "Untagged";
                    Debug.Log("D I G");
                }

                if (hit.collider.CompareTag("FarmMound")) //if one of the farm mounds
                {
                   
                    Debug.Log("dig sound");
                    hit.collider.gameObject.GetComponent<Mound>().RaiseRadish();
                    
                }

                if(hit.collider.CompareTag("Vegetable")) //if hit veggie
                {
                    AkSoundEngine.PostEvent("Event_DigSound", gameObject);
                    Destroy(hit.collider.gameObject); //destroy gameobject
                    veggiesCollected++; //add to veggies collected
                }

                if (hit.collider.CompareTag("Book")) //if hit book
                {
                    Destroy(hit.collider.gameObject);
                    book.SetActive(true);
                    //sound of book being placed
                    Debug.Log(book.GetComponent<AutoFlip>());
                    book.GetComponent<AutoFlip>().Invoke("FlipRightPage", .3f);

                    //AkSoundEngine.PostEvent("Event_PageFlip", gameObject);
                    //Debug.Log("EventPlayed");

                    player.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                
                if (hit.collider.CompareTag("Memory")) //if memory
                {
                    var artifact = hit.collider.gameObject.GetComponent<Artifact>(); //create a variable that called artifact component
                    script.ArtifactJournal(artifact.rightPage, artifact.leftPage); //grab the memory and artifact from component
                    Destroy(hit.collider.gameObject); //destroy the object
                    script.book.SetActive(true);
                    Debug.Log(bookPage);
                    bookPage += 1;
                    Debug.Log(bookPage);
                    script.book.GetComponent<AutoFlip>().enabled = true; //it will enable auto flip
                    script.book.GetComponent<BookPro>().interactable = true;
                    script.book.GetComponent<AutoFlip>().StartFlipping(bookPage);
                    script.PlayerOff();
                    script.buttonPressed++; //add to buttons pressed
                    script.buttonPressed++; //add to buttons pressed
                    script.ChangeFlyer();//change flyer to next riddle
                    
                    
                }

                if (hit.collider.CompareTag("Tree Future 1")) //if its the tree in the first scene
                {
                    AkSoundEngine.PostEvent("Event_ArtifactUp2", gameObject);
                    hit.collider.gameObject.GetComponentInChildren<Mound>().SpawnArtifact(); //spawn artifact
                    gameManager.GetComponent<Narrative>().tree.tag = "Untagged";
                }
                
                if (hit.collider.CompareTag("Tree Future 2")) //if its the second scene
                {
                    AkSoundEngine.PostEvent("Event_ArtifactUp2", gameObject);
                    hit.collider.gameObject.GetComponentInChildren<Mound>().SpawnArtifact();
                    gameManager.GetComponent<GameManager>().leaves.GetComponent<LeavesShrink>().Invoke("UnshrinkLeaves", 2f);
                    gameManager.GetComponent<GameManager>().Invoke("TreeCamOn", 1f); //change camera in 1 second
                }

                if (hit.collider.CompareTag("Flyer")) //if hit flyer
                {
                    Destroy(hit.collider.gameObject); //destory 
                    flyerCanvas.SetActive(true); //turn on canvas
                    player.SetActive(false);

                }
                
            }
        }

     
    }

   
    
}
