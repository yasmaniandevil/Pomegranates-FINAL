using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesShrink : MonoBehaviour
{
    public float speed;
    public float delay;

    private Material transparency;

    public GameObject[] Pomegranates;
    
    // Start is called before the first frame update
    void Start()
    {
        transparency = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnshrinkLeaves()
    {
        gameObject.SetActive(true);
        StartCoroutine(LeafShrink());
    }

    public void TransparentLeaves()
    {
        StartCoroutine(LeafMaterialShrink());
    }

    private IEnumerator LeafShrink()
    {
        Debug.Log("gworing");
        Vector3 pomegranateSize = default;
        Vector3 leafsize = gameObject.transform.localScale; //makes vector3 to store xyz data
        foreach (GameObject pom in Pomegranates)
        {
            pomegranateSize = pom.transform.localScale;
        }
        Vector3 target = new Vector3(100f, 100f, 100f); //makes the target size
        
        while (gameObject.transform.localScale.y < target.y) //if leaf size is less than target size
        {
            Debug.Log("gworing2");
            speed = Random.Range(1, 5);
            Vector3 currentSize = leafsize; //store current leaf size
            var scale = gameObject.transform.localScale;
            scale.y += currentSize.y * speed;
            gameObject.transform.localScale = scale; //add speed to current size
            
            yield return new WaitForSeconds(speed /delay); //do again in speed divided by 100
        }

        while (Pomegranates[0].transform.localScale.y < 36)
        {
            Debug.Log("gworing3");
            foreach (var pom in Pomegranates)
            {
                Vector3 currentPomSize = pomegranateSize; //store current Pom size
                var pomScale = pom.transform.localScale;
                pomScale.y += currentPomSize.y * speed;
                pomScale.x += currentPomSize.x * speed;
                pomScale.z += currentPomSize.z * speed;
                pom.transform.localScale = pomScale; //add speed to current size
            }
            
            yield return new WaitForSeconds(speed /delay);

        }
    }

    public IEnumerator LeafMaterialShrink()
    {
        float targetAlpha = 0f; // Target alpha value (fully transparent)
        float currentAlpha = transparency.color.a; // Current alpha value

// Calculate the time needed to reach the target alpha
        float totalTime = (currentAlpha - targetAlpha) / speed;

// Interpolate alpha value gradually over time
        float elapsedTime = 0f;
        while (elapsedTime < totalTime)
        {
            // Calculate the new alpha value based on elapsed time
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, elapsedTime / totalTime);
    
            // Assign the new alpha value to the color
            transparency.color = new Color(transparency.color.r, transparency.color.g, transparency.color.b, newAlpha);
    
            // Increment elapsed time
            elapsedTime += Time.deltaTime;
    
            // Wait for the next frame
            yield return null;
        }

// Ensure the final alpha value is set to the target alpha
        transparency.color = new Color(transparency.color.r, transparency.color.g, transparency.color.b, targetAlpha);
    }
}
