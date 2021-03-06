﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static int score = 0;
    readonly static int ketasu = 6;
    GameObject[] numberDisplay = new GameObject[ketasu];
    SpriteRenderer[] renderers = new SpriteRenderer[ketasu];
    Sprite[] sprites = new Sprite[10];
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i <ketasu;i++)
        {
            numberDisplay[i] = new GameObject(i + "thNum", typeof(SpriteRenderer));
            numberDisplay[i].transform.parent = this.transform;
            numberDisplay[i].transform.localPosition = new Vector3(i * 3 + 10, 0);
            numberDisplay[i].transform.localScale = new Vector3(3,3,0);
            renderers[i] = numberDisplay[i].GetComponent<SpriteRenderer>();
            renderers[i].sortingLayerName = "UI";
            renderers[i].color = Color.magenta;
        }
        for(int i = 0;i < 10; i++)
        {
            sprites[i] = Resources.Load<Sprite>("Numbers/"+i);
           
        }
        

    }
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        string str = score.ToString("000000");
        for(int i = 0; i< ketasu;i++)
        {
            renderers[i].sprite = sprites[str[i] - '0'];
        }
        
    }
}
