using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public float alfa=255;
    float fadespeed = 0.01f;
    float red, green, blue;

    bool isFadingOut = false;
    bool isFadingIn = false;

    public void StartFadingOut()
    {
        isFadingOut = true;
    }
    public void StartfadingIn()
    {
        isFadingIn = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        red = GetComponent<Image>().color.r;
        green = GetComponent<Image>().color.g;
        blue = GetComponent<Image>().color.b;
        alfa = GetComponent<Image>().color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFadingOut)
        {
            alfa -= fadespeed;
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
            
        }
        if (isFadingIn)
        {
            alfa += fadespeed;
            GetComponent<Image>().color = new Color(red, green, blue, alfa);
        }
        
    }
}
