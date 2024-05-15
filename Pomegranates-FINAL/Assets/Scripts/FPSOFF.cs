using System;
using System.Collections;
using System.Collections.Generic;
using BookCurlPro;
using UnityEngine;

public class FPSOFF : MonoBehaviour
{

    public Canvas bookCanvas;
    public GameObject FPS;
    public GameObject book;
    public GameObject reticle;

    private int currentPage;
    private bool bookOn = false;

    public static FPSOFF Instance;

    public int firstStart;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) //make ssingleton
        {
            Instance = this;
            DontDestroyOnLoad(bookCanvas);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    


    // Update is called once per frame
    void Update()
    {
        if (FPS == null) //if there is nothing on reticle
        {
            reticle = GameObject.Find("Reticle"); //it will find these game objects
            FPS = GameObject.Find("PlayerCapsule");
            TurnPlayerOn();
        }

        if (book == null)
        {
            book = GameObject.Find("BookPro");
        }
        
        //turn off FPS
        if (book.activeSelf)
        {
            bookOn = true;
        }
        else
        {
            bookOn = false;
        }
        
        if(Input.GetKeyUp(KeyCode.J) && bookOn == false) //when pressing f and bookon is false, it will open the book
        {
            FPS.SetActive(false);
            reticle.SetActive(false);
            book.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            book.GetComponent<AutoFlip>().enabled = true; //it will enable auto flip
            book.GetComponent<AutoFlip>().Invoke("FlipRightPage", 1f);
            book.GetComponent<BookPro>().interactable = true;
            return;
        }

        //turn on FPS
        if(Input.GetKeyUp(KeyCode.J) && bookOn == true) //if book is open
        {
            currentPage = book.GetComponent<BookPro>().currentPaper;
            book.GetComponent<AutoFlip>().enabled = true; 
            book.GetComponent<AutoFlip>().StartFlipping(0); //flip to the cover page
            Invoke("TurnPlayerOn", currentPage); //turn player on
        }

    }

    void TurnPlayerOn()
    {
        FPS.SetActive(true);
        reticle.SetActive(true);
        book.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        bookOn = false;
        book.GetComponent<BookPro>().interactable = true;
    }
}
