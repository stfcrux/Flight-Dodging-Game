using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    public GameObject player;
    public float showDistance;

	void Update () {
        // only show the wall if the player is within showDistance of it
        GetComponent<Renderer>().enabled =
            Mathf.Abs(player.transform.position.x - transform.position.x) < showDistance;
	}
}

