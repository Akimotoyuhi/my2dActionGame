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
    [SerializeField] private ShotFoword[] m_shot1;
    [SerializeField] private ShotFoword[] m_shot2;
    [SerializeField] private ShotFoword[] m_shot3;
    [SerializeField] private ShotFoword[] m_shot4;


    private void Start()
    {
        AnimSetUp();
        ActionSet();
        NumberSet();
    }

    private void NumberSet()
    {
        if (m_faze == Faze.one)
        {
            int i = 0;
            //int i = Random.Range(0, 2);
            Action(i);
        }
        else if (m_faze == Faze.two)
        {
            //int i = Random.Range(2, m_actions.Length);
            //Action(i);
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

    private IEnumerator Pattern1()
    {
        StartCoroutine(AllShot(m_shot1, 1));
        yield break;
    }

    private IEnumerator Pattern2()
    {
        yield return null;
    }

    private IEnumerator Pattern3()
    {
        yield return null;
    }

    private IEnumerator Pattern4()
    {
        yield return null;
        //SetOnShot(m_singles4, m_nways4, uzumakis4);
    }

    /// <summary>
    /// ランダムで決定した行動をする
    /// </summary>
    /// <param name="num">行動する関数</param>
    /// <returns></returns>
    private void Action(int num)
    {
        m_actions[num]();
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
