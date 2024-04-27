using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mound : MonoBehaviour
{
    public GameObject artifact;
    public GameObject bookSpawner;
    private bool moundDug;
    private Vector3 moundPos;
    
    // Start is called before the first frame update
    void Start()
    {
        moundPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y,
            transform.position.z); //make a vector 3 from mound position
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnArtifact()
    {
        if (gameObject.CompareTag("Tree Future 1") || gameObject.CompareTag("Tree Future 2"))
        {
            moundPos = new Vector3(bookSpawner.transform.position.x, bookSpawner.transform.position.y,
                bookSpawner.transform.position.z);
        }
        if (moundDug == false) //if it hasnt been clicked
        {
            Instantiate(artifact, moundPos, transform.rotation); //instantiate artifact from mound position
            moundDug = true; //turn to true to not be clicked again
        }
    }

    public void RaiseRadish()
    {
        Invoke("ChangeTag", 1f);
        gameObject.AddComponent<Artifact>();
    }

    void ChangeTag()
    {
        gameObject.tag = "Vegetable";
    }
}
