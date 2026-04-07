using UnityEngine;
using System.Collections.Generic;
public class CrossbowTester : MonoBehaviour
{

    public List<Crossbow> crossbows;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Crossbow c in crossbows)
        {
            c.Shoot();
        }
    }
}
