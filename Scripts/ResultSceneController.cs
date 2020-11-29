using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneController : MonoBehaviour
{
    private int[] results = new int[5];
    public Text Score;
    public Text ScoresTable;
    // Start is called before the first frame update
    void Start()
    {
        results = LevelController.LC.GetScores();
        int perfectCount = results[0];
        int goodCount = results[1];
        int normalCount = results[2];
        int missedCount = results[3];
        int totalScore = results[4];
        Transform StarHolder = transform.Find("StarHolder");
        Score.text = "Score :" + totalScore;
        ScoresTable.text = "Perfect: " + perfectCount + "\nGood: " + goodCount + "\nNormal: " + normalCount + "\nMissed: " + missedCount;
        ActivateStar(StarHolder, totalScore);
    }
    private void ActivateStar(Transform StarHolder, int totalScore)
    {
        if (totalScore > 350000)
        {
            StarHolder.GetChild(0)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
        if(totalScore > 500000)
        {
            StarHolder.GetChild(1)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
        if(totalScore > 700000)
        {
            StarHolder.GetChild(2)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
        if(totalScore > 850000)
        {
            StarHolder.GetChild(3)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
        if(totalScore > 910000)
        {
            StarHolder.GetChild(4)
                      .GetComponent<SpriteRenderer>().color = new Color(1f, 0.84f, 0f);
        }
    }

}
