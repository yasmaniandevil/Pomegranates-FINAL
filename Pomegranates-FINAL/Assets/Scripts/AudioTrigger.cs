using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    AK.Wwise.Event play;
    AK.Wwise.Bank bank;

    //public string audioVar;

    public void Start()
    {

        
           
    }

    public void isPlaying()
    {

        // Output the result to the console
        //Debug.Log("Is the Wwise sound playing? " + isPlaying);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        //AkSoundEngine.PostEvent("PortalEnter", gameObject);
        //AkSoundEngine.PostEvent(audioVar, gameObject);
        Debug.Log("enter");
    }*/

    /*private void OnTriggerExit(Collider other)
    {
        //AkSoundEngine.sto
    }

    private void OnTriggerExit(Collider other)
    {
        AkSoundEngine.PostEvent("PortalExit", gameObject);
        Debug.Log("exit");
    }*/
}
