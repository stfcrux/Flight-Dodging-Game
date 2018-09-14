using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireBallController : MonoBehaviour {

    public GameObject player;
    public UnityEvent gameOver;

	// Use this for initialization
    void Start () {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update () {
        if (player.transform.position.z > this.transform.position.z) {
            Destroy(this.gameObject);
        }
	}

}
