using UnityEngine;
using System.Collections.Generic;
public class CheckpointManager : MonoBehaviour
{

    public static CheckpointManager singleton;
    public List<Checkpoint> checkpoints;
    public GameObject playerCreature;

    Checkpoint lastCheckpoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(singleton != null)
        {
            Debug.LogError("multiple checkpoint managers! D:");
        }
        singleton = this;
    }

    void Start()
    {
        //SaverLoader.SaveString("meepis","scronkle");
        //SaverLoader.SaveFloat("my_float",0.125f);
        //SaverLoader.SaveToFile();
        SaverLoader.LoadFromFile();
        Debug.Log(SaverLoader.LoadString("meepis"));
        Debug.Log(SaverLoader.LoadFloat("my_float"));

        LoadCheckpoint();
    }

    public void LoadCheckpoint()
    {
        string lastCheckpoint = PlayerPrefs.GetString("checkpoint");
        for(int i = 0; i<checkpoints.Count; i++)
        {
            if(checkpoints[i].checkpointID == lastCheckpoint)
            {
                playerCreature.transform.position = checkpoints[i].transform.position + new Vector3(0,0,2);
            }
        }
    }

    public void ResetLastCheckpointLight()
    {
        lastCheckpoint?.ResetCheckpointLight();
    }
    public void StoreLastCheckpoint(Checkpoint checkpoint)
    {
        lastCheckpoint = checkpoint;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
