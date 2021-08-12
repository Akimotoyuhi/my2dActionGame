using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itembase : MonoBehaviour
{
    enum ItemType
    {
        Wepon,
        Status
    }
    enum StatusItems
    {
        Life,
        Mana,
        Power
    }
    [SerializeField] private ItemType m_itemType = ItemType.Status;
    private Wepon m_wepon = Wepon.Blast;
    [SerializeField] private StatusItems m_statusItems = StatusItems.Life;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (m_itemType == ItemType.Wepon)
            {
                player.m_weponFlag[(int)m_wepon] = true;
            }
            else if (m_itemType == ItemType.Status)
            {
                if (m_statusItems == StatusItems.Life)
                {

                }
                else if (m_statusItems == StatusItems.Mana)
                {

                }
                else if (m_statusItems == StatusItems.Power)
                {

                }
            }
        }
    }
}
