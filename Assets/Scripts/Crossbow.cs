using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Crossbow : MonoBehaviour
{
    float maxInacc = 45;

    public float boltSpeed = 10;
    public float cooldown = 0.25f;
    public float accuracy = 1; //0 - 1.0 as percentage
    public float projectileSize = 0.05f;



    public int projectileCount = 1;
    public GameObject boltPrefab;
    public Transform spawnTransform;



    bool coolingDown = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [Header("Generation")]
    public bool generateOnStart = false;
    public bool randomSeedOnStart = false;
    public int seed = 0;

    void Start()
    {
        if (randomSeedOnStart)
        {
            seed = Random.Range(int.MinValue, int.MaxValue);
        }
        if (generateOnStart)
        {
            Generate(seed);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Generate(int seed)
    {
        Random.InitState(seed);

        List<float> statDistribution = new List<float>();

        for(int i = 0; i<5; i++)
        {
            statDistribution.Add(Random.Range(0f,1f));
        }


        boltSpeed = Mathf.Lerp(1,30, statDistribution[0]);// Random.Range(1, 30);
        cooldown = Mathf.Lerp(.1f, 2f, statDistribution[1]);
        accuracy = statDistribution[2];
        projectileSize = Mathf.Lerp(.01f, .3f, statDistribution[3]);
        if (Random.Range(0f,1f) < 0.25f)
        {
            projectileCount = (int)Mathf.Lerp(2f, 20f, statDistribution[4]);
        }
        else
        {
            projectileCount = 1;
        }
    }

    public void Shoot()
    {
        if (coolingDown)
        {
            return;
        }
        coolingDown = true;
        StartCoroutine(CooldownRoutine());
            for(int i = 0; i<projectileCount; i++){
            GameObject newBolt = Instantiate(boltPrefab, spawnTransform.position, transform.rotation);

            float tempMaxInacc = maxInacc * (1-accuracy);

            float finalAimAngle = Random.Range(-tempMaxInacc, tempMaxInacc);
            newBolt.transform.RotateAround(newBolt.transform.position, new Vector3(0,1,0), finalAimAngle);
            //newBolt.transform.rotation = Quaternion.LookRotation(Quaternion.Euler(0,45f,0) * newBolt.transform.forward);
            newBolt.GetComponent<Rigidbody>().linearVelocity = newBolt.transform.forward * boltSpeed;

            newBolt.transform.localScale = new Vector3(projectileSize, projectileSize, projectileSize*10);

            Destroy(newBolt, 10);
        }
    }

    IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(cooldown);
        coolingDown = false;
    }
}
