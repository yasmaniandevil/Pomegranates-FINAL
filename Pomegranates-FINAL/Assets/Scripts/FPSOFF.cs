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
        if (Instance == null)
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
        if (reticle == null)
        {
            reticle = GameObject.Find("Reticle");
            book = GameObject.Find("BookPro");
            FPS = GameObject.Find("PlayerCapsule");
            TurnPlayerOn();
        }
        //turn off FPS
        if(Input.GetKeyUp(KeyCode.J) && bookOn == false)
        {
            FPS.SetActive(false);
            reticle.SetActive(false);
            bookCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            bookOn = true;
            book.GetComponent<AutoFlip>().enabled = true;
            if (currentPage == 0)
            {
                book.GetComponent<AutoFlip>().Invoke("FlipRightPage", 1f);
            }
            else if (currentPage > 0)
            {
                book.GetComponent<AutoFlip>().StartFlipping(currentPage);
            }

            book.GetComponent<BookPro>().interactable = true;
            return;
        }

        //turn on FPS
        if(Input.GetKeyUp(KeyCode.J) && bookOn == true)
        {
            currentPage = book.GetComponent<BookPro>().currentPaper;
            book.GetComponent<AutoFlip>().enabled = true; 
            book.GetComponent<AutoFlip>().StartFlipping(0);
            Invoke("TurnPlayerOn", currentPage/2);
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
