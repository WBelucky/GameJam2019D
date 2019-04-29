using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kotatu :Enemy
{
    private float hp = 1000000.0f;
    private float speed = 0f;
    //自分から見てプレイヤーがどの方向にいるかを示す
    private float angle;
    public override void Move()
    {
        // プレイヤーの現在位置へ向かうベクトルを作成する
            angle = GetAngle(
            transform.localPosition,
            Player.Instance.transform.localPosition);
        var direction = GetDirection(angle);

        // プレイヤーが存在する方向に移動する
        transform.localPosition += direction * speed;

        // プレイヤーが存在する方向を向く
        var angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
    }
    public override float returnEnemyAngle()
    {
        return angle;
    }
    public override void AddBulletShooterObject()
    {
        base.AddBulletShooterObject();
    }
    void Start()
    {
        AddBulletShooterObject();
    }

    public override void SetChangedHp(float damage)
    {
        if (hp - damage < 0)
        {
            hp = 0;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this);
            Resources.UnloadUnusedAssets();
        }
        else
        {
            hp -= damage;
        }
    }

    void Update()
    {
        Move();
        if(SpawnEnemy.IsAllEnemyEmerged)
        {
            speed = 0.01f;
            hp = 2000.0f;
        }
    }
}
