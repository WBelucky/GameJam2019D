using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += ResetTimer;
    }

    private void ResetTimer(Scene arg0, Scene arg1)
    {
        Timecount = 5;
    }

    float Timecount = 5;
    // Update is called once per frame
    void Update()
    {
        Timecount -= Time.deltaTime;
        if(Timecount <= 0)
        {
            
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                if (Input.GetMouseButton(0))
                {
                    SceneManager.LoadScene("TitleScene");
                    Destroy(this.gameObject);
                }
            }
            if (SceneManager.GetActiveScene().name == "GameClear")
            {
                if (Input.GetMouseButton(0))
                {
                    SceneManager.LoadScene("TitleScene");
                    Destroy(this.gameObject);
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            if (Player.Instance.HP <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            if (Kotatu.isClear)
            {
                SceneManager.LoadScene("GameClear");
            }
        }

    }
    
}
