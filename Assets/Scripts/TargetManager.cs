using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject target;
    public float zSpawn = 50f;
    private List<GameObject> activeTargets = new List<GameObject>();
    public Transform playerTransform;
    public float distanceBetweenTargets = 200f;
    public int numberOfTargets = 5;
    public static GameObject go;

    private void Start()
    {
        zSpawn += distanceBetweenTargets;
        for (int i = 0; i<numberOfTargets; i++)
        {
            SpawnTarget();
            SetTag();
        }
    }
    private void Update()
    {
        if (playerTransform.position.z - distanceBetweenTargets > zSpawn - (numberOfTargets * distanceBetweenTargets))
        {
            SpawnTarget();
            SetTag();
            DeleteTarget();
        }
    }
    private Vector3 spawnRotation = new Vector3(0, 90, 90);
    public void SpawnTarget()
    {
        float xSpawn = Random.Range(-40, 40);
        float ySpawn = Random.Range(20, 80);
        go = Instantiate(target, new Vector3(xSpawn,ySpawn,zSpawn), transform.rotation);
        go.transform.Rotate(spawnRotation);
        activeTargets.Add(go);
        zSpawn += distanceBetweenTargets;
    }
    private void SetTag()
    {
        foreach (Transform t in go.transform)
        {
            t.gameObject.tag = "Target";
        }
    }
    private void DeleteTarget()
    {
        Destroy(activeTargets[0]);
        activeTargets.RemoveAt(0);
    }
}
