using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countdown : MonoBehaviour {
    public Text CD;
    public float timeTilDeath;
    private PlayerInfo player;

	// Use this for initialization
	void Start () {
        timeTilDeath = 20f;
        player = GetComponent<PlayerInfo>();
        CD = GetComponent<Text>();
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }
    public void Count()
    {
        timeTilDeath -= Time.deltaTime;
        //print(timeTilDeath);
        //CD.text = timeTilDeath.ToString();
        if (timeTilDeath <= 0)
        {
            player.isAlive = false;
            player.isBurning = false;
            SceneManager.LoadScene("Dead");
        }
    }
}
