using UnityEngine;
using UnityEngine.InputSystem;
public class Creature : MonoBehaviour
{
    [Header("Social")]
    public bool isPlayer = false;
    public string team = "";
    [Header("Stats")]
    public float speed = 10;
    public float maxHealth = 20;
    public float currentHealth;

    float rotateSpeed = 10;

    [Header("Smoke")]
    public GameObject smokeBombPrefab;
    public int maxSmokeBombs = 10;
    public int currentSmokeBombs = 0;

    [Header("Crossbow")]
    public Crossbow crossbow;
    public int maxBolts = 10;
    public int currentBolts  = 0;


    [Header("Gravity")]
    public Transform groundCheck;
    public LayerMask groundMask;
    public float jumpPower = 10f;
    Vector3 gravityVector;
    public float gravityAccel;
    Rigidbody rb;
    CharacterController cc;

    [Header("Audio")]
    AudioSource audioSource;
    public AudioClip jumpClip;

    //Animations
    AnimationStateChanger animationStateChanger;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        animationStateChanger = GetComponent<AnimationStateChanger>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentSmokeBombs = maxSmokeBombs;
        currentHealth = maxHealth;
        currentBolts = maxBolts;

        gravityVector = new Vector3(0,-2,0);

        CreatureAIManager.singleton.AddCreatureToManager(this);

        //GetComponent<Transform>().position += new Vector3(0,10,0);
        //transform.position += new Vector3(0, 10, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //60fps
        //transform.position += new Vector3(speed,0,0) * Time.deltaTime;
        SimulateGravity();
    }

    public bool OnGround()
    {
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position,0.25f,groundMask);
        if(colliders.Length > 0)
        {
            return true;
        }
        return false;
    }

    public void SimulateGravity(){
        //if I'm on the ground
        //reset my gravityVector
        if (OnGround() && gravityVector.y <= 0)
        {
            gravityVector = new Vector3(0,-2,0);
        }

        gravityVector.y += gravityAccel * Time.deltaTime;
        cc.Move(gravityVector * Time.deltaTime);
    }

    public void Move(Vector3 direction)
    {
        if(direction == Vector3.zero)
        {
            //rb.linearVelocity = Vector3.zero;
            animationStateChanger.ChangeAnimationState("Idle",.25f);
            return;
        }

        animationStateChanger.ChangeAnimationState("Walk",0.05f, speed/5);
        direction = direction.normalized;

        cc.Move(direction*speed*Time.deltaTime);

        Vector3 flatDirection = new Vector3(direction.x, 0f, direction.z);
        if (flatDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(flatDirection),
                rotateSpeed * Time.deltaTime
            );
        }
    }

    public void AimCreature(Vector3 pos)
    {
        Vector3 direction = pos - transform.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateSpeed * Time.deltaTime);
    }



    public void Stop()
    {
        Move(Vector3.zero);
    }

    public void MoveTowards(Vector3 destination)
    {
        Vector3 moveVector = destination - transform.position;
        Move(moveVector);
    }

    public void Jump()
    {
        if (!OnGround())
        {
            return;
        }

        audioSource.PlayOneShot(jumpClip);
        gravityVector = new Vector3(0,jumpPower,0);
    }

    public void RotateCreatureForCamera(Transform cameraTransform)
    {
        transform.rotation = cameraTransform.rotation;
    }

    public void ThrowSmokeBomb()
    {
        if(currentSmokeBombs < 1)
        {
            currentSmokeBombs = 0;
            return;
        }
        currentSmokeBombs--;
        GameObject newSmoke = Instantiate(smokeBombPrefab, transform.position, Quaternion.identity);
    }

    public void ShootCrossbow()
    {
        if(currentBolts <= 0)
        {
            return;
        }
        currentBolts -= 1;
        crossbow.Shoot();
    }

    public void Teleport(Vector3 position)
    {
        cc.enabled = false;
        transform.position = position;
        cc.enabled = true;
    }

    public bool GiveSmokeBombs(int amount)
    {
        if(currentSmokeBombs >= maxSmokeBombs)
        {
            return false;
        }
        currentSmokeBombs += amount;
        if(currentSmokeBombs > maxSmokeBombs)
        {
            currentSmokeBombs = maxSmokeBombs;
        }
        return true;
    }

    public void TakeDamage(Damage damage){
        Debug.Log("Taking damage on creature!");
        currentHealth -= damage.Amount;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }


    void Die()
    {
        Debug.Log("I Died! D:");
        Destroy(this.gameObject);
    }
}
