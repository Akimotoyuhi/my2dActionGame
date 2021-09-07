using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashroom : Enemy
{
    void Start()
    {
        AnimSetUp();
    }

    void Update()
    {
        AtPlayer();
        m_muzzle[0].SetActive(true);
    }
}
