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
    [Header("オーディオ")]
    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_bgm;
    [SerializeField] private AudioClip m_bossBgm;

    void Start()
    {
        m_bossHpGage.SetActive(false);
        if (isTest)
        {
            m_spawnPoint = new Vector2(m_testSpawnPos.position.x, m_testSpawnPos.position.y);
        }
        PlayBgm();
        PlayerSpawn();
    }

    public void PlayBgm(bool isBoss = false)
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
