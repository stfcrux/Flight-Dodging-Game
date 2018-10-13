using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour {

    public void StartEasyGameScene()
    {
        ExplosionCreator.explosionCreated = false;
        SceneManager.LoadScene("EasyGameScene");
    }

    public void StartMediumGameScene()
    {
        ExplosionCreator.explosionCreated = false;
        SceneManager.LoadScene("MediumGameScene");
    }

    public void StartHardGameScene()
    {
        ExplosionCreator.explosionCreated = false;
        SceneManager.LoadScene("HardGameScene");
    }

    public void StartToonGameScene()
    {
        ExplosionCreator.explosionCreated = false;
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
