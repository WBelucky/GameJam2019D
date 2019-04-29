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
    private List<GameObject> beds = new List<GameObject>();
    //ベッドの数を保持。
    private List<GameObject> hutons = new List<GameObject>();

    // これをコピー元として使いまわします。
    private GameObject enemyOrigin;

    //ここに敵の画像を保持。
    private readonly Sprite bedSprite;
    private readonly Sprite hutonSprite;
    private readonly Sprite kotatuSprite;

    //スポーンする間隔と前のスポーンからの時間を保持
    float SpawnTimeInterval = 5.0f;
    float SpwanTimeTaker = 0;

    //今、どこまでリストのインデックスがいったかを保持するint
    int bedIndex = 0;
    int hutonIndex = 0;

    //全ての敵を打ち尽くしたときにこのboolをOnにして外に知らせる。
    public bool IsAllEnemyEmerged { private set; get; } = false;

    //Unityの方で使う関数======================================================
    void Start()
    {
        enemyOrigin = new GameObject();
        //まずこたつを召喚する。
        GameObject kotatu = new GameObject();
        kotatu.AddComponent<Kotatu>();
        kotatu.AddComponent<Collider2D>();
        kotatu.AddComponent<SpriteRenderer>();
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
                EnableColliderRenderer(beds[bedIndex]);
                bedIndex++;
            }
            if (hutonIndex < hutonNum)
            {
                EnableColliderRenderer(hutons[hutonIndex]);
                hutonIndex++;
            }
        }
    }
    //==================================================================

    private void CreateInActiveEnemy(int enemyCount, string enemyClass)
    {
        enemyOrigin = new GameObject();
        enemyOrigin.transform.parent = null;
        if (enemyClass == "Bed" || enemyClass == "Huton" || enemyClass == "Kotatu")
        {
            System.Type className = System.Type.GetType(enemyClass);
            gameObject.AddComponent(className);
            gameObject.AddComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            Debug.LogError("そんな名前の敵はいないよ");
            Debug.LogError(enemyClass);
            return;
        }
        
        if(enemyClass == "Bed")
        {
            for(int i = 0; i< bedNum;i++)
            {
                gameObject.AddComponent<SpriteRenderer>().sprite = bedSprite;
                enemyOrigin = UnableColliderRenderer(enemyOrigin);
                beds.Add(Object.Instantiate(enemyOrigin) as GameObject);
            }
        }
        else if(enemyClass == "Huton")
        {
            for(int i = 0; i< hutonNum;i++)
            {
                gameObject.AddComponent<SpriteRenderer>().sprite = hutonSprite;
                enemyOrigin = UnableColliderRenderer(enemyOrigin);
                hutons.Add(Object.Instantiate(enemyOrigin) as GameObject);
            }
        }
    }

    //そのゲームオブジェクトのcolliderとrendererをenableをonにしたりoffにしたりするやつ
    private GameObject EnableColliderRenderer(GameObject gameObject)
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        return gameObject;
    }

    private GameObject UnableColliderRenderer(GameObject gameObject)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        return gameObject;
    }
}
