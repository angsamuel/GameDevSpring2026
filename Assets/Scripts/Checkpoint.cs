using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public string checkpointID;
    public GameObject playerCreature;
    public Renderer lightRenderer;
    public Color lightColor;
    public Color darkColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightRenderer.material = new Material(lightRenderer.material);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other){
        Creature creature = other.GetComponent<Creature>();
        if ( creature == null)
        {
            return;
        }
        if (!creature.isPlayer)
        {
            return;
        }

        SaveCheckpoint();
    }

    void SaveCheckpoint()
    {
        CheckpointManager.singleton.ResetLastCheckpointLight();
        CheckpointManager.singleton.StoreLastCheckpoint(this);
        Debug.Log("Saving Checkpoint");
        lightRenderer.material.SetColor("_BaseColor", lightColor);
        PlayerPrefs.SetString("checkpoint", checkpointID);
    }

    public void ResetCheckpointLight()
    {
        lightRenderer.material.SetColor("_BaseColor", darkColor);
    }
}
