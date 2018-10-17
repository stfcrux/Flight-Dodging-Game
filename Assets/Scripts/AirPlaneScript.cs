﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirPlaneScript : MonoBehaviour {

    public PointLight pointLight;

    // Use this for initialization
    void Start () {
        // Get renderer component (in order to pass params to shader)
        MeshRenderer renderer = this.gameObject.GetComponent<MeshRenderer>();

        // Pass updated light positions to shader
        renderer.material.SetColor("_PointLightColor", this.pointLight.color);
        renderer.material.SetVector("_PointLightPosition", this.pointLight.GetWorldPosition());
    }

    // Update is called once per frame
    void Update () {

    }
}
