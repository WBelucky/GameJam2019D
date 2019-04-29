using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player Instance;
    private float HP = 1000.0f;
    private float speed = 0.5f;
    
    // 移動可能な範囲
    public static Vector2 m_moveLimit = new Vector2(50.0f, 50.0f);

    public  Sprite[] sprites = new Sprite[4];

    //unity内で使う関数
    //=============================
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
       
    }
    
    void Update()
    {
        PlayerMove();
        SpriteChangeByPlayerRotation();
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,0);
    }
    //=============================

    //ここからプレイヤーの持つメソッド部分

    public void PlayerMove()
    {
        // 矢印キーの入力情報を取得する
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 矢印キーが押されている方向にプレイヤーを移動する
        Vector3 velocity = new Vector3(h, v) * speed;
        transform.localPosition += velocity;
        transform.localPosition = ClampPosition(transform.localPosition);
        // プレイヤーのスクリーン座標を計算する
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        // プレイヤーから見たマウスカーソルの方向を計算する
        Vector3 direction = Input.mousePosition - screenPos;

        // マウスカーソルが存在する方向の角度を取得する
        float angle = GetAngle(Vector3.zero, direction);

        // プレイヤーがマウスカーソルの方向を見るようにする
        Vector3 angles = transform.localEulerAngles;
        angles.z = angle - 90;
        transform.localEulerAngles = angles;
    }


    // 指定された位置を移動可能な範囲に収めた値を返す
    public static Vector3 ClampPosition(Vector3 position)
    {
        return new Vector3
        (
            Mathf.Clamp(position.x, -m_moveLimit.x, m_moveLimit.x),
            Mathf.Clamp(position.y, -m_moveLimit.y, m_moveLimit.y),
            0
        );
    }
    // 指定された 2 つの位置から角度を求めて返す
    public static float GetAngle(Vector2 from, Vector2 to)
    {
        var dx = to.x - from.x;
        var dy = to.y - from.y;
        var rad = Mathf.Atan2(dy, dx);
        return rad * Mathf.Rad2Deg;
    }

    public void SpriteChangeByPlayerRotation() {
        float currentZRotation = transform.rotation.eulerAngles.z + 180;
        SpriteRenderer spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        if (currentZRotation <= 225 && currentZRotation >= 135)
        {
            spriteRenderer.sprite = sprites[3];
        }
        else if(currentZRotation > 225 && currentZRotation < 315)
        {
            spriteRenderer.sprite = sprites[2];
        }
        else if((currentZRotation < 360 && currentZRotation >= 315) ||(currentZRotation >= 0 && currentZRotation <= 45))
        {
            spriteRenderer.sprite = sprites[0];
        }
        else
        {
            spriteRenderer.sprite = sprites[1];
        }
     }
}

    