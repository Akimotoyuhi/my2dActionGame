using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBoss : BossEnemyBase
{
    private Actions[] m_actions;
    [SerializeField] private GameObject[] m_shot1;
    [SerializeField] private GameObject[] m_shot2;
    [SerializeField] private GameObject[] m_shot3;
    [SerializeField] private GameObject[] m_shot4;
    private int m_num = 0;

    void Start()
    {
        SetUp();
    }

    private void Update()
    {
        if (m_isMove)
        {
            m_isMove = true;
            ActionStart();
        }
    }

    private void ActionStart()
    {
        StartCoroutine(Action(m_num));
    }

    private void ActionSet()
    {
        m_actions = new Actions[4];
        m_actions[0] = Pattern1;
        //m_actions[1] = Pattern2;
        //m_actions[2] = Pattern3;
        //m_actions[3] = Pattern4;
    }

    private IEnumerator Action(int num)
    {
        yield return StartCoroutine(m_actions[num]());
        yield return new WaitForSeconds(m_moveInterval);
        m_num++;
        m_isMove = false;
    }

    private IEnumerator Pattern1()
    {
        StartCoroutine(TyottoShot(m_shot1[0], m_shot1[1], m_position[0]));
        yield return new WaitForSeconds(1f);
    }
}
