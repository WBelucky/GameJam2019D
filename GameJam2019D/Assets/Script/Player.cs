using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static Player instance;
    public float speed;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 velocity = new Vector3(x, y) .normalized* speed;
        transform.localPosition += velocity;
    }
}
