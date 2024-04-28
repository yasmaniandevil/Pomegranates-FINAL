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
                    Debug.Log("bookoff");
                    book.SetActive(true);
                    Debug.Log("book on");
                    Debug.Log(book.GetComponent<AutoFlip>());
                    book.GetComponent<AutoFlip>().Invoke("FlipRightPage", 2);
                    Debug.Log("page flip");
                    Invoke("TurnBookOn", 3);
                    player.SetActive(false);
                    reticleCanvas.SetActive(false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                
                if (hit.collider.CompareTag("Memory"))
                {
                    var artifact = hit.collider.gameObject.GetComponent<Artifact>();
                    script.ArtifactJournal(artifact.memoryText, artifact.artifact);
                    Destroy(hit.collider.gameObject);
                    script.ChangeFlyer();
                    script.MemoryManager();
                    script.ChangeFlyer();
                    script.buttonsPressed++;
                    
                }

                if (hit.collider.CompareTag("Tree Future 1"))
                {
                    hit.collider.gameObject.GetComponentInChildren<Mound>().SpawnArtifact();
                }
                
                if (hit.collider.CompareTag("Tree Future 2"))
                {
                    hit.collider.gameObject.GetComponentInChildren<Mound>().SpawnArtifact();
                    gameManager.GetComponent<GameManager>().Invoke("CameraOn", 4f);
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
