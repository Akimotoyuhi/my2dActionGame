using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashroom : Enemy
{
    void Start()
    {
        AnimSetUp();
        m_muzzle[0].SetActive(true);
    }

    void Update()
    {
        AtPlayer();
    }
}
