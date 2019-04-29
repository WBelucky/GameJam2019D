using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  BulletType
{
    Straight
}

public enum BulletImage
{
    makura,
    cuteMakura,
    horrorMakura
}

public class BulletCreater 
{
    //オブジェクトプールを使うならここで拾ってくる処理を入れませう
    private GameObject creatingBullet = new GameObject();
    //まくらの画像を確率で変えるためのランダム
    System.Random rnd = new System.Random();    

    public BulletCreater()
    {
        InitializeBullet(creatingBullet);
        AddCommonComponent(creatingBullet);
    }

    public void CretateBullet(BulletType bulletType, float angle, Transform transform)
    {
        GameObject cloneBullet = Object.Instantiate(creatingBullet, transform);
        cloneBullet.GetComponent<SpriteRenderer>().sprite = VariedBulletImage();
        switch (bulletType)
        {
            case BulletType.Straight:
                //このaddcomponentを行った瞬間に動きます。
                cloneBullet.AddComponent<StraightBullet>().Init(angle);
                Debug.Log("弾発射");
                break;
            default:
                Debug.LogError("そんな弾の種類は存在しませんenumに追加してクラスを作ってちょ");
                break;
        }

    }
    //共通コンポーネントをここで貼り付けます。
    private GameObject AddCommonComponent(GameObject Bullet)
    {
        Bullet.AddComponent<BoxCollider2D>().isTrigger = true;
        Bullet.AddComponent<SpriteRenderer>();
        return Bullet;
    }
    //共通の初期設定があればここで行います。
    private GameObject InitializeBullet(GameObject Bullet)
    {
        Bullet.tag = "Bullet";
        Bullet.name = "StraightBullet";
        return Bullet;
    }
    
    private Sprite VariedBulletImage()
    {
        int x = rnd.Next(10);        // 0～9の乱数を取得
        if(x <= 4)
        {
            return Resources.Load("makura", typeof(Sprite)) as Sprite;
        }
        else if( x <= 8)
        {
            return Resources.Load("cutemakura", typeof(Sprite)) as Sprite;
        }
        else
        {
            return Resources.Load("makurahorror", typeof(Sprite)) as Sprite;
        }
    }
}
