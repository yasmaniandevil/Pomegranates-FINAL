using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeCamera", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeCamera()
    {
        camera.SetActive(false);
    }
}
