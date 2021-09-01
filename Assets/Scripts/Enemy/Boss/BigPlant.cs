using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigPlant : BossEnemyBase
{
    private delegate void Actions();

    enum Faze
    {
        zero = 0,
        one = 1,
    }

    enum Pattern
    {
        way,
    }

    private Faze m_faze = Faze.zero;
    private Pattern m_pattern = Pattern.way;
    private Actions[] m_actions;
    private Transform m_muzzle;
    [Header("パターン１")]
    [SerializeField] private Nway m_nway1;
    [SerializeField] private Nway m_nway2;
    [Header("パターン２")]
    [SerializeField] private Single m_single1;

    void Start()
    {
        FullSetUp();
        ActionSet();
        NumberSet();
    }

    private void NumberSet()
    {
        int i = Random.Range(0, m_actions.Length);
        StartCoroutine(Action(i));
    }

    private void ActionSet()
    {
        m_actions = new Actions[2];
        m_actions[0] = Pattern1;
        m_actions[1] = Pattern2;
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
    }
}
