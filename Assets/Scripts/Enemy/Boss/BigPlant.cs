using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigPlant : BossEnemyBase
{
    enum Faze
    {
        one = 1,
        two = 2,
    }

    private Faze m_faze = Faze.one;
    private Actions[] m_actions;
    [Header("パターン１")]
    [SerializeField] private Single[] m_singles1;
    [SerializeField] private Nway[] m_nways1;
    [SerializeField] private Uzumaki[] uzumakis1;
    [Header("パターン２")]
    [SerializeField] private Single[] m_singles2;
    [SerializeField] private Nway[] m_nways2;
    [SerializeField] private Uzumaki[] uzumakis2;
    [Header("パターン３")]
    [SerializeField] private Single[] m_singles3;
    [SerializeField] private Nway[] m_nways3;
    [SerializeField] private Uzumaki[] uzumakis3;
    [Header("パターン４")]
    [SerializeField] private Single[] m_singles4;
    [SerializeField] private Nway[] m_nways4;
    [SerializeField] private Uzumaki[] uzumakis4;


    private void Start()
    {
        FullSetUp();
        ActionSet();
        NumberSet();
    }

    private void NumberSet()
    {
        if (m_faze == Faze.one)
        {
            int i = Random.Range(0, 2);
            StartCoroutine(Action(i));
        }
        else if (m_faze == Faze.two)
        {
            int i = Random.Range(2, m_actions.Length);
            StartCoroutine(Action(i));
        }
    }

    private void ActionSet()
    {
        m_actions = new Actions[4];
        m_actions[0] = Pattern1;
        m_actions[1] = Pattern2;
        m_actions[2] = Pattern3;
        m_actions[3] = Pattern4;
    }

    private void Pattern1()
    {
        SetOnShot(m_singles1, m_nways1, uzumakis1);
    }

    private void Pattern2()
    {
        SetOnShot(m_singles2, m_nways2, uzumakis2);
    }

    private void Pattern3()
    {
        SetOnShot(m_singles3, m_nways3, uzumakis3);
    }

    private void Pattern4()
    {
        SetOnShot(m_singles4, m_nways4, uzumakis4);
    }

    /// <summary>
    /// ランダムで決定した行動をする
    /// </summary>
    /// <param name="num">行動する関数</param>
    /// <returns></returns>
    private IEnumerator Action(int num)
    {
        m_actions[num]();
        yield return new WaitForSeconds(m_moveinterval);
        NumberSet();
    }

    /// <summary>
    /// 体力が一定量以下になったら形態移行する
    /// </summary>
    private void FazeChanger()
    {
        if (Percent(m_life ,m_maxLife) < 50 && m_faze == Faze.one)
        {
            m_faze++;
            NumberSet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerBullet" || collision.tag == "Blast")
        {
            BulletBase bullet = collision.GetComponent<BulletBase>();
            m_life -= bullet.m_power;
            Vector2 v = new Vector2(this.transform.position.x + Random.Range(-1f, 1f), this.transform.position.y + Random.Range(-1f, 1f));
            var inst = Instantiate(m_damagePrefab, v, Quaternion.identity);
            inst.GetComponent<DamageText>().m_vec = v;
            Text text = inst.transform.GetChild(0).GetComponent<Text>();
            text.text = $"{bullet.m_power}";
        }

        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }
        else
        {
            FazeChanger();
            Debug.Log($"敵体力{Percent(m_life, m_maxLife)}%");
        }
    }
}
