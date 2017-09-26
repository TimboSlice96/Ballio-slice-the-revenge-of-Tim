using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinball : MonoBehaviour
{

    bool touching;

    private void Start()
    {
        touching = false;
    }

    //On colliding with the player set the touching flag to true
	private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            touching = true;
        }
    }

    //When we stop colliding with the player set the touching flag to false
	private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            touching = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {

        //if the pick up is RED and the marble is touching the player via the touching flag, then pick up the red collectable and add 1 to the Red pickup count.
		if ((other.gameObject.CompareTag("Pickup Red")) && (touching == true))
        {
			PlayerManager.Get ().stats.Red += 1;
			other.gameObject.SetActive(false);
		}

    }
}
