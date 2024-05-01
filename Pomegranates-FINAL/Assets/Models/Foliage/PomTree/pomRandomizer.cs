using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pomRandomizer : MonoBehaviour
{
    public Texture2D[] pomTexs;
    // Start is called before the first frame update
    void Start()
    {
        var meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = new Material(meshRenderer.material);
        meshRenderer.material.mainTexture = pomTexs[(int)Random.Range(0, pomTexs.Length)];   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
