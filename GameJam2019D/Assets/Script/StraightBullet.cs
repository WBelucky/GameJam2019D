using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{
    private float speed = 0.2f;
    private Vector3 velocity;
    private float damage = 200.0f;
    private bool isPlayerBullet = false;

    //unity側でつかう関数===================
    void Start()
    {
        Destroy(gameObject, 4);
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
    public void Init(float angle, bool isPlayerBullet)
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

        //敵か味方の弾かの識別
        this.isPlayerBullet = isPlayerBullet;
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
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player" && !isPlayerBullet)
        {
            Debug.Log("味方の弾衝突した");
            Player.Instance.HP -= damage;
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this);
            Resources.UnloadUnusedAssets();
        }
        else if (collision.tag == "Enemy" && isPlayerBullet)
        {
            Debug.Log("衝突した");
            collision.GetComponent<Enemy>().SetChangedHp(damage);
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(this);
            Resources.UnloadUnusedAssets();
        }
    }
}
