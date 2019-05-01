using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class leerScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float time = 7f;
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                SceneManager.LoadScene("TitleScene");
            }
        }
       
    }
}
