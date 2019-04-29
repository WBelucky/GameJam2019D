using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void Move();
    public virtual void AddBulletShooterObject()
    {
        GameObject bulletShooter = new GameObject("BulletShooter", System.Type.GetType("BulletShooter"));
        bulletShooter.transform.parent = this.gameObject.transform;
    }
    public abstract void MovePatternSelect();

}
