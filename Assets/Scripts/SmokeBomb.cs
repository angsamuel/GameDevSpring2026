using UnityEngine;
using System.Collections;


public class SmokeBomb : MonoBehaviour
{

    public float lifeTime = 3; //how long to shrink to nothing
    bool shrinking = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Shrink();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void Shrink(){
        if (shrinking)
        {
            return;
        }
        shrinking = true;
        StartCoroutine(ShrinkRoutine());
        IEnumerator ShrinkRoutine()
        {
            float timer = lifeTime;
            Vector3 startSize = transform.localScale;
            while(timer > 0)
            {
                timer -= Time.deltaTime;
                transform.localScale = startSize * (timer/lifeTime);
                yield return null;
            }
            yield return null;
            Destroy(this.gameObject);
        }
    }
}
