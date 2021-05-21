using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    private string ground = "Block";
    private bool isGrouded = false;
    private bool m_enter, m_stay, m_exit;

    public bool IsGrouded()
    {
        if (m_enter || m_stay)
        {
            isGrouded = true;
        }
        else if (m_exit)
        {
            isGrouded = false;
        }

        m_enter = false;
        m_exit = false;
        m_stay = false;

        return isGrouded;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ground)
        {
            m_enter = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == ground)
        {
            m_stay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == ground)
        {
            m_exit = true;
        }
    }
}
