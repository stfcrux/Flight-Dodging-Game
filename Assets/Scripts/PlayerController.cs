using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float difficulty;
    public float rotationIncrement;
    public float rotationRatio;
    public float maxRotation;

    private float forwardSpeed = 1.0f;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        // increment the position forward based on speed
        forwardSpeed += difficulty;
        transform.Translate(Vector3.forward * Time.deltaTime * forwardSpeed,
                            Space.World);

        // change the rotation
        if (Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z < maxRotation) {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationIncrement);
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.rotation.z > -maxRotation) {
            transform.Rotate(Vector3.forward * Time.deltaTime * -rotationIncrement);
        }

        // move left/right based on rotation (like a plane does)
        transform.Translate(Vector3.left * transform.rotation.z * rotationRatio
                            * Time.deltaTime, Space.World);
	}
}
