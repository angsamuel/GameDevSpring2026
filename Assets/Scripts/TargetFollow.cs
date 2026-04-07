using UnityEngine;

public class TargetFollow : MonoBehaviour
{

    public Transform followTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        transform.position = followTransform.position;
    }
}
