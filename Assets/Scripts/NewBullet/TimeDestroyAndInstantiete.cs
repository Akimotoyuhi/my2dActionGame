using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyAndInstantiete : NewBullet
{
    [SerializeField] GameObject m_muzzle;
    [SerializeField] float m_destroyTime;
    float m_timer = 0;

    void Update()
    {
        Move();
        m_timer += Time.deltaTime;
        if (m_timer > m_destroyTime)
        {
            Instantiate(m_muzzle, this.gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
