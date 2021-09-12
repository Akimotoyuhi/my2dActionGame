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
    [SerializeField] private GameObject[] m_shot1;
    [SerializeField] private GameObject[] m_shot2;
    [SerializeField] private GameObject[] m_shot3;
    [SerializeField] private GameObject[] m_shot4;


    private void Start()
    {
        AnimSetUp();
        ActionSet();
    }

    private void Update()
    {
        if (!m_isMove)
        {
            m_isMove = true;
            ActionStart();
        }
    }

    private void ActionStart()
    {
        if (m_faze == Faze.one)
        {
            int i = Random.Range(0, 2);
            StartCoroutine(Action(i));
        }
        else if (m_faze == Faze.two)
        {
            int i = Random.Range(2, 4);
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


    /// <summary>
    /// ランダムで決定した行動をする
    /// </summary>
    /// <param name="num">行動する関数</param>
    /// <returns></returns>
    private IEnumerator Action(int num)
    {
        yield return StartCoroutine(m_actions[num]());
        yield return new WaitForSeconds(m_moveInterval);
        m_isMove = false;
    }

    private IEnumerator Pattern1()
    {
        yield return StartCoroutine(AllShot(m_shot1, m_position[0], 1));
    }

    private IEnumerator Pattern2()
    {
        StartCoroutine(TyottoShot(m_shot2[0], m_position[0]));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(TyottoShot(m_shot2[1], m_position[0]));
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(TyottoShot(m_shot2[2], m_position[0]));
    }

    private IEnumerator Pattern3()
    {
        yield return StartCoroutine(TyottoShot(m_shot3[0], m_shot3[1], m_position[0], 2f));
        yield return new WaitForSeconds(0.1f);
        yield return StartCoroutine(TyottoShot(m_shot3[2], m_shot3[3], m_position[0], 2f));
        yield return new WaitForSeconds(1f);
    }

    private IEnumerator Pattern4()
    {
        StartCoroutine(TyottoShot(m_shot4[0], m_position[0]));
        yield return new WaitForSeconds(5f);
    }

    /// <summary>
    /// 体力が一定量以下になったら形態移行する
    /// </summary>
    private void FazeChanger()
    {
        if (Percent(m_life ,m_maxLife) < 50 && m_faze == Faze.one)
        {
            m_faze++;
            ActionStart();
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
