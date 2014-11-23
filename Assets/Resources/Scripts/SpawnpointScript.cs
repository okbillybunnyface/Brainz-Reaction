using System;
using UnityEngine;
using System.Collections;

public class SpawnpointScript : MonoBehaviour {

    public GameObject[] spawnedPrefabs;
    public float minDistance = 10f;
    public int numSpawns = 100;



    void Start()
    {
        Camera mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector2[] spawnpoints = PoissonDisc.Bridsons(transform.position, minDistance, numSpawns, 10,
            mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect - 1f, mainCamera.transform.position.x - mainCamera.orthographicSize * mainCamera.aspect + 1f,
            mainCamera.transform.position.y + mainCamera.orthographicSize - 1f, mainCamera.transform.position.y - mainCamera.orthographicSize + 1f);
        System.Random rng = new System.Random();
        foreach (Vector2 point in spawnpoints)
        {
            GameObject spawnee = (GameObject)GameObject.Instantiate(spawnedPrefabs[rng.Next(spawnedPrefabs.Length)]);
            spawnee.transform.position = point;
        }
    }
}
