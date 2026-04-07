using UnityEngine;

public class Damage
{
    float _amount = 0;
    public float Amount
    {
        get
        {
            return _amount;
        }
        set
        {
            _amount = Mathf.Max(value,0);
        }
    }

    public Damage(int amount)
    {
        _amount = amount;
    }
}
