using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperDrop : MonoBehaviour
{
    public GameObject paperPrefab;
    public float spawnInterval = 1.0f;
    public float spawnHeight = 10.0f;
    public float spawnRadius = 5.0f;
    public float totalSpawnTime = 10.0f;

    private float timer;
    private float totalTimeElapsed;
    private bool stopSpawning = false;
    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopSpawning)
        {
            timer -= Time.deltaTime;
            totalTimeElapsed += Time.deltaTime;
        }

        if (timer <= 0f)
        {
           SpawnPaper();
           timer = spawnInterval;

           if (totalTimeElapsed >= totalSpawnTime)
           {
               stopSpawning = true;
           }
        }
    }

    void SpawnPaper()
    {
        Vector3 spawnOffset = Random.insideUnitSphere * spawnRadius;
        spawnOffset.y = spawnHeight;

        Vector3 spawnPosition = playerTransform.position + spawnOffset;
        Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);
        Debug.Log("spawn rotation: " + spawnRotation);

        GameObject paper = Instantiate(paperPrefab, spawnPosition, spawnRotation);
        Debug.Log("paper:" + paper.transform.position);
        Rigidbody paperRB = paper.GetComponent<Rigidbody>();
        paperRB.AddForce(Vector3.down * 10f, ForceMode.Impulse);
    }
}
