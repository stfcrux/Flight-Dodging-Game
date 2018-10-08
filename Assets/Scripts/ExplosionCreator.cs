using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCreator : MonoBehaviour {

    public static bool explosionCreated;
    public GameObject explosion;
    public GameObject player;

    public void Explode(){
        if (player && !explosionCreated)
        {
            explosionCreated = true;
            GameObject exp = Instantiate(explosion,
                        player.transform.position,
                        Quaternion.identity);
            Destroy(exp, exp.GetComponent<ParticleSystem>().main.duration);
        }
    }

}
