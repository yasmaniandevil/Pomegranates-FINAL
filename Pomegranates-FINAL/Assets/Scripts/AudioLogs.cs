using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogs : MonoBehaviour
{
    public string Event;

    private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!hasPlayed && other.CompareTag("Player"))
        {
            AkSoundEngine.PostEvent(Event, gameObject);
            hasPlayed = true;
        }
        
    }
}
