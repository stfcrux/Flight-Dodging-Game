using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedScoreController : MonoBehaviour {

    public PlayerController player;

    Text text;

	void Start () {
        text = GetComponent<Text>();
	}

    void Update()
    {
        // keep the views updated
        // todo unsure the effect this sort of thing has on framerate
        text.text = "Speed: " + player.Speed.ToString("0.0") + "\n" +
            "Min Speed: " + player.MinSpeed.ToString("0.0") + "\n" +
            "Score: " + player.transform.position.z.ToString("0");
	}
}
