using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour
{

    //アイテムで弾の種類を変えたいならここでいい感じに変える。
    // 複数の弾を発射する時の角度
    public float shotAngleRange = 30.0f;
    // 弾の発射タイミングを管理するタイマー
    public float shotTimer = 0;
    // 弾の発射数
    public int shotCount = 1;
    // 弾の発射間隔（秒）
    public float shotInterval = 2.0f;
    //弾の種類を変えるならここ
    BulletType bulletType = BulletType.Straight; 

    // プレイヤーまたは敵のangleを保持
    private float angle;
    // プレイヤーのtransformを拾ってくる。
    private Transform parentTransform;
    //弾の生産場所
    private BulletCreater bulletCreater;
    // プレイヤーならコンポーネントを保持
    private Player player;
    void Start()
    {
        
        
        // 親のオブジェクトのtranformを拾ってくる。
        parentTransform = transform.parent.gameObject.GetComponent<Transform>();

        // 親のオブジェクトのプレイヤーコンポ―ネントを拾ってくる。
        if (parentTransform.tag == "Player")
        {
            player = transform.parent.gameObject.GetComponent<Player>();
        }
        bulletCreater = new BulletCreater(parentTransform);
    }

    void Update()
    {
        if (parentTransform.tag == "Player")
        {
            angle = player.angle;
            // 時間になったら弾を発射する
            if (IsTimeOfShoot() && Input.GetMouseButton(0))
            {
                // 弾の発射タイミングを管理するタイマーをリセットする
                if (shotTimer > shotInterval) shotTimer = 0;
                ShootNWay(angle, shotAngleRange, shotCount);
            };
        }
        else { }
    }
    

    private bool IsTimeOfShoot()
    {
        // 弾の発射タイミングを管理するタイマーを更新する
        shotTimer += Time.deltaTime;
        bool a = shotTimer > shotInterval;
        
        //boolを返す。
        return a;
    }

    // 弾を発射する関数
    private void ShootNWay(
        float angleBase, float angleRange, int count)
    {

        // 弾を複数発射する場合
        if (1 < count)
        {
            // 発射する回数分ループする
            for (int i = 0; i < count; ++i)
            {
                // 弾の発射角度を計算する
                var angle = angleBase +
                    angleRange * ((float)i / (count - 1) - 0.5f);

                // 発射する弾を生成する
                bulletCreater.CretateBullet(bulletType, angle, parentTransform);
            }
        }
        // 弾を 1 つだけ発射する場合
        else if (count == 1)
        {
            // 発射する弾を生成する
            bulletCreater.CretateBullet(bulletType, angle, parentTransform);
        }
    }
}
