using UnityEngine;

using System.Collections.Generic;
public class GameManager : MonoBehaviour
{

    public GameObject smokeBombRefillPrefab;

    public List<GameObject> spawnLocations;
    List<GameObject> activeSmokeBombs;

    public float smokeBombSpawnTime = 10;
    float smokeBombSpawnTimer = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeSmokeBombs = new List<GameObject>();
        for(int i = 0; i < spawnLocations.Count; i++)
        {
            activeSmokeBombs.Add(null);
        }
        SpawnSmokeBombs();
    }

    // Update is called once per frame
    void Update()
    {
        smokeBombSpawnTimer += Time.deltaTime;
        if(smokeBombSpawnTimer > smokeBombSpawnTime)
        {
            smokeBombSpawnTimer = 0;
            SpawnSmokeBombs();
        }
    }

    public void SpawnSmokeBombs()
    {
        for(int i = 0; i<spawnLocations.Count; i++)
        {
            if(activeSmokeBombs[i] != null)
            {
                continue;
            }
            activeSmokeBombs[i] = Instantiate(smokeBombRefillPrefab,spawnLocations[i].transform.position,Quaternion.identity);
        }
    }
}
