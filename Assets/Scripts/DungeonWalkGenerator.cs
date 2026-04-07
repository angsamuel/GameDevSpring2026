using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class DungeonWalkGenerator : MonoBehaviour
{
    public int seed;
    public GameObject roomPrefab;
    public int targetRooms = 100;
    public int maxSteps = 1000; //how many steps I am allowed to take'
    int roomsPlaced = 0;
    public float stepSize = 10;

    public float stepPause = 0.25f;

    public List<GameObject> dungeonDecorations;

    Dictionary<Vector3, DungeonRoom> visitDict;

    void Awake()
    {
        visitDict = new Dictionary<Vector3, DungeonRoom>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateDungeonRoutine(seed);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateDungeonRoutine(int seed){
        Random.InitState(seed);

        List<Vector3> directions = new List<Vector3>();
        directions.Add(new Vector3(0, 0, 1));
        directions.Add(new Vector3(0, 0, -1));
        directions.Add(new Vector3(1, 0, 0));
        directions.Add(new Vector3(-1, 0, 0));

        for (int i =0; i<maxSteps; i++){
            //place a room
            int directionIndex = Random.Range(0, directions.Count);
            int backDirectionIndex = 0;
            switch (directionIndex)
            {
                case 0:
                    backDirectionIndex = 1;
                    break;
                case 1:
                    backDirectionIndex = 0;
                    break;
                case 2:
                    backDirectionIndex = 3;
                    break;
                case 3:
                    backDirectionIndex = 2;
                    break;
            }

            if (i != 0)
            {
                visitDict[transform.position].RemoveWall(directionIndex);
            }

            transform.position += directions[directionIndex] * stepSize;
            if (visitDict.ContainsKey(transform.position))
            {
                continue;
            }
            DungeonRoom newRoom = Instantiate(roomPrefab,transform.position,Quaternion.identity).GetComponent<DungeonRoom>();

            PlaceDecorations(newRoom);

            if(i != 0)
            {
                newRoom.RemoveWall(backDirectionIndex);
            }

            roomsPlaced++;
            visitDict[transform.position] = newRoom;
            if (roomsPlaced >= targetRooms)
            {
                 break;
            }
            //yield return new WaitForSeconds(stepPause);
        }
    }


    void PlaceDecorations(DungeonRoom room)
    {
        int placementsCount = Random.Range(0,3);
        for(int i = 0; i<placementsCount; i++)
        {
            int spawnPointIndex = Random.Range(0, room.spawnPoints.Count);
            Instantiate(
                dungeonDecorations[Random.Range(0,dungeonDecorations.Count)],
                room.transform.position + room.spawnPoints[spawnPointIndex],
                Quaternion.identity
                );
            room.spawnPoints.RemoveAt(spawnPointIndex);
        }
    }
}
