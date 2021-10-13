using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooltestBullet : MonoBehaviour, IPoolable
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    //以下オブジェクトプール
    public bool IsActive => m_renderer.enabled;
    private Renderer m_renderer;

    public void DisactiveForInstantiate()
    {
        //生成時に非表示にする
        m_renderer = GetComponent<Renderer>();
        m_renderer.enabled = false;
    }

    public void Create()
    {

    }

    public void Destroy()
    {

    }
}
