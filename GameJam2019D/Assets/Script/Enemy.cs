﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public abstract void Move();
    public virtual void AddCustomBulletShooterObject()
    {
        GameObject bulletShooter = new GameObject("BulletShooter", System.Type.GetType("BulletShooter"));
        bulletShooter.transform.parent = this.gameObject.transform;
    }

    public virtual void AddCustomBulletShooterObject(float givenShotAngleRange, float givenShotInterval, int givenShotCount, BulletType givenBulletType)
    {
        GameObject bulletShooter = new GameObject("CustomBulletShooter", System.Type.GetType("BulletShooter"));
        bulletShooter.GetComponent<BulletShooter>().Init(givenShotAngleRange, givenShotInterval, givenShotCount, givenBulletType);
        bulletShooter.transform.parent = this.gameObject.transform;
    }
    //HPのセッター用の関数。
    public abstract void SetChangedHp(float damage);

    //指定された 2 つの位置から角度を求めて返す
    protected virtual float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }
    //指定された角度（ 0 ～ 360 ）をベクトルに変換して返す
    protected virtual Vector3 GetDirection(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        );
    }
    //弾をどの方向にうつかに使う関数
    public abstract float returnEnemyAngle();
}
