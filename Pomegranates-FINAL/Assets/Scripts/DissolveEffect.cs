using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveEffect : MonoBehaviour
{
    //cutoff modifiers
    public float speed;
    //noise texture
    public float offset;
    public float scale;
    //how long till its called
    public float minDelay;
    public float maxDelay;
    
    public Material[] myMats;
    
    // Start is called before the first frame update
    void Start()
    {
        //finds all materials in the game object
        myMats = GetComponent<MeshRenderer>().materials; 
        
    }

    // Update is called once per frame
    public void startDissolveTrigger()
    {
        Invoke("startDissolve", Random.Range(minDelay, maxDelay));
    }

    public void startRessolveTrigger()
    {
        Invoke("startRessolve",Random.Range(minDelay, maxDelay));
    }

    public void  startDissolve()
    {
        foreach (Material mat in myMats)
        {
            StartCoroutine(Disappear(mat));
        }
    }
    public void  startRessolve()
    {
        foreach (Material mat in myMats)
        {
            StartCoroutine(Reappear(mat));
        }
    }

    public IEnumerator Disappear(Material myMat)
    {
        Debug.Log("disappear");
        while (myMat.GetFloat("_Cutoff") < 1) // while material's dissolve amount is smaller than one
        {
            
            float currentCutOff = myMat.GetFloat("_Cutoff"); //make a current cutoff float and equal it to the current dissolve amount
            currentCutOff += speed; //add the speed to the current cutoff
            myMat.SetFloat("_Cutoff", currentCutOff); //set the material's dissolve amount as the new current cut off

            yield return new WaitForSeconds(speed / 2); //wait this amount of time to call it again.
        }
        
    }

    public IEnumerator Reappear(Material myMat)
    {
        Debug.Log("reappear");
        while (myMat.GetFloat("_Cutoff") > 0) //same thing but opposite
        {
            float currentCutOff = myMat.GetFloat("_Cutoff");
            currentCutOff -= speed;
            myMat.SetFloat("_Cutoff", currentCutOff);

            yield return new WaitForSeconds(speed / 2);
        }
    }
}