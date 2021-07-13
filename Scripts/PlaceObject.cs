using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour {

	private CalcDistance dist;
	private GameObject scale, burner,player, cup;
	private float distToBurner, distToScale, distToCup;
    Vector3 pos;
    
	// Use this for initialization
	void Start () {
		dist = GetComponent<CalcDistance> ();
		burner = GameObject.Find ("burner");
		scale = GameObject.Find ("balance");
        player = GameObject.Find("Player");
        cup = GameObject.Find("WaterPitcher");
        pos = gameObject.transform.position;
	}
	public void ReturnToIntialPos()
    {
        gameObject.transform.position = pos;
    }
	// Update is called once per frame
	void Update () {
		distToScale = dist.GetDistanceBetweenTwoObjects (gameObject, scale);
		distToBurner = dist.GetDistanceBetweenTwoObjects (gameObject, burner);
        distToCup = dist.GetDistanceBetweenTwoObjects(gameObject, cup);
        if (Input.GetKeyDown(KeyCode.F)) {
            if (distToScale > 0 && distToScale < .80f)
            {
                gameObject.transform.position = new Vector3(-7.035f, 1.8745f, -13.532f);
                player.GetComponent<RigidbodyPickUp>().ResetPickUp();
            }
            if (distToBurner > 0 && distToBurner < .80f)
            {
                gameObject.transform.position = new Vector3(-7.21f, 2.2f, 1.464f);
                player.GetComponent<RigidbodyPickUp>().ResetPickUp();
            }
            if (distToCup > 0 && distToCup < .80f)
            {
                gameObject.transform.position = new Vector3(-7.198f, 2f, -9.063f);
                player.GetComponent<RigidbodyPickUp>().ResetPickUp();
            }
        }

	}

}

