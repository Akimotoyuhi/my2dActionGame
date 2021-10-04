using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCollider : MonoBehaviour
{
    [SerializeField]private GameObject m_activeOnject;
    void Start()
    {
        m_activeOnject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_activeOnject.SetActive(true);
    }
}
