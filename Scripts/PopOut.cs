using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOut : MonoBehaviour {

	private GameObject popOut;
	private bool isActive, isVirgin;
    private PlayerInfo player;
	void OnMouseOver(){
		if(Input.GetKeyDown(KeyCode.E)){
            if (gameObject.name.Equals("BookSafety")&&isVirgin)
            {
                player.usedManual = true;
            }
            if(gameObject.name.Equals("ScaleExperiment") && isVirgin)
            {
                player.readScalePop = true;
            }
            if (!isActive) {
				popOut.SetActive (true);
				isActive = true;
			} else {
				popOut.SetActive (false);
				isActive = false;
			}
		}
	}
    private void OnMouseEnter()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (gameObject.name.Equals("BookSafety") && isVirgin)
            {
                player.usedManual = true;
            }
            if (!isActive)
            {
                popOut.SetActive(true);

            }
        }
    }
    private void OnMouseExit()
    {
        if (popOut.activeSelf)
        {
            popOut.SetActive(false);
            isActive = false;
        }
    }
    // Use this for initialization
    void Start () {
		//isActive = false;
		if (gameObject.name.Equals ("Periodic Table")) {
			popOut = GameObject.Find ("PTablePopOut");
            isVirgin = false;
		}
		if (gameObject.name.Equals ("BookSafety")) {
			popOut = GameObject.Find ("LabSafetyPopout");
            player = GameObject.Find("Player").GetComponent<PlayerInfo>();
            isVirgin = true;
		}
		if (gameObject.name.Equals ("ControlsPopout")) {
			popOut = GameObject.Find ("ControlsPopout");
            isVirgin = false;
        }
        if (gameObject.name.Equals("ScaleExperiment"))
        {
            popOut = GameObject.Find("ScaleExpPopOut");
            isVirgin = false;
        }
		popOut.SetActive (false);
		

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isActive) {
				popOut.SetActive (false);
				isActive = false;
			}
		}
	}
}
