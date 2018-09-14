using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFireBalls : MonoBehaviour {

    public GameObject fireball;
    public GameObject player;
    public float objectPerRow;
    public float rowDistanceApart;
    public float y;
    public float minX;
    public float maxX;
    public float rowsShowing;

    private float nextRow = 10;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < rowsShowing; i++) {
            GenNextRow();
        }
	}

    private void Update()
    {
        if (player.transform.position.z + rowDistanceApart * rowsShowing > nextRow) {
            GenNextRow();
        }
    }

    void GenNewFireBalls(float z) {

        for (int i = 0; i < objectPerRow; i++)
        {
            GameObject fb = Instantiate(fireball,
                        new Vector3(Random.Range(minX, maxX),y,z),
                        Quaternion.identity);
        }
    }

    void GenNextRow() {
        GenNewFireBalls(nextRow);
        nextRow += rowDistanceApart;
    }
}
