using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    public GameObject player;

    void Update () {

        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = renderer.materials[0];
        Color col = renderer.material.GetColor("_Color");
        col.a = Mathf.Abs(player.transform.position.x - transform.position.x) / 20;
        material.SetColor("_Color", col);
    }
}

