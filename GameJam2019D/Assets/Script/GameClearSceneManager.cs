using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameClearSceneManager : MonoBehaviour
{
    public Image GameClearImage;
    public Image BakusiImage;
    public Text text;
    public Text score;  
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float time1 = 5.0f;
    // Update is called once per frame
    void Update()
    {
        time1 -= Time.deltaTime;
       // if(time1 >= 0)
        {
            //BakusiImage.enabled = false;
        }
        if (time1 < 0)
        {
            //BakusiImage.enabled = false;
            GameClearImage.GetComponent<FadeScript>().StartFadingOut();
        }
       if(time1 < -1)
        {
            BakusiImage.GetComponent<FadeScript>().StartfadingIn();
            //BakusiImage.enabled = true;
           //GameClearImage.enabled = false;
        }
    }
}
