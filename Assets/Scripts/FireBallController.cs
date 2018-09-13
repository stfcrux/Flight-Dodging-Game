using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

    public GameObject player;

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

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject == player)
        {
            Destroy(col.gameObject);
        }
    }


}
