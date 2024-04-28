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
    public GameObject reticleCanvas;
    public GameObject player;
    public GameObject bookSpawner;

    private GameManagerPast script;

    /*string SFX_Soundbank;
    AK.Wwise.Event Event_DigSound;
    AK.Wwise.Bank SFXSoundbank;*/

    private void Start()
    {
        script = gameManager.GetComponent<GameManagerPast>();
        
    }

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
             
                    //AkSoundEngine.LoadBank("SFX_Soundbank");
                    //AkSoundEngine.PostEvent("Event_DigSound", gameObject);
                    //Debug.Log(Event_DigSound + "sound");
                    hit.collider.gameObject.GetComponent<Mound>().SpawnArtifact(); //spawn artifact
                    Debug.Log("D I G");
                }

                if (hit.collider.CompareTag("FarmMound")) //if one of the farm mounds
                {
                    //digging sound
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
                    Debug.Log(book.GetComponent<AutoFlip>());
                    book.GetComponent<AutoFlip>().Invoke("FlipRightPage", 2);
                    player.SetActive(false);
                    reticleCanvas.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                
                if (hit.collider.CompareTag("Memory")) //if memory
                {
                    var artifact = hit.collider.gameObject.GetComponent<Artifact>(); //create a variable that called artifact component
                    script.ArtifactJournal(artifact.memoryText, artifact.artifact); //grab the memory and artifact from component
                    Destroy(hit.collider.gameObject); //destroy the object
                    script.ChangeFlyer(); 
                    script.MemoryManager(); //change location
                    script.ChangeFlyer();//change flyer to next riddle
                    script.buttonsPressed++; //add to buttons pressed
                    
                }

                if (hit.collider.CompareTag("Tree Future 1")) //if its the tree in the first scene
                {
                    hit.collider.gameObject.GetComponentInChildren<Mound>().SpawnArtifact(); //spawn artifact
                }
                
                if (hit.collider.CompareTag("Tree Future 2")) //if its the second scene
                {
                    hit.collider.gameObject.GetComponentInChildren<Mound>().SpawnArtifact(); //spawn artifatc
                    gameManager.GetComponent<GameManager>().Invoke("CameraOn", 4f); //change camera in 4 seconds
                }

                if (hit.collider.CompareTag("Flyer")) //if hit flyer
                {
                    Destroy(hit.collider.gameObject); //destory 
                    flyerCanvas.SetActive(true); //turn on canvas
                    player.SetActive(false);
                    reticleCanvas.SetActive(false);

                }
                
            }
        }

     
    }

   
    
}
