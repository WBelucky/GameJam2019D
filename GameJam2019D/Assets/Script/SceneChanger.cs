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
        Timecount = 7;
    }

    float Timecount = 7;
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
            if(Input.GetKey(KeyCode.F2))
            {
                SceneManager.LoadScene("GameClear");
                Kotatu.isClear = false;

            }
            if (Player.Instance.HP <= 0 || Clock.instance.IsTimerStoped())
            {
                SceneManager.LoadScene("GameOver");
            }
            if (Kotatu.isClear)
            {
                SceneManager.LoadScene("GameClear");
                Kotatu.isClear = false;
            }
        }


        Debug.Log("TimeCount" + Timecount);
    }
    
}
