using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Single_RandomSpeed : Muzzle
{
    void Update()
    {
        if (_pattenName == Pattern.Aim_at_Player)
        {
            Single_RandomSpeed();
        }
        if (_pattenName == Pattern.Designation)
        {
            Single_RandomSpeed(m_vector);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
