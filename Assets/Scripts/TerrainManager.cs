using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    public GameObject player;

	// Update is called once per frame
	void Update () {
        // if the terrain is no longer in view move it to be in view beyond
        // the other terrain
        if (player.transform.position.z > transform.position.z + 500) {
            transform.Translate(Vector3.forward * 2000);
        }
	}
}
