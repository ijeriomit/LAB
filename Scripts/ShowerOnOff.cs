using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerOnOff : MonoBehaviour {

    private GameObject showerHead;
    private CalcDistance dist;
    private GameObject player;
    private bool virgin = true, showerOn = false;
    private float timeStamp, currentTime;
    // Use this for initialization
    void Start () {
        showerHead = GameObject.Find("ShowerHead");
        player = GameObject.Find("Player");
        showerHead.GetComponent<ParticleSystem>().Stop();
        showerHead.GetComponent<ParticleSystem>().Clear();
        dist = GetComponent<CalcDistance>();
        gameObject.GetComponent<BoxCollider>().enabled = false;
        currentTime = Time.time;
        timeStamp = currentTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name.Equals("Player"))
        {
                player.GetComponent<PlayerInfo>().Extinguish();
        }
    }
    void ShowerOffandON()
    {
        if (showerHead.GetComponent<ParticleSystem>().isStopped)
        {
            showerHead.GetComponent<ParticleSystem>().Play();
             gameObject.GetComponent<BoxCollider>().enabled = true;
            //print("ON");
            //showerOn = true;
            timeStamp = currentTime + 5f;
        }
        else if (showerHead.GetComponent<ParticleSystem>().isPlaying)
        {
           // print("OFF");
            showerHead.GetComponent<ParticleSystem>().Stop();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            showerOn = false;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        currentTime = Time.time;
        if(currentTime>=timeStamp)
        {
            showerHead.GetComponent<ParticleSystem>().Stop();
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //showerOn = false;
            //print("OFF");
        }
		if(dist.inRangeofPlayer(gameObject))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                ShowerOffandON();
                if (virgin)
                    player.GetComponent<PlayerInfo>().usedShower = true;
                    virgin = false;
             }
         }
    }
}
