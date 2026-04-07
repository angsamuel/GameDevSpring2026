using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnPrefab;
    GameObject currentSpawn;
    public float spawnTime = 5;
    float spawnTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpawn != null)
        {
            spawnTimer = 0;
        }
        spawnTimer+=Time.deltaTime;
        if(spawnTimer >= spawnTime)
        {
            spawnTimer = 0;
            Spawn();
        }
    }

    public void Spawn()
    {
        currentSpawn = Instantiate(spawnPrefab, transform.position, Quaternion.identity);
    }
}
