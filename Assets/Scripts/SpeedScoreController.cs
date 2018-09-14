using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedScoreController : MonoBehaviour {

    public PlayerController player;
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}

    // Update is called once per frame
    void Update()
    {
        text.text = "Speed: " + player.Speed.ToString("0.0") + "\n" +
            "Min Speed: " + player.MinSpeed.ToString("0.0") + "\n" +
            "Score: " + player.transform.position.z.ToString("0");
	}
}
