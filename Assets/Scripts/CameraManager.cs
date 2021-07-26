using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject m_player;
    private GameObject m_child;
    private 
    //private GameObject m_child;

    void Start()
    {
        m_player = GameObject.Find("Player");
        m_child = m_player.transform.Find("CM vcam").gameObject;
        //m_child = gameObject.transform.GetChild(0).gameObject;
        //m_child.SetActive(false);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //m_child.SetActive(true);
        }
    }
}
