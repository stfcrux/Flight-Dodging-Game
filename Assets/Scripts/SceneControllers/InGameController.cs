using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameController : MonoBehaviour {
    
    public Text scoreText;
    public Text healthText;


    public PlayerController player;

    // A very simple way to keep data persistent between scenes is via
    // a static attribute as below. There are other ways whereby statics
    // can be avoided, but involve a bit more complexity (use of persistent
    // objects between scenes).
    public static bool lastGameWon;
    
    // difficulty is 0 to 1 in the slider
	void Start ()
    {
        player.MinSpeed = 1.0f + (GlobalOptions.difficulty * 20.0f);
    }

    void Update ()
    {


    }

    // Called when the game should be ended
    // Changes the UI accordingly
    public void GameOver()
    {
        InGameController.lastGameWon = false;
        SceneManager.LoadScene("GameEnded");
    }

    public void PlayerWon()
    {
        InGameController.lastGameWon = true;
        SceneManager.LoadScene("GameEnded");
    }
}
