using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : Enemy
{
    void Start()
    {
        AnimSetUp();
    }

    void Update()
    {
        AtPlayer();
    }
}
