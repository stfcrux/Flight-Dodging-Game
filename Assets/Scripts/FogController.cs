using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogController : MonoBehaviour {

    public GameObject player;
    public float viewMargin;

    void Start()
    {
        // becuase the fireball is a prefab we will need to set the player
        // ourselves
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    void Update()
    {
        if (player.transform.position.z >
this.transform.position.z + viewMargin)
        {
            Destroy(this.gameObject);
        }
    }
}
