using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public void RestartGame() {
        // restart the game by just reloading the scene
        SceneManager.LoadScene("GameScene");
    }
}
