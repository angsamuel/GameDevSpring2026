using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeAI : MonoBehaviour
{
    public Creature myCreature;
    public Creature targetCreature;
    public float patrolRange = 20;
    public GameObject markerPrefab;
    public float sightRange = 10;
    public float chaseRange = 12;
    public float attackRange = 6f;
    IEnumerator currentState;


    public LayerMask sightMask;

    void Start()
    {
        ChangeState(PatrolStateRoutine());
    }

    void Update()
    {
    }

    void FindBestOp()
    {
        float minDist = 1000000;
        List<Creature> creatureList = CreatureAIManager.singleton.creatures;
        Creature selectedCreature = null;
        for (int i = 0; i < CreatureAIManager.singleton.creatures.Count; i++)
        {
            if(creatureList[i] == null)
            {
                continue;
            }
            if(myCreature == creatureList[i])
            {
                continue;
            }
            if(myCreature.team == creatureList[i].team)
            {
                continue;
            }
            float tempDistance = Vector3.Distance(transform.position, creatureList[i].transform.position);
            if (tempDistance > minDist)
            {
                continue;
            }
            if(tempDistance > sightRange)
            {
                continue;
            }
            if (!CanSeePosition(creatureList[i].transform.position))
            {
                continue;
            }
            minDist = tempDistance;
            selectedCreature = creatureList[i];
        }

        if(selectedCreature != null)
        {
            targetCreature = selectedCreature;
        }
        //look through the ai manager creature list
        //find the nearest creature we can see
        //set our targetCreature to that

    }

    void ChangeState(IEnumerator newState)
    {
        if(currentState != null)
        {
            StopCoroutine(currentState);
        }
        currentState = newState;
        StartCoroutine(currentState);
    }

    IEnumerator AttackStateRoutine()
    {
        yield return null;
        myCreature.Stop();

        bool attackIsValid = true;

        while (attackIsValid)
        {
            if (targetCreature == null)
            {
                ChangeState(PatrolStateRoutine());
                yield break;
            }
            myCreature.AimCreature(targetCreature.transform.position);
            myCreature.ShootCrossbow();

            attackIsValid = Vector3.Distance(transform.position, targetCreature.transform.position) < attackRange + 2;
            attackIsValid = attackIsValid && CanSeePosition(targetCreature.transform.position);
            yield return null;
        }
        yield return null;
        ChangeState(ChaseStateRoutine());
        yield break;

        yield return null;
    }

    IEnumerator ChaseStateRoutine()
    {

        yield return null;
        while (true)
        {

            if(targetCreature == null)
            {
                ChangeState(PatrolStateRoutine());
                yield break;
            }

            if(Vector3.Distance(transform.position, targetCreature.transform.position) > chaseRange)
            {
                ChangeState(PatrolStateRoutine());
                yield break;
            }

            if (!CanSeePosition(targetCreature.transform.position))
            {
                ChangeState(PatrolStateRoutine());
                yield break;
            }

            if (Vector3.Distance(transform.position, targetCreature.transform.position) < attackRange)
            {
                ChangeState(AttackStateRoutine());
                yield break;
            }

            myCreature.MoveTowards(targetCreature.transform.position);

            yield return null;
        }
        yield return null;
    }

    bool ShouldStartAttacking()
    {
        if(targetCreature == null)
        {
            return false;
        }
        if (Vector3.Distance(transform.position, targetCreature.transform.position) < sightRange)
        {
            if (CanSeePosition(targetCreature.transform.position))
            {
                return true;
            }
        }
        return false;
    }

    IEnumerator PatrolStateRoutine()
    {
        Debug.Log("PATROL STATE");
        yield return null;


        Vector3 homePosition = transform.position;
        while (true)
        {
            Vector3 randomPosition = homePosition + new Vector3(Random.Range(-patrolRange, patrolRange),0, Random.Range(-patrolRange, patrolRange));
            //Instantiate(markerPrefab, randomPosition, Quaternion.identity);

            while (Vector3.Distance(transform.position, randomPosition) > 1)
            {
                myCreature.MoveTowards(randomPosition);

                FindBestOp();
                if(ShouldStartAttacking()){
                    ChangeState(ChaseStateRoutine());
                    yield break;
                }

                yield return null;
            }

            myCreature.Stop();


            float timer = 0;
            while(timer < 2)
            {
                FindBestOp();
                timer +=Time.deltaTime;
                if (ShouldStartAttacking())
                {
                    ChangeState(ChaseStateRoutine());
                    yield break;
                }
                yield return null;
            }
        }

        yield return null;
    }

    public bool CanSeePosition(Vector3 targetPosition)
    {
        if(Physics.Linecast(transform.position, targetPosition, sightMask))
        {
            return false;
        }
        Debug.Log("CAN SEE CREATURE");
        return true;
    }
}
