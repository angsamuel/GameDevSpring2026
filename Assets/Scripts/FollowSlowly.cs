using UnityEngine;

public class FollowSlowly : MonoBehaviour
{
    public float speed = 10;
    public Transform target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;
        direction *= speed;
        direction *= Time.deltaTime;

        transform.position += direction;

    }
}
