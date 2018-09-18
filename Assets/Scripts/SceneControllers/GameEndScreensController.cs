using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndScreensController : MonoBehaviour {

    public Text resultText;

    void Start()
    {
        // Display text based on last game result
        if (InGameController.lastGameWon)
        {
            this.resultText.text = "Congrats, You Won!";
        }
        else
        {
            this.resultText.text = "You Lost, Better Luck Next Time!";
        }
    }

    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
