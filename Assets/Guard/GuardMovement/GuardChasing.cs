using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardChasing : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GuardMovement chasing = gameObject.GetComponentInParent<GuardMovement>();

        if (collision.gameObject.tag == ("Player"))
        {
            chasing.GuardChasing();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GuardMovement chasing = gameObject.GetComponentInParent<GuardMovement>();
        
    }
}
