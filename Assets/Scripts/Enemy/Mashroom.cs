using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashroom : Enemy
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Move()
    {
        if (m_player)
        {
            m_moveTimer += Time.deltaTime;
            if (m_moveTimer > m_moveinterval)
            {

            }
        }
    }
}
