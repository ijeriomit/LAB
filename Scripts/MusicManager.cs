using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {

    // Use this for initialization
    public Slider volume;
    public AudioSource myMusic;
    void Start() {
        myMusic = GetComponent<AudioSource>();
        volume = GameObject.Find("Slider").GetComponent<Slider>();
            }
	// Update is called once per frame
	void Update () {
        myMusic.volume = volume.value;
	}
}
