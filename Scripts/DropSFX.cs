using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSFX : MonoBehaviour {
    public AudioSource dropsfx;
	// Use this for initialization
	void Start () {
        
        dropsfx = GetComponent<AudioSource>();
        dropsfx.playOnAwake = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.name == "Floor")
        //{
            dropsfx.Play();
        //}
    }
}
