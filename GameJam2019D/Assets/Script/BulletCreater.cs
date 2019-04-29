using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  BulletType
{
    Straight
}
public class BulletCreater 
{
    //オブジェクトプールを使うならここで拾ってくる処理を入れませう
    private GameObject creatingBullet = new GameObject();

    public GameObject CretateBullet(BulletType bulletType)
    {
        switch (bulletType)
        {
            case BulletType.Straight:
                creatingBullet.AddComponent<StraightBullet>();
                return creatingBullet;
                
            default:
                Debug.LogError("そんな弾の種類は存在しませんenumに追加してクラスを作ってちょ");
                return creatingBullet;
        }

    }
    
}
