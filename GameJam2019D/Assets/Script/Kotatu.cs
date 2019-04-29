using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kotatu :Enemy
{
    
    public override void Move()
    {
        throw new System.NotImplementedException();
    }

    public override void AddBulletShooterObject()
    {
        base.AddBulletShooterObject();
    }
    void Start()
    {
        AddBulletShooterObject();
    }

     void Update()
    {
        
    }

    public override void MovePatternSelect()
    {
        throw new System.NotImplementedException();
    }
}
