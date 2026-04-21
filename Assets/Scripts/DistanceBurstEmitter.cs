using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class DistanceBurstEmitter : MonoBehaviour
{

    ParticleSystem ps;

    [Header("Emission Settings")]
    public float stepDistance = 1;
    public int particlesPerStep = 3;
    public float particleSpeed = 0.5f;
    public int numParticles = 3;
    Vector3 lastPos;
    float distanceAccum;


    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        lastPos = transform.position;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float moved = Vector3.Distance(transform.position, lastPos);
        distanceAccum+=moved;
        if(distanceAccum >= stepDistance)
        {
            //spawn some particles
            EmitParticles(transform.position);
            distanceAccum -= stepDistance;
        }
        lastPos = transform.position;
    }

    void EmitParticles(Vector3 position)
    {
        for(int i = 0; i<numParticles; i++)
        {
            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();

            emitParams.position = position;
            emitParams.velocity = Random.insideUnitSphere * particleSpeed;
            emitParams.startSize = Random.Range(0.8f, 1.2f);
            emitParams.startLifetime = Random.Range(0.8f, 1.2f);

            ps.Emit(emitParams, 1);
        }

    }

}