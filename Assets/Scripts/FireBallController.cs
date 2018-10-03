using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireBallController : MonoBehaviour {

    public GameObject player;
    public UnityEvent gameOver;

    readonly float VIEW_MARGIN = 1;
    void Start () {
        // becuase the fireball is a prefab we will need to set the player
        // ourselves
        if (player == null) {
            player = GameObject.Find("Player");
        }
    }

    void Update () {
        // if no longer in view (with some margin to be safe) destroy self
        if (player.transform.position.z >
                this.transform.position.z + VIEW_MARGIN) {
            Destroy(this.gameObject);
        }
	}

}
