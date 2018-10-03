using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    public void StartEasyGameScene()
    {
        SceneManager.LoadScene("EasyGameScene");
    }

    public void StartMediumGameScene()
    {
        SceneManager.LoadScene("MediumGameScene");
    }

    public void StartHardGameScene()
    {
        SceneManager.LoadScene("HardGameScene");
    }

    public void StartToonGameScene()
    {
        SceneManager.LoadScene("ToonGameScene");
    }


    public void StartSelectGameScene()
    {
        SceneManager.LoadScene("LevelSelection");
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
