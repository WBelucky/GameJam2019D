using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    // Start is called before the first frame update
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = Player.Instance.HP / Player.Instance.maxHP;
        if (Player.Instance.HP < Player.Instance.maxHP / 2) 
        {
            image.color = Color.red;
        }
    }
}
