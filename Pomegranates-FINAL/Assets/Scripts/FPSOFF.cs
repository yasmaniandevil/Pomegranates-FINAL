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
        if (reticle == null) //if there is nothing on reticle
        {
            reticle = GameObject.Find("Reticle"); //it will find these game objects
            book = GameObject.Find("BookPro");
            FPS = GameObject.Find("PlayerCapsule");
            TurnPlayerOn();
        }
        //turn off FPS
        if(Input.GetKeyUp(KeyCode.J) && bookOn == false) //when pressing f and bookon is false, it will open the book
        {
            FPS.SetActive(false);
            reticle.SetActive(false);
            bookCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            bookOn = true;
            book.GetComponent<AutoFlip>().enabled = true; //it will enable auto flip
            if (currentPage == 0) //if page is the cover it will flip once
            {
                book.GetComponent<AutoFlip>().Invoke("FlipRightPage", 1f);
            }
            else if (currentPage > 0) //if its another page, it will go to that page
            {
                book.GetComponent<AutoFlip>().StartFlipping(currentPage);
            }

            book.GetComponent<BookPro>().interactable = true;
            return;
        }

        //turn on FPS
        if(Input.GetKeyUp(KeyCode.J) && bookOn == true) //if book is open
        {
            currentPage = book.GetComponent<BookPro>().currentPaper;
            book.GetComponent<AutoFlip>().enabled = true; 
            book.GetComponent<AutoFlip>().StartFlipping(0); //flip to the cover page
            Invoke("TurnPlayerOn", currentPage/2); //turn player on
        }

    }

    void TurnPlayerOn()
    {
        FPS.SetActive(true);
        reticle.SetActive(true);
        bookCanvas.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        bookOn = false;
        book.GetComponent<BookPro>().interactable = true;
    }
}
