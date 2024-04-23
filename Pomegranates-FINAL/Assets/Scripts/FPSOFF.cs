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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //turn off FPS
        if(Input.GetKeyUp(KeyCode.J) && bookOn == false)
        {
            FPS.SetActive(false);
            reticle.SetActive(false);
            bookCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            bookOn = true;
            Debug.Log(currentPage);
            Debug.Log(book.GetComponent<BookPro>().currentPaper);
            if (currentPage == book.GetComponent<BookPro>().currentPaper)
            {
                book.GetComponent<AutoFlip>().Invoke("FlipRightPage", 1f);
            }
            else if (currentPage > book.GetComponent<BookPro>().currentPaper)
            {
                book.GetComponent<AutoFlip>().StartFlipping(currentPage);
            }
           
            Debug.Log("flip");
            return;
        }

        //turn on FPS
        if(Input.GetKeyUp(KeyCode.J) && bookOn == true)
        {
            Debug.Log(book.GetComponent<BookPro>().currentPaper);
            currentPage = book.GetComponent<BookPro>().currentPaper;
            Debug.Log(book.GetComponent<BookPro>().currentPaper);
            book.GetComponent<AutoFlip>().StartFlipping(0);
            Debug.Log(book.GetComponent<BookPro>().currentPaper);
            Invoke("TurnPlayerOn", currentPage);
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
