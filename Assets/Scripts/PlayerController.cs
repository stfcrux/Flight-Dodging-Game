using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float difficulty;
    public float rotationIncrement;
    public float rotationRatio;
    public float maxRotation;
    public float startingSpeed;
    public float upIncrement;

    private float minSpeed;
    private float speed;

	// Use this for initialization
	void Start () {
        minSpeed = startingSpeed;
    }

    // Update is called once per frame
    void Update () {
        // increment the position forward based on speed
        transform.Translate(Vector3.forward * speed,
                            Space.World);

        // change the rotation
        if (Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z < maxRotation) {
            transform.Rotate(Vector3.forward * Time.deltaTime * rotationIncrement);
        }
        if (Input.GetKey(KeyCode.RightArrow) && transform.rotation.z > -maxRotation) {
            transform.Rotate(Vector3.forward * Time.deltaTime * -rotationIncrement);
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            speed += upIncrement;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            speed -= upIncrement;
        }

        speed = speed < minSpeed ? minSpeed : speed;
        minSpeed += difficulty * Time.deltaTime;

        // move left/right based on rotation (like a plane does)
        transform.Translate(Vector3.left * transform.rotation.z * rotationRatio
                            * Time.deltaTime, Space.World);
	}
}
