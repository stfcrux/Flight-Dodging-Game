using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreController : MonoBehaviour
{

    public PlayerController player;
    public enum Level { TOON, EASY, MEDIUM, HARD };
    public Level level;


    Text text;
    float previousBest = 0;
    float currentScore = 0;

    void Start()
    {
        text = GetComponent<Text>();

        switch (level)
        {
            case (Level.TOON): previousBest = GlobalOptions.previousBestScoreToon; break;
            case (Level.EASY): previousBest = GlobalOptions.previousBestScoreEasy; break;
            case (Level.MEDIUM): previousBest = GlobalOptions.previousBestScoreMedium; break;
            case (Level.HARD): previousBest = GlobalOptions.previousBestScoreHard; break;
        }
    }

    void Update()
    {
        text.text = "Your Final Score is: " + (player.transform.position.z * (GlobalOptions.difficulty + 1)).ToString("0") + "\n" +
            "Your Previous Best Score is: " + previousBest.ToString("0");
        currentScore = (player.transform.position.z * (GlobalOptions.difficulty + 1));

        if (currentScore > previousBest)
        {
            switch (level)
            {
                case (Level.TOON): GlobalOptions.previousBestScoreToon = currentScore; break;
                case (Level.EASY): GlobalOptions.previousBestScoreEasy = currentScore; break;
                case (Level.MEDIUM): GlobalOptions.previousBestScoreMedium = currentScore; break;
                case (Level.HARD): GlobalOptions.previousBestScoreHard = currentScore; break;
            }
        }



    }
}
