using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoilFlask : MonoBehaviour {

    float timeStamp, currentTime;
    bool boil = false;
    // Use this for initialization
    void Start()
    {
        currentTime = Time.time;
        timeStamp = currentTime;
        gameObject.GetComponent<ParticleSystem>().Stop();
        gameObject.GetComponent<ParticleSystem>().Clear();
    }

    public void Boil()
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        boil = true;
    }
    public void BoilingTime()
    {
        timeStamp = currentTime + 5f;
        boil = false;
    }
    // Update is called once per frame
    void Update()
    {
        currentTime = Time.time;
        if (currentTime >= timeStamp&&!boil)
        {
           // print("NoStop");
            if (gameObject.GetComponent<ParticleSystem>().isPlaying)
            {
                gameObject.GetComponent<ParticleSystem>().Stop();
            }
           // boil = false;
        }
    }
}