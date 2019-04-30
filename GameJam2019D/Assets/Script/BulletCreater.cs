using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum  BulletType
{
    Straight,
    RapidStraight
}

public enum BulletImage
{
    makura,
    cuteMakura,
    horrorMakura
}

public class BulletCreater 
{
    bool isPlayerShooter = false;
    //オブジェクトプールを使うならここで拾ってくる処理を入れませう
    private GameObject creatingBullet;
    //まくらの画像を確率で変えるためのランダム
    System.Random rnd = new System.Random();    

    public BulletCreater(Transform transform,bool isPlayerShooter)
    {
        creatingBullet = new GameObject();
        creatingBullet.transform.parent = transform.parent;
        InitializeBullet(creatingBullet);
        AddCommonComponent(creatingBullet);
        this.isPlayerShooter = isPlayerShooter;
    }

    public void CretateBullet(BulletType bulletType, float angle, Transform transform)
    {
        GameObject cloneBullet = Object.Instantiate(creatingBullet, transform);
        var cloneBulletRenderer = cloneBullet.GetComponent<SpriteRenderer>();
        cloneBullet.GetComponent<BoxCollider2D>().enabled = true;
        cloneBulletRenderer.sprite = VariedBulletImage(transform);
        cloneBulletRenderer.sortingLayerName = "Bullet";
       
        switch (bulletType)
        {
            case BulletType.Straight:
                //このaddcomponentを行った瞬間に動きます。
                cloneBullet.AddComponent<StraightBullet>().Init(angle,isPlayerShooter);
                cloneBullet.transform.parent = null;
                break;
            case BulletType.RapidStraight:
                cloneBullet.AddComponent<StraightBullet>().Init(angle, isPlayerShooter);
                cloneBullet.transform.parent = null;
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
        Bullet.GetComponent<BoxCollider2D>().size = new Vector2(2.0f,1.5f);
        Bullet.GetComponent<BoxCollider2D>().enabled = false;
        Bullet.AddComponent<SpriteRenderer>();
        Bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        Bullet.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        Bullet.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        return Bullet;
    }
    //共通の初期設定があればここで行います。
    private GameObject InitializeBullet(GameObject Bullet)
    {
        Bullet.tag = "Bullet";
        Bullet.name = "StraightBulletOrigin";
        return Bullet;
    }
    
    private Sprite VariedBulletImage(Transform transform)
    {
        if (transform.name =="Kotatu")
        {
            return Resources.Load("mikan",typeof(Sprite)) as Sprite;
        }
        else
        {
            int x = rnd.Next(10);        // 0～9の乱数を取得
            if (x <= 4)
            {
                return Resources.Load("makura", typeof(Sprite)) as Sprite;
            }
            else if (x <= 8)
            {
                return Resources.Load("makuracute", typeof(Sprite)) as Sprite;
            }
            else
            {
                return Resources.Load("makurahorror", typeof(Sprite)) as Sprite;
            }
        }
    }
}
