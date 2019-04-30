using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    //bedの数
    private int bedNum = 5;
    //布団の数
    private int hutonNum = 5;

    //布団のリストを保持。
    private List<GameObject> beds;
    //ベッドの数を保持。
    private List<GameObject> hutons;

    //これをコピー元として使いまわします。
    private GameObject enemyOrigin;

    //母艦こたつのオブジェクト
    private GameObject kotatu;

    //ここに敵の画像を保持。
    private  Sprite bedSprite ;
    private  Sprite hutonSprite;
    private  Sprite kotatuSprite;

    //スポーンする間隔と前のスポーンからの時間を保持
    private float SpawnTimeInterval = 5.0f;
    private float SpwanTimeTaker = 0;

    //今、どこまでリストのインデックスがいったかを保持するint
    int bedIndex = 0;
    int hutonIndex = 0;

    //全ての敵を打ち尽くしたときにこのboolをOnにして外に知らせる。
    public static bool IsAllEnemyEmerged { private set; get; } = false;

    //こたつの初期位置
    private Vector3 enemySpawnPosition = new Vector3(20.0f,20.0f,0f);

    //Unityの方で使う関数======================================================
    void Start()
    {
        //初期化
        this.gameObject.name = "spawnPoint";
        beds = new List<GameObject>();
        hutons = new List<GameObject>();

        //画像を引っ張ってくる。
        bedSprite = Resources.Load("bed", typeof(Sprite)) as Sprite;
        hutonSprite = Resources.Load("huton", typeof(Sprite)) as Sprite;
        kotatuSprite = Resources.Load("kotatu", typeof(Sprite)) as Sprite;
        enemyOrigin = new GameObject();
        //まずこたつを召喚する。
        kotatu = new GameObject("Kotatu");
        kotatu.tag = "Enemy";
        kotatu.transform.position = enemySpawnPosition;
        kotatu.AddComponent<Kotatu>();
        kotatu.AddComponent<BoxCollider2D>().isTrigger = true;
        kotatu.GetComponent<BoxCollider2D>().size = new Vector2(10.0f,8.0f);
        kotatu.AddComponent<SpriteRenderer>().sprite = kotatuSprite;
        kotatu.AddComponent<Rigidbody2D>().gravityScale = 0;
        kotatu.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        kotatu.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        kotatu.transform.parent = null;

        //先にすべての敵を作って取っておく。
        CreateInActiveEnemy(bedNum,"Bed");
        CreateInActiveEnemy(hutonNum,"Huton");
    }

    void Update()
    {
        
        //時間になったら敵をどんどん召喚する
        SpwanTimeTaker += Time.deltaTime;
        if (SpwanTimeTaker > SpawnTimeInterval)
        {
            SpwanTimeTaker = 0;
            if (bedIndex < bedNum)
            {
                beds[bedIndex].transform.position = kotatu.transform.position;
                EnableMainComponent(beds[bedIndex]);
                bedIndex++;
            }
            if (hutonIndex < hutonNum)
            {
                hutons[hutonIndex].transform.position = kotatu.transform.position;
                EnableMainComponent(hutons[hutonIndex]);
                hutonIndex++;
            }
            if(bedIndex == bedNum && hutonIndex == hutonNum)
            {
                IsAllEnemyEmerged = true;
            }
        }
    }
    //==================================================================

    private void CreateInActiveEnemy(int enemyCount, string enemyClass)
    {
        GameObject gameObjectCashTmp = new GameObject(); 
        enemyOrigin = new GameObject();
        enemyOrigin.tag = "Enemy";
        enemyOrigin.transform.parent = null;
        //まとめてできる処理と適切かのチェック
        if (enemyClass == "Bed" || enemyClass == "Huton" || enemyClass == "Kotatu")
        {
            System.Type className = System.Type.GetType(enemyClass);
            enemyOrigin.AddComponent(className);
            enemyOrigin.AddComponent<BoxCollider2D>().isTrigger = true;
            enemyOrigin.AddComponent<Rigidbody2D>().gravityScale = 0;
            enemyOrigin.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            enemyOrigin.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
           
        }
        else
        {
            Debug.LogError("そんな名前の敵はいないよ");
            Debug.LogError(enemyClass);
            return;
        }
        //個別の処理
        if(enemyClass == "Bed")
        {
            enemyOrigin.name = "Bed";
            enemyOrigin.AddComponent<SpriteRenderer>().sprite = bedSprite;
            enemyOrigin = UnableMainComponent(enemyOrigin);
            enemyOrigin.GetComponent<BoxCollider2D>().size = new Vector2(12.0f, 6.0f);
            enemyOrigin.GetComponent<BoxCollider2D>().offset = new Vector2(0,0);
            enemyOrigin.GetComponent<SpriteRenderer>().sortingLayerName = "Default";

            for (int i = 0; i< enemyCount;i++)
            {
                gameObjectCashTmp = Object.Instantiate(enemyOrigin) as GameObject;
                gameObjectCashTmp.transform.position = enemySpawnPosition;
                beds.Add(gameObjectCashTmp);
            }
        }
        else if(enemyClass == "Huton")
        {
            enemyOrigin.name = "Huton";
            enemyOrigin.AddComponent<SpriteRenderer>().sprite = hutonSprite;
            enemyOrigin = UnableMainComponent(enemyOrigin);
            enemyOrigin.GetComponent<BoxCollider2D>().size = new Vector2(12.0f, 6.0f);
            enemyOrigin.GetComponent<BoxCollider2D>().offset = new Vector2(-0.5f, -1.5f);
            enemyOrigin.GetComponent<SpriteRenderer>().sortingLayerName = "Default";

            for (int i = 0; i< enemyCount;i++)
            {
                gameObjectCashTmp = Object.Instantiate(enemyOrigin) as GameObject;
                gameObjectCashTmp.transform.position = enemySpawnPosition;
                hutons.Add(gameObjectCashTmp);
            }
        }
    }

    //そのゲームオブジェクトのcolliderとrendererをenableをonにしたりoffにしたりするやつ
    private GameObject EnableMainComponent(GameObject givenGameObject)
    {
        givenGameObject.GetComponent<BoxCollider2D>().enabled = true;
        givenGameObject.GetComponent<SpriteRenderer>().enabled = true;
        givenGameObject.GetComponent<Enemy>().enabled = true;
        return givenGameObject;
    }

    private GameObject UnableMainComponent(GameObject givenGameObject)
    {
        givenGameObject.GetComponent<BoxCollider2D>().enabled = false;
        givenGameObject.GetComponent<SpriteRenderer>().enabled = false;
        givenGameObject.GetComponent<Enemy>().enabled = false;
        return givenGameObject;
    }
}
