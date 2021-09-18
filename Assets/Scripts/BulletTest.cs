using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    enum Pattern
    {
        none = 0,
        NwayAndAtPlayer = 1,
        SpeedUpAtPlayer = 2,
        SpinAndSpeedUp = 3,
        Tornado = 4,
        go = 5,
        six = 6,
        seven = 7,
        eite = 8,
        nine = 9,
        ten = 10
    }
    [SerializeField] private Pattern m_pattern = Pattern.none;
    [SerializeField] private GameObject[] m_shot1;
    [SerializeField] private GameObject[] m_shot2;
    [SerializeField] private GameObject[] m_shot3;
    [SerializeField] private GameObject[] m_shot4;
    [SerializeField] private GameObject[] m_shot5;
    [SerializeField] private GameObject[] m_shot6;
    [SerializeField] private GameObject[] m_shot7;
    [SerializeField] private GameObject[] m_shot8;
    [SerializeField] private GameObject[] m_shot9;
    [SerializeField] private GameObject[] m_shot10;
    public delegate IEnumerator Actions();
    private Actions[] m_actions;
    private bool m_isShot = false;
    private bool m_flag = false;

    private void Start()
    {
        ActionSet();
    }

    private void Update()
    {
        if (!m_isShot)
        {
            m_isShot = true;
            ActionStart();
        }
    }

    private void ActionStart()
    {
        StartCoroutine(Action((int)m_pattern));
    }

    private IEnumerator Action(int num)
    {
        yield return StartCoroutine(m_actions[num]());
        m_isShot = false;
    }

    private void ActionSet()
    {
        m_actions = new Actions[10];
        m_actions[0] = Pattern0;
        m_actions[1] = Pattern1;
        m_actions[2] = Pattern2;
        m_actions[3] = Pattern3;
        m_actions[4] = Pattern4;
        //m_actions[5] = Pattern5;
        //m_actions[6] = Pattern6;
        //m_actions[7] = Pattern7;
        //m_actions[8] = Pattern8;
        //m_actions[9] = Pattern9;
        //m_actions[10] = Pattern10;
    }

    private IEnumerator Pattern0()
    {
        yield return null;
    }

    private IEnumerator Pattern1()
    {
        while (m_pattern == Pattern.NwayAndAtPlayer)
        {
            StartCoroutine(TyottoShot(m_shot1[0], this.transform));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(TyottoShot(m_shot1[1], this.transform));
            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator Pattern2()
    {
        while (m_pattern == Pattern.SpeedUpAtPlayer)
        {
            StartCoroutine(AllShot(m_shot2, this.transform));
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator Pattern3()
    {
        while (m_pattern == Pattern.SpinAndSpeedUp)
        {
            yield return StartCoroutine(AllShot(m_shot3, this.transform, 0.5f));
        }
    }

    private IEnumerator Pattern4()
    {
        while (m_pattern == Pattern.Tornado)
        {
            yield return StartCoroutine(AllShot(m_shot4, this.transform, 99f));
        }
    }

    public IEnumerator AllShot(GameObject[] muzzle, Transform pos, float sec = 0)
    {
        GameObject[] g = new GameObject[muzzle.Length];
        for (int i = 0; i < muzzle.Length; i++)
        {
            g[i] = Instantiate(muzzle[i], pos.position, Quaternion.identity);
            g[i].transform.parent = this.transform;
        }
        yield return new WaitForSeconds(sec);
        for (int i = 0; i < muzzle.Length; i++)
        {
            Destroy(g[i]);
        }
    }

    public IEnumerator TyottoShot(GameObject muzzle, Transform pos, float sec = 0)
    {
        GameObject g = Instantiate(muzzle, pos.position, Quaternion.identity);
        //g.transform.parent = this.transform;
        yield return new WaitForSeconds(sec);
        Destroy(g);
    }

    public void TyottoShot(GameObject muzzle1, GameObject muzzle2, Transform pos)
    {
        GameObject g1 = Instantiate(muzzle1, pos.position, Quaternion.identity);
        g1.transform.parent = this.transform;
        GameObject g2 = Instantiate(muzzle2, pos.position, Quaternion.identity);
        g2.transform.parent = this.transform;
    }

    public void TyottoShot(GameObject muzzle1, GameObject muzzle2, GameObject muzzle3, Transform pos)
    {
        GameObject g1 = Instantiate(muzzle1, pos.position, Quaternion.identity);
        g1.transform.parent = this.transform;
        GameObject g2 = Instantiate(muzzle2, pos.position, Quaternion.identity);
        g2.transform.parent = this.transform;
        GameObject g3 = Instantiate(muzzle3, pos.position, Quaternion.identity);
        g3.transform.parent = this.transform;
    }
}
