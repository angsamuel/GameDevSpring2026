using UnityEngine;

public class DealDamageZone : MonoBehaviour
{
    Damage damage;
    public bool debugOneDamage = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(debugOneDamage){
            damage = new Damage(1);
        }
    }

    public void SetDamage(Damage damage)
    {
        this.damage = damage;
    }

    public void EndDamage()
    {
        damage = null;
    }

    void OnTriggerEnter(Collider other)
    {
        if(damage == null)
        {
            return;
        }

        TakeDamageZone takeDamageZone = other.GetComponent<TakeDamageZone>();
        if(takeDamageZone == null)
        {
            return;
        }

        takeDamageZone.TakeDamage(this.damage);

    }
}
