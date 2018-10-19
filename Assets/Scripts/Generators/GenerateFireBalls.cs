using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFireBalls : MonoBehaviour {

    // the fireball prefab
    public GameObject fireball;
    public GameObject player;
    public int objectPerRow;
    public float rowDistanceApart;
    public float y;
    public float minX;
    public float maxX;
    public float rowsShowing;

    float nextRowZ = 10;
    Vector3[] toGenerate;
    int currIndex;

    void Start()
    {
        // gen the number of rows that should be showing
        for (int i = 0; i < rowsShowing; i++)
        {
            GenNextRowNow();
        }
        toGenerate = new Vector3[objectPerRow * 100];
    }

    private void Update()
    {
        // generate a new row if we have moved rowDistanceApart since generating
        // the last one
        if (player.transform.position.z + rowDistanceApart * rowsShowing
                > nextRowZ) {
            GenNextRow();
        }
        // generate one fb per frame
        for (int i = 0; i < 3; i++)
        {
            if (currIndex > 0)
            {
                Instantiate(fireball,
                            toGenerate[currIndex--],
                            Quaternion.identity,
                            this.transform);
            }
        }

    }

    void GenNewFireBalls(float z) {
        // generate objectPerRows random x fireballs
        for (int i = 0; i < objectPerRow; i++) {
            toGenerate[currIndex++] = new Vector3(Random.Range(minX, maxX), y, z);
        }
    }

    void GenNextRow() {
        GenNewFireBalls(nextRowZ);
        nextRowZ += rowDistanceApart;
    }

    void GenNextRowNow() {
        GenNewFireBallsNow(nextRowZ);
        nextRowZ += rowDistanceApart;
    }

    void GenNewFireBallsNow(float z)
    {
        // generate objectPerRows random x fireballs
        for (int i = 0; i < objectPerRow; i++)
        {
            Instantiate(fireball,
                       new Vector3(Random.Range(minX, maxX), y, z),
                        Quaternion.identity,
                        this.transform);
        }
    }

}