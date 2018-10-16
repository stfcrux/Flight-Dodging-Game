using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedScoreController : MonoBehaviour {

    public PlayerController player;

    Text text;
    public float score;

	void Start () {
        text = GetComponent<Text>();
	}

    void Update()
    {
        score = (player.transform.position.z * (GlobalOptions.difficulty + 1));
        // keep the views updated
        text.text = "Speed: " + player.Speed.ToString("0.0") + "\n" +
            "Min Speed: " + player.MinSpeed.ToString("0.0") + "\n" +
            "Score: " + (player.transform.position.z * (GlobalOptions.difficulty + 1)).ToString("0");
	}
}
