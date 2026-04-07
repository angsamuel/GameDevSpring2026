using UnityEngine;
using System.Collections.Generic;

public class CreatureAIManager : MonoBehaviour
{

    public static CreatureAIManager singleton;

    void Awake()
    {
        if(singleton != null)
        {
            Debug.LogError("More than one manager in scene D:");
            Destroy(this.gameObject);
        }
        singleton = this;
    }

    public List<Creature> creatures;

    public void AddCreatureToManager(Creature c)
    {
        creatures.Add(c);
    }

}
