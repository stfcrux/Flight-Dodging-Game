using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallController : MonoBehaviour {

    public GameObject playerObject;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnCollisionEnter (Collision col)
    {
        if(col.gameObject == playerObject)
        {
            print("heello");
            Destroy(col.gameObject);
        }
    }


}
