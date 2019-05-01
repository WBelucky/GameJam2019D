using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scenario : MonoBehaviour
{
    public float speed  = 1;
    GameObject canvas;
    public float whereToEraseY = 500;

    // Start is called before the first frame update
    void Start()
    {
        // TextCanvasを呼び出す
        canvas = GameObject.Find("TextCanvas");
    }

    // Update is called once per frame
    void Update()
    {   
        if (transform.localPosition.y <= whereToEraseY)
        {
            transform.Translate(0, 0.75f * speed * Time.deltaTime * 60, 0);
        }
    }
}
