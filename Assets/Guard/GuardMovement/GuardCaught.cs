using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCaught : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {

        GuardMovement caught = gameObject.GetComponentInParent<GuardMovement>();
        if (collision.gameObject.tag == ("Player"))
        {
            caught.GuardCuaght();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
