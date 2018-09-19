using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    public void StartGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void StartMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void StartOptions()
    {
        SceneManager.LoadScene("Options");
    }
}
