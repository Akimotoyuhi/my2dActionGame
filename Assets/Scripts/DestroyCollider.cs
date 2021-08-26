using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollider : MonoBehaviour
{
    [SerializeField] private GameObject m_destroyObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(m_destroyObj);
            Destroy(this.gameObject);
        }
    }
}
