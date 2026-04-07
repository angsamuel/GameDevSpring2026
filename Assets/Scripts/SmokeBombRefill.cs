using UnityEngine;

public class SmokeBombRefill : MonoBehaviour
{

    public int numToGive = 100;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Pickup!");
        if(other.GetComponent<Creature>() == null)
        {
            return;
        }
        Creature creatureToGiveTo = other.GetComponent<Creature>();

        if (creatureToGiveTo.GiveSmokeBombs(numToGive))
        {
            Destroy(this.gameObject);
        }
    }
}
