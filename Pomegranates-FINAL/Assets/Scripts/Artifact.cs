using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    private bool spawn;

    private Vector3 finalPosition;
    
    public float speed = 1f; // Adjust the speed as needed
    private float startTime;
    public float amplitude = 0.1f;
    public float offset = 0.1f;

    public Sprite rightPage;
    public Sprite leftPage;
    
    void Start()
    {
        spawn = true;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update() //this is just swaying up and down
    {
        if (spawn == true)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
                
            finalPosition = transform.position;
            Invoke(nameof(ChangeToFalse), 1f);
        }
        else 
        {
            float t = (Time.time - startTime) * speed;
            float y = EaseInSine(t);

            // Adjust the amplitude and offset as needed
            
            transform.position = new Vector3(finalPosition.x, finalPosition.y + amplitude * y + offset, finalPosition.z);

        }
        
            

    }

    private float EaseInSine(float x)
    {
        return 1 - Mathf.Cos((x * Mathf.PI) / 2);
    }
    void ChangeToFalse()
    {
        spawn = false;
    }
}
