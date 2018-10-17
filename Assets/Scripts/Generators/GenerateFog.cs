using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFog : MonoBehaviour
{

    // the fireball prefab
    public GameObject fog;
    public GameObject player;
    public float rowDistanceApart;
    public float y;
    public float rowsShowing;

    float nextRowZ = 10;

    void Start()
    {
        // gen the number of rows that should be showing
        for (int i = 0; i < rowsShowing; i++)
        {
            GenNextRow();
        }
    }

    private void Update()
    {
        // generate a new row if we have moved rowDistanceApart since generating
        // the last one
        if (player != null)
        {
            if (player.transform.position.z + rowDistanceApart * rowsShowing
                    > nextRowZ)
            {
                GenNextRow();
            }
        }
    }

    void GenNextRow()
    {
        Instantiate(fog,
                    new Vector3(0, y, nextRowZ),
                    Quaternion.identity);
        nextRowZ += rowDistanceApart;
    }
}