using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFireBalls : MonoBehaviour {

    // the fireball prefab
    public GameObject fireball;
    public GameObject player;
    public float objectPerRow;
    public float rowDistanceApart;
    public float y;
    public float minX;
    public float maxX;
    public float rowsShowing;

    float nextRowZ = 10;

	void Start () {
        // gen the number of rows that should be showing
        for (int i = 0; i < rowsShowing; i++) {
            GenNextRow();
        }
	}

    private void Update()
    {
        // generate a new row if we have moved rowDistanceApart since generating
        // the last one
        if (player.transform.position.z + rowDistanceApart * rowsShowing
                > nextRowZ) {
            GenNextRow();
        }
    }

    void GenNewFireBalls(float z) {
        // generate objectPerRows random x fireballs
        for (int i = 0; i < objectPerRow; i++) {
            Instantiate(fireball,
                        new Vector3(Random.Range(minX, maxX),y,z),
                        Quaternion.identity);
        }
    }

    void GenNextRow() {
        GenNewFireBalls(nextRowZ);
        nextRowZ += rowDistanceApart;
    }
}