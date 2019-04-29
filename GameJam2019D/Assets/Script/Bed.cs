using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Enemy
{
    private float hp = 200.0f;
    private float speed = 0.1f;

    public override void Move()
    {
        // プレイヤーの現在位置へ向かうベクトルを作成する
        var angle = GetAngle(
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

    void Start()
    {
        AddBulletShooterObject();
    }

    void Update()
    {
        Move();
    }
}
