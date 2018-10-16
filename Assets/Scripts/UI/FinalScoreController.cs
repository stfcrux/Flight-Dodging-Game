﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreController : MonoBehaviour {

    public PlayerController player;

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = "Your Final Score is:" + (player.transform.position.z * (GlobalOptions.difficulty + 1)).ToString("0");
    }
}
