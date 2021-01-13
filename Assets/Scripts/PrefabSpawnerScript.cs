using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawnerScript : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<GameObject> spawnLocations;
    public float minTime = 0.2F;
    public float maxTime = 3F;
    float timeRemaining = 0;
    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = Random.Range(minTime, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        } else
        {
            timeRemaining = Random.Range(minTime, maxTime);
            Instantiate(prefabs[Random.Range(1, prefabs.Count - 1)], 
                spawnLocations[Random.Range(1, spawnLocations.Count - 1)].transform.position, 
                Quaternion.identity);
        }
    }
}
