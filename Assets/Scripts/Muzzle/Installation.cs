using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Installation : Muzzle
{
    void Start()
    {
        
    }

    void Update()
    {
        Installation();
    }

    public override void OnShot()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Shot()
    {
        throw new System.NotImplementedException();
    }

    public override IEnumerator Shot(Vector2 vec)
    {
        throw new System.NotImplementedException();
    }
}
