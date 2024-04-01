using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSOFF : MonoBehaviour
{

    public Canvas bookCanvas;
    public GameObject FPS;

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
            bookCanvas.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            bookOn = true;
            return;
        }

        //turn on FPS
        if(Input.GetKeyUp(KeyCode.J) && bookOn == true)
        {
            FPS.SetActive(true);
            bookCanvas.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            bookOn = false;
        }
    }
}
