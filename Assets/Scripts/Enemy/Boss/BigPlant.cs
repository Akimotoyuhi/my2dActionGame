using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigPlant : BossEnemyBase
{
    private delegate void Actions();

    enum Faze
    {
        one = 1,
        two = 2,
    }

    enum Pattern
    {
        way = 0,
        single = 1,
        uzumaki = 2
    }

    private Faze m_faze = Faze.one;
    private Actions[] m_actions;
    [Header("パターン１")]
    [SerializeField] private Nway m_nway1;
    [SerializeField] private Nway m_nway2;
    [SerializeField] private Nway m_nway3;
    [Header("パターン２")]
    [SerializeField] private Single m_single1;
    [Header("パターン３")]
    [SerializeField] private Uzumaki m_uzumaki1;
    [SerializeField] private Uzumaki m_uzumaki2;
    [SerializeField] private Uzumaki m_uzumaki3;
    [SerializeField] private Uzumaki m_uzumaki4;


    void Start()
    {
        FullSetUp();
        ActionSet();
        NumberSet();
    }

    private void NumberSet()
    {
        Debug.Log("スタート");
        if (m_faze == Faze.one)
        {
            int i = Random.Range((int)Pattern.way, (int)Pattern.uzumaki);
            StartCoroutine(Action(i));
        }
        else if (m_faze == Faze.two)
        {
            int i = Random.Range((int)Pattern.uzumaki, m_actions.Length);
            StartCoroutine(Action(i));
        }
    }

    private void ActionSet()
    {
        m_actions = new Actions[3];
        m_actions[0] = Pattern1;
        m_actions[1] = Pattern2;
        m_actions[2] = Pattern3;
    }

    private void Pattern1()
    {
        m_nway1.OnShot();
        m_nway2.OnShot();
    }

    private void Pattern2()
    {
        m_single1.OnShot();
    }

    private void Pattern3()
    {
        m_uzumaki1.OnShot();
        m_uzumaki2.OnShot();
        m_uzumaki3.OnShot();
        m_uzumaki4.OnShot();
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

    private void FazeChanger()
    {
        if (Percent(m_life ,m_maxLife) < 50)
        {
            m_faze++;
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
