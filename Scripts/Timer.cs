using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {

    public Text Text;
    public float theTime, speed = 1; 
    private PlayerInfo info; 

	void Start () {
        Text = GetComponent<Text>();
        info = GameObject.Find("Player").GetComponent<PlayerInfo>();

	}
	
	void Update () {
        theTime += Time.deltaTime * speed;
        string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
        string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
        string seconds = (theTime % 60).ToString("00");
        Text.text = hours + ":" + minutes + ":" + seconds;
        if (info.isBurning)
        {
            seconds += 10f;
        }
    }
}
