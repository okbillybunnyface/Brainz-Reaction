using System;
using UnityEngine;
using System.Collections;

public class SpawnpointScript : MonoBehaviour {

    public GameObject[] spawnedPrefabs;
    public float minDistance = 10f;
    public int numSpawns = 100;

    void Awake()
    {
        Vector2[] spawnpoints = PoissonDisc.Bridsons(transform.position, minDistance, numSpawns, 10);
        System.Random rng = new System.Random();
        foreach (Vector2 point in spawnpoints)
        {
            GameObject spawnee = (GameObject)GameObject.Instantiate(spawnedPrefabs[rng.Next(spawnedPrefabs.Length)]);
            spawnee.transform.position = point;
        }
    }
}
