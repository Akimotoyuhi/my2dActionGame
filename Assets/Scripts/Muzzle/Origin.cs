using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Origin : BulletBase
{
    [SerializeField] private Single[] m_singles;
    [SerializeField] private Nway[] m_nways;
    [SerializeField] private Uzumaki[] m_uzumakis;

    private void Start()
    {
        SetState();
    }

    void Update()
    {
        for (int i = 0; i < m_singles.Length; i++)
        {
            m_singles[i].OnShot();
        }
        for (int i = 0; i < m_nways.Length; i++)
        {
            m_nways[i].OnShot();
        }
        for (int i = 0; i < m_uzumakis.Length; i++)
        {
            m_uzumakis[i].OnShot();
        }
    }
}
