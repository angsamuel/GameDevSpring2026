using UnityEngine;

public class Bolt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Damage damage;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("CanDestroy"))
        {
            return;
        }
        Destroy(other.gameObject);
    }
}
