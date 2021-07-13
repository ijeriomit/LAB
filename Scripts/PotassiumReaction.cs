using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotassiumReaction : MonoBehaviour {
    float timeStamp, currentTime;
    GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Potassium"))
        {
            other.gameObject.SetActive(false);
            player.GetComponent<PlayerInfo>().exp1 = true;
            GetComponent<ParticleSystem>().Play();
            timeStamp = currentTime + 1.5f;
        }
        else
        {
            other.gameObject.GetComponent<PlaceObject>().ReturnToIntialPos();
        }
        
    }

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Stop();
        GetComponent<ParticleSystem>().Clear();
        currentTime = Time.time;
        timeStamp = currentTime;
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = Time.time;
        if(currentTime>= timeStamp)
        {
            if(GetComponent<ParticleSystem>().isPlaying)
            {
                GetComponent<ParticleSystem>().Stop();
            }
        }
	}
}
