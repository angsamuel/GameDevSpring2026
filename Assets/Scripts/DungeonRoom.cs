using UnityEngine;
//using System.Collections;
using System.Collections.Generic;
public class DungeonRoom : MonoBehaviour
{
    public List<GameObject> walls;
    public List<Vector3> spawnPoints;

    public void RemoveWall(int i)
    {
        walls[i].SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
