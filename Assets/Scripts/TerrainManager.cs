using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour {

    public GameObject player;
    public float size;

    readonly float MARGIN_FACTOR = 1.1f;

	void Update () {
        // if the terrain is no longer in view (10% behind it to be safe)
        // move it to be in view beyond the other terrain
        if (player.transform.position.z >
            transform.position.z + size * MARGIN_FACTOR) {
            // both terrains should be the same size so just move it double size
            // forwards
            transform.Translate(Vector3.forward * size * 2);
        }
	}
}
