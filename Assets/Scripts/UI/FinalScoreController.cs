using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreController : MonoBehaviour {

    public PlayerController player;

    Text text;
    float previousBest = 0;
    float currentScore = 0;

    void Start()
    {
        text = GetComponent<Text>();

        previousBest = GlobalOptions.previousBestScore;

    }

    void Update()
    {
        text.text = "Your Final Score is:" + (player.transform.position.z * (GlobalOptions.difficulty + 1)).ToString("0") + "\n" +
            "Your Previous Best Score is:" + previousBest.ToString("0");
        currentScore = (player.transform.position.z * (GlobalOptions.difficulty + 1));

        if (currentScore > previousBest)
        {
            GlobalOptions.previousBestScore = currentScore;
        }



    }
}
