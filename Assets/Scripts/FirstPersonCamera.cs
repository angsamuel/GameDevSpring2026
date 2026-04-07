using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    float xRotation = 0;
    float yRotation = 0;

    public float xSpeed = 10f;
    public float ySpeed = 10f;

    public float maxLookUpAngle = 85f;
    public float maxLookDownAngle = -85;

    public Transform cameraTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void AdjustRotation(float xDelta, float yDelta)
    {
        xDelta *= xSpeed * Time.deltaTime;
        yDelta *= ySpeed * Time.deltaTime;

        yRotation += xDelta;
        xRotation -= yDelta;

        xRotation = Mathf.Clamp(xRotation, maxLookDownAngle, maxLookUpAngle );

        cameraTransform.localRotation = Quaternion.Euler(xRotation,yRotation,0);

    }
}
