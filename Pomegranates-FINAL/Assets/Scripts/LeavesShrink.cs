using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesShrink : MonoBehaviour
{
    public float speed;
    public float delay;

    private Material transparency;
    
    // Start is called before the first frame update
    void Start()
    {
        transparency = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShrinkLeaves()
    {
        StartCoroutine(LeafShrink());
    }

    private IEnumerator LeafShrink()
    {
        Vector3 leafsize = gameObject.transform.localScale; //makes vector3 to store xyz data
        Vector3 target = new Vector3(100f, 100f, 100f); //makes the target size
        while (gameObject.transform.localScale.x < target.x) //if leaf size is less than target size
        {
            Vector3 currentSize = leafsize; //store current leaf size
            gameObject.transform.localScale += currentSize * speed; //add speed to current size

            yield return new WaitForSeconds(speed /delay); //do again in speed divided by 100
        }
    }

    private IEnumerator LeafMaterialShrink()
    {
        while (transparency.color.a < 255)
        {
            float currentTransparency = transparency.color.a;
            var color = transparency.color;
            color.a = currentTransparency;
            transparency.color += color;
            
            yield return new WaitForSeconds(speed /delay); //do again in speed divided by 100
        }
    }
}
