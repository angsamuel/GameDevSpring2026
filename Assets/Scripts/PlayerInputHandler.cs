
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class PlayerInputHandler : MonoBehaviour
{

    public Creature playerCreature;
    public FirstPersonCamera playerCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame


    void Update()
    {
        Vector3 direction = new Vector3(0,0,0);
        if (Keyboard.current.wKey.isPressed)
        {

            direction.z += 1;
        }
        if (Keyboard.current.sKey.isPressed)
        {
            direction.z -= 1;
        }
        if (Keyboard.current.aKey.isPressed)
        {
            direction.x -= 1;
        }
        if (Keyboard.current.dKey.isPressed)
        {
            direction.x += 1;
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            playerCreature.ThrowSmokeBomb();
        }

        if (Mouse.current.leftButton.isPressed)
        {
            playerCreature.ShootCrossbow();
        }


        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            //restart the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //failure state
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            playerCreature.Jump();
        }

        direction = playerCamera.cameraTransform.TransformDirection(direction);

        direction.y = 0;
        playerCreature.Move(direction);

        //cameraControl
        if(playerCamera.enabled){
            playerCamera.AdjustRotation(Mouse.current.delta.x.value, Mouse.current.delta.y.value);
            playerCreature.RotateCreatureForCamera(playerCamera.cameraTransform);
        }


    }
}
