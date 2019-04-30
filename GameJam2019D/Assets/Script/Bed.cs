using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Enemy
{
    private float hp = 3000.0f;
    private float speed = 0.1f;
    public int point = 500;
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


    public override void SetChangedHp(float damage)
    {
        if (hp - damage < 0)
        {
            hp = 0;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            ScoreManager.score += point;
            Destroy(this);
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
            Resources.UnloadUnusedAssets();
        }
        else
        {
            hp -= damage;
        }
    }

    void Start()
    {
        AddCustomBulletShooterObject(30.0f, 3.5f, 1, BulletType.Straight);
        AddCustomBulletShooterObject(30.0f, 7.0f, 2, BulletType.Straight);
    }

    void Update()
    {
        Move();
    }
}
