using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialInteraction : MonoBehaviour {
    public GameObject Flask;
    public GameObject Spill;
    public GameObject Base;
    public ParticleSystem particles;
    private BoxCollider AcidBurn;
    private PlayerInfo player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").GetComponent<PlayerInfo>();
        Flask = GameObject.Find("AcidFlask");
        Spill = GameObject.Find("Spill");
        Base = GameObject.Find("Base");
        particles = GetComponentInChildren<ParticleSystem>();
        AcidBurn = GetComponent<BoxCollider>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
            player.Acidburn.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {

        if (other.name == "Base")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                particles.Stop();
                Spill.SetActive(false);
            }
        }
    }
}
