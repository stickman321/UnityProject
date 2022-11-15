using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject[] objectPrefabs;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy ()
    {
        Vector3 spawnLocation = new Vector3(Random.Range(-17, 17), Random.Range(-4, 4), 0);
        
        Instantiate(objectPrefabs[0], spawnLocation, objectPrefabs[0].transform.rotation);
    }
}
