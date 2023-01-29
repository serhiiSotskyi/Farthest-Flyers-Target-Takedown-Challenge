using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float zSpawn = 0.0f;
    public float obstacleLength = 200f;
    public int numberOfObstacle = 5;
    private List<GameObject> activeObstacles = new List<GameObject>();
    public Transform playerTransform;

    private void Start()
    {
        for(int i = 0; i < numberOfObstacle; i++)
        {
            if (i == 0)
            {
                SpawnObstacle(0);
            }
            else
            {
                SpawnObstacle(Random.Range(1, obstaclePrefabs.Length));
                SetTag();
            }
        }
    }

    private void Update()
    {
        if (playerTransform.position.z - obstacleLength > zSpawn - (numberOfObstacle * obstacleLength))
        {
            SpawnObstacle(Random.Range(1, obstaclePrefabs.Length));
            SetTag();
            DeleteObstacle();
        }
    }
    private GameObject go;
    public void SpawnObstacle(int indexObstacle)
    {
        go = Instantiate(obstaclePrefabs[indexObstacle], transform.forward * zSpawn, transform.rotation);
        activeObstacles.Add(go);
        zSpawn += obstacleLength;
    }

    private void SetTag()
    {
        foreach (Transform t in go.transform)
        {
            t.gameObject.tag = "Obstacle";
        }
    }

    private void DeleteObstacle()
    {
        Destroy(activeObstacles[0]);
        activeObstacles.RemoveAt(0);
    }
}
