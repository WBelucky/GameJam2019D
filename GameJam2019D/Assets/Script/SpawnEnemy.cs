using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    //bedの数
    private int bedNum = 10;
    //布団の数
    private int hutonNum = 10;

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
    public float SpawnTimeInterval = 5.0f;
    public float SpwanTimeTaker = 0;

    //今、どこまでリストのインデックスがいったかを保持するint
    int bedIndex = 0;
    int hutonIndex = 0;

    //全ての敵を打ち尽くしたときにこのboolをOnにして外に知らせる。
    public bool IsAllEnemyEmerged { private set; get; } = false;

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
        kotatu.AddComponent<SpriteRenderer>().sprite = kotatuSprite;
        kotatu.transform.parent = this.gameObject.transform;

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
                Debug.Log(bedIndex.ToString());
                EnableColliderRenderer(beds[bedIndex]);
                bedIndex++;
                Debug.Log(bedIndex.ToString());
            }
            if (hutonIndex < hutonNum)
            {
                EnableColliderRenderer(hutons[hutonIndex]);
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
        enemyOrigin.transform.parent = this.gameObject.transform;
        if (enemyClass == "Bed" || enemyClass == "Huton" || enemyClass == "Kotatu")
        {
            Debug.Log(enemyClass+"追加");
            System.Type className = System.Type.GetType(enemyClass);
            enemyOrigin.AddComponent(className);
            enemyOrigin.AddComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            Debug.LogError("そんな名前の敵はいないよ");
            Debug.LogError(enemyClass);
            return;
        }
        
        if(enemyClass == "Bed")
        {
            enemyOrigin.name = "Bed";
            enemyOrigin.AddComponent<SpriteRenderer>().sprite = bedSprite;
            enemyOrigin = UnableColliderRenderer(enemyOrigin);
           
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
            enemyOrigin = UnableColliderRenderer(enemyOrigin);
            
            for (int i = 0; i< enemyCount;i++)
            {
                gameObjectCashTmp = Object.Instantiate(enemyOrigin) as GameObject;
                gameObjectCashTmp.transform.position = enemySpawnPosition;
                hutons.Add(gameObjectCashTmp);
            }
        }
    }

    //そのゲームオブジェクトのcolliderとrendererをenableをonにしたりoffにしたりするやつ
    private GameObject EnableColliderRenderer(GameObject gameObject)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        return gameObject;
    }

    private GameObject UnableColliderRenderer(GameObject givenGameObject)
    {
        givenGameObject.GetComponent<BoxCollider2D>().enabled = false;
        givenGameObject.GetComponent<SpriteRenderer>().enabled = false;
        return givenGameObject;
    }
}
