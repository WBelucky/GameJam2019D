using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScoreInText : MonoBehaviour
{
    private Text scoreText;
    private int additionalTimeScore;
    private int additionalHpScore;
    private int totalScore;
    // Start is called before the first frame update
    void Start()
    {
        scoreText = GetComponent<Text>();
         additionalTimeScore = (int)((1 - Clock.instance.totalTime / Clock.instance.limitTime) * 10000);
         additionalHpScore = (int)((Player.Instance.HP / Player.Instance.maxHP)* 5000);
        totalScore = ScoreManager.score + additionalHpScore + additionalTimeScore;
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text =
            $"SCORE:                 {ScoreManager.score}pt" +
          $"\nTIME:{Clock.instance.totalTime : #.##}s       +{additionalTimeScore}pt" +
          $"\nAWAIKING Lv:{(Player.Instance.HP / Player.Instance.maxHP * 100 ):#.#}%  +{additionalHpScore}pt"  +
      $"\n\n\nTOTAL SCORE:        {totalScore}pt" +
        "\n\nTHANKS FOR PLAYING !";
        
        if(transform.position.y <= 120) transform.position += Vector3.up * Time.deltaTime * 120 * 0.8f;
    }
}
