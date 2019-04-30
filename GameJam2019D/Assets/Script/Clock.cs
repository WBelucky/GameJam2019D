using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    float totalTime = 0;
    public float limitTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((360f * totalTime) / limitTime > -360f)
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, (360f * totalTime) / limitTime);
            totalTime -= Time.deltaTime;
        }
        else
        {

        }


    }
}
