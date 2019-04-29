using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenario : MonoBehaviour
{
    float speed  = 1;
    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        // TextCanvasを呼び出す
        canvas = GameObject.Find("TextCanvas");
    }

    // Update is called once per frame
    void Update()
    {
        // 上矢印キーをおした時の処理
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            speed++;
        }
        // 下矢印キーをおした時の処理
        else if (Input.GetKeyDown(KeyCode.DownArrow) && speed > 1)
        {
            speed--;
        }

        // 0.05ずつy軸マイナス方向にtextを動かしていく
        transform.Translate(0, 0.25f * speed, 0);
        // textのy座標が-55以下になったらcanvasを削除
        if (transform.localPosition.y >= 500)
        {
            Destroy(canvas);
        }
    }
}
