using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSwitch : MonoBehaviour
{
    [Header("Objs")]
    public GameObject AliveTree;
    public GameObject DeadTree;
    public GameObject DeadBranch;
    Collider DeadColl;
    Collider AliveColl;
    Rigidbody DeadRigidbody;

    [Header("Restore Vars")]
    public float restTime = 1f;
    public float restSpeed = 1f;
    public float switchThreshold = .25f;

    // Start is called before the first frame update
    void Start()
    {
        DeadColl = DeadTree.GetComponent<Collider>();
        AliveColl = AliveTree.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

        if(DeadBranch.transform.localScale.magnitude <= switchThreshold)
        {
            DeadBranch.SetActive(false);
            DeadTree.SetActive(false);
            AliveTree.SetActive(true);
        }
    }

    public void KillTree()
    {

    }

    public void StartRestore(float restDelay)
    {
        StartCoroutine(TriggerRestore(restDelay));
    }

    public void RestoreTree(float time, float maxSpeed)
    {
        Vector3 currentVel = Vector3.zero;
        DeadBranch.transform.localScale = Vector3.SmoothDamp(DeadBranch.transform.localScale, Vector3.zero, ref currentVel, time, maxSpeed);
    }

    public void ResetBranch()
    {
        DeadBranch.transform.localPosition = Vector3.zero;
        DeadBranch.transform.localRotation = Quaternion.identity;
        DeadBranch.transform.localScale = Vector3.zero;
    }

    IEnumerator TriggerRestore(float time)
    {
        yield return new WaitForSeconds(time);
        RestoreTree(restTime, restSpeed);
    }
}
