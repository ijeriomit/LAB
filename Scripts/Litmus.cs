using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Litmus : MonoBehaviour {
    public GameObject LitmusPaper, player;
    public Material acidMat, basicMat;
    private float timeStamp, currentTime;
    bool flip = false;
    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(gameObject.name == "Acid")
            {
                //print("ACID");
                print(acidMat.name);
                LitmusPaper.GetComponent<MeshRenderer>().sharedMaterial = acidMat;
                //if(acidMat == null)
               // print("ACID");
            }
            else if(gameObject.name == "Base")
            {
                LitmusPaper.GetComponent<Renderer>().sharedMaterial = basicMat;
                if(basicMat == null)
                print("BASE");
            }
			else if(gameObject.name == "Spill"){
				//LitmusPaper.GetComponent<Renderer>().sharedMaterial = acidMat;
				print("ACID");
			}
        }
    }
    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
       // LitmusPaper = player.GetComponent<Inventory>().pLitmus;
        if (LitmusPaper == null)
            print("Check Litmus");
        //plainMat = GetComponent<Material>();
        //acidMat = (Material)Resources.Load("LitmusAcid", typeof(Material));
        // basicMat = (Material)Resources.Load("LitmusBase", typeof(Material));
        currentTime = Time.time;
        timeStamp = currentTime;
	}
	
	// Update is called once per frame
	void Update () {
       
	}
}
