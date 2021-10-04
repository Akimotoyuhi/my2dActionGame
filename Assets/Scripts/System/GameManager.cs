using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject m_player = null;
    [System.NonSerialized] public Vector2 m_spawnPoint = new Vector2(-11f, -2.5f);
    [SerializeField] private Transform m_testSpawnPos;
    [SerializeField] private bool isTest = false;
    [SerializeField] private GameObject m_bossHpGage;
    [SerializeField] private GameObject m_pause;
    [Header("オーディオ")]
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_bgm;
    [SerializeField] private AudioClip m_bossBgm;
    private bool m_isPause = false;

    void Start()
    {
        m_bossHpGage.SetActive(false);
        m_pause.SetActive(false);
        if (isTest)
        {
            m_spawnPoint = new Vector2(m_testSpawnPos.position.x, m_testSpawnPos.position.y);
        }
        PlayBgm();
        PlayerSpawn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_isPause)
            {
                m_isPause = false;
                Time.timeScale = 1;
                m_pause.SetActive(false);
            }
            else
            {
                m_isPause = true;
                Time.timeScale = 0;
                m_pause.SetActive(true);
            }
        }
    }

    public void PlayBgm(bool isBoss = false)
    {
        if (m_audioSource)
        {
            if (isBoss)
            {
                m_audioSource.clip = m_bossBgm;
                m_audioSource.Play();
            }
            else
            {
                m_audioSource.clip = m_bgm;
                m_audioSource.Play();
            }
        }
    }

    /// <summary>
    /// プレイヤーが死んだ事を受け取って数秒待ってからスポーンさせる
    /// </summary>
    public void PlayerDead()
    {
        Invoke("PlayerSpawn", 3f);
    }

    /// <summary>
    /// プレイヤーのスポーン
    /// </summary>
    public void PlayerSpawn()
    {
        Instantiate(m_player, m_spawnPoint, Quaternion.identity);
    }
}
