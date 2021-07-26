using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EriaManager : MonoBehaviour
{
    enum SavePosition
    {
        y,
        x
    }
    /// <summary>プレイヤーのエリア移動の際、どの座標の値を保持するか</summary>
    [SerializeField] private SavePosition m_savePosition = SavePosition.x;
    private GameObject m_canvas = null;
    private float m_alpha = 0f;
    /// <summary>ワープ先</summary>
    private Transform m_warpPos;
    void Start()
    {
        m_warpPos = gameObject.transform.GetChild(0);
        m_canvas = GameObject.Find("Canvas");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Fade();

            if (m_savePosition == SavePosition.x)
            {
                collision.transform.position = new Vector2(m_warpPos.position.x, collision.transform.position.y);
            }
            else
            {
                collision.transform.position = new Vector2(collision.transform.position.x, m_warpPos.position.y);
            }
        }
    }

    //private void Fade()
    //{
    //    Image image = m_canvas.transform.Find("Panel").GetComponent<Image>();
    //    while (m_alpha < 1)
    //    { 
    //        m_alpha += 0.001f;
    //        image.color = new Color(0, 0, 0, m_alpha);
    //    }

    //    while (m_alpha >= 0)
    //    {
    //        m_alpha -= 0.001f;
    //        image.color = new Color(0, 0, 0, m_alpha);
    //    }
    //}
}
