using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public float totalTime { get; private set; } = 0;
    public float limitTime;

    public static Clock instance;

    void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -(360f * totalTime) / limitTime);
        totalTime += Time.deltaTime;


    }
    public bool IsTimerStoped()
    {
        return totalTime >= limitTime;
    }
}
