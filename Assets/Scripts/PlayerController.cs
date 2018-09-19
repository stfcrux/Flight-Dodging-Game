using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    public float rotationIncrement;
    public float rotationRatio;
    public float maxRotation;
    public float upIncrement;
    public float outOfBoundsX;

    public UnityEvent gameOverEvent;

    bool stopped = false;

    public float MinSpeed { get; private set; }
    public float Speed { get; private set; }

    private float minAcceleration;

    void Start () {
        minAcceleration = GlobalOptions.difficulty * 0.3f + 0.1f;
        MinSpeed = GlobalOptions.difficulty * 9 + 1;
    } 

    void Update () {
        // only move if the obejct is not stopped
        if (!stopped) {
            // update speed based on input
            if (Input.GetKey(KeyCode.UpArrow)) {
                Speed += upIncrement;
            }
            if (Input.GetKey(KeyCode.DownArrow)) {
                Speed -= upIncrement;
            }

            Speed = Speed < MinSpeed ? MinSpeed : Speed;
            MinSpeed += minAcceleration * Time.deltaTime;

            // increment the position forward based on speed
            transform.Translate(Vector3.forward * Speed * Time.deltaTime,
                                Space.World);

            // update the rotation based on input
            if (Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z < maxRotation) {
                transform.Rotate(Vector3.forward * Time.deltaTime * rotationIncrement);
            }
            if (Input.GetKey(KeyCode.RightArrow) && transform.rotation.z > -maxRotation) {
                transform.Rotate(Vector3.forward * Time.deltaTime * -rotationIncrement);
            }


            // if moving left/right doesn't put us out of bounds move according
            // to rotation
            if ((transform.rotation.z > 0 && transform.position.x > -outOfBoundsX)
                || (transform.rotation.z <= 0 && transform.position.x < outOfBoundsX)) {
                transform.Translate(Vector3.left * transform.rotation.z * rotationRatio
                                        * Time.deltaTime, Space.World);
            }
        }
	}


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("fireball"))
        {
            gameOverEvent.Invoke();
        }
    }

    public void Stop() {
        stopped = true;
    }

}
