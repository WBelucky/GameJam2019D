using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kotatu :Enemy
{
    private float hp = 1000000.0f;
    private float speed = 0f;
    public int point = 10000;
    public static bool isClear = false;
    //自分から見てプレイヤーがどの方向にいるかを示す
    private float angle;
    private bool isWeakend = false;
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
    public override void AddCustomBulletShooterObject()
    {
        base.AddCustomBulletShooterObject();
    }
    void Start()
    {
        AddCustomBulletShooterObject(30.0f, 10.0f, 1, BulletType.Straight);
        AddCustomBulletShooterObject(30.0f, 2.0f, 12, BulletType.Straight);
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
            isClear = true;
        }
        else
        {
            hp -= damage;
            Debug.Log(hp.ToString());
        }
    }

    void Update()
    {
        Move();
        if(SpawnEnemy.IsAllEnemyEmerged && !isWeakend)
        {
            speed = 0.01f;
            hp = 2000.0f;
            isWeakend = true;
        }
    }
}
