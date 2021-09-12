﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyAndInstantiete : NewBullet
{
    [SerializeField] GameObject m_muzzle;
    [SerializeField] float m_destroyTime;
    float m_timer = 0;

    void Start()
    {
        
    }

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer > m_destroyTime)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Instantiate(m_muzzle, this.transform.position, Quaternion.identity);
    }
}
