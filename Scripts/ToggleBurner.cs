using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBurner : MonoBehaviour {

	public ParticleSystem fire;
    private GameObject flame;
    private GameObject list;
    private CalcDistance dist;
    private bool isFireActive;
    private GameObject player;
    BoxCollider[] colliders;
    BoxCollider col;
    //private AudioClip Warning_F;
    private bool virgin = true, virgin2 = true;
    float timeStamp, currentTime;
    private void OnTriggerEnter(Collider other)
    {
        if (col.enabled)
        {
            if(other.gameObject.name.Contains("WaterFlask"))
            {
                other.gameObject.GetComponent<BoilFlask>().Boil();
            }
            if (virgin2)
            {
                player.GetComponent<PlayerInfo>().usedBurner = true;
                virgin2 = false;
            }
            if (!player.GetComponent<PlayerInfo>().hasTongs)
                player.GetComponent<PlayerInfo>().BurnPlayer();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("WaterFlask"))
        {
            print("Exit");
            other.gameObject.GetComponent<BoilFlask>().BoilingTime();
        }
    }
    void OnMouseOver(){
        if (dist.inRangeofPlayer(gameObject))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(virgin)
                {
                    player.GetComponent<PlayerInfo>().interactBurner = true;
                   // if(!player.GetComponent<PlayerInfo>().audio.isPlaying)
                      //  player.GetComponent<PlayerInfo>().GetComponent<AudioSource>().PlayOneShot(Warning_F);
                    virgin = false;
                }
                if (isFireActive)
                {
                    col.enabled = false;
                    fire.Stop();
                    isFireActive = false;
                    flame.SetActive(false);
                }
                else
                {
                    col.enabled = true;
                    fire.Play();
                    isFireActive = true;
                    flame.SetActive(true);
                    //timeStamp = currentTime + 5f;
                    //checkList();
                    //print("FIRE");
                }
            }
           
        }

	}
	// Use this for initialization
	void Start () {
        colliders = gameObject.GetComponents<BoxCollider>();
        foreach (BoxCollider collider in colliders)
        {
            if (collider.isTrigger)
            {
                col = collider;
            }
        }
        col.enabled = false; 
        player = GameObject.Find("Player");
        fire = GameObject.Find("BunsenFlame").GetComponent<ParticleSystem>();
        flame = gameObject.transform.Find("flame").gameObject;
        list = GameObject.Find("Check 4");
		isFireActive = false;
        flame.SetActive(false);
		fire.Stop ();
        //list.SetActive(false);
        dist = GetComponent<CalcDistance>();
        currentTime = Time.time;
        timeStamp = currentTime;
	}
	/*void checkList()
    {
        list.SetActive(true);
    }*/
	// Update is called once per frame
	void Update () {
       /* if (currentTime >= timeStamp)
        {
            fire.Stop();
            flame.SetActive(false);
        }*/
	}
}
