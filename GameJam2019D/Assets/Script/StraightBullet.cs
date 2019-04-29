using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    private float speed = 0.5f;
    private Vector3 velocity;
    private float damage = 200.0f;

    //unity側でつかう関数===================
    void Start()
    {
        
    }
    
    void Update()
    {
        Move();
    }
    //=======================================

    private void Move()
    {
        transform.localPosition += velocity;
    }

    // 指定された 2 つの位置から角度を求めて返す
    public  float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }


    // 弾を発射する時に初期化するための関数
    //angleはプレイヤーから渡されてくる。
    public void Init(float angle)
    {
        // 弾の発射角度をベクトルに変換する
        Vector3 direction = GetDirection(angle);

        // 発射角度と速さから速度を求める
        velocity = direction * speed;

        // 弾が進行方向を向くようにする
        Vector3 angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;

        // 4 秒後に削除する
        Destroy(gameObject, 4);
    }

    // 指定された角度（ 0 ～ 360 ）をベクトルに変換して返す
    public static Vector3 GetDirection(float angle)
    {
        return new Vector3
        (
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad),
            0
        );
    }
}
