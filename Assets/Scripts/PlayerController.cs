using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    public float difficulty;
    public float rotationIncrement;
    public float rotationRatio;
    public float maxRotation;
    public float startingSpeed;
    public float upIncrement;
    public UnityEvent gameOverEvent;

    private float minSpeed;
    private float speed;
    private bool stopped = false;

    public float MinSpeed
    {
        get
        {
            return minSpeed;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
    }

    // Use this for initialization
    void Start () {
        minSpeed = startingSpeed;
        speed = startingSpeed;
    }

    // Update is called once per frame
    void Update () {
        if (!stopped)
        {
            // increment the position forward based on speed
            transform.Translate(Vector3.forward * Speed * Time.deltaTime,
                                Space.World);

            // change the rotation
            if (Input.GetKey(KeyCode.LeftArrow) && transform.rotation.z < maxRotation)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * rotationIncrement);
            }
            if (Input.GetKey(KeyCode.RightArrow) && transform.rotation.z > -maxRotation)
            {
                transform.Rotate(Vector3.forward * Time.deltaTime * -rotationIncrement);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                speed += upIncrement;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                speed -= upIncrement;
            }

            speed = Speed < MinSpeed ? MinSpeed : Speed;
            minSpeed += difficulty * Time.deltaTime;

            // if moving left/right doesn't put us out of bounds do so
            if ((transform.rotation.z > 0 && transform.position.x > -19.5)
                || (transform.rotation.z <= 0 && transform.position.x < 19.5)) {
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
