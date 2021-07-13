using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsPopup : MonoBehaviour {
    public GameObject controls;
    private bool controlsOn;
	// Use this for initialization
	void Start () {
        controlsOn = true;
        controls = GameObject.Find("ControlsPopout");
        controls.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (controlsOn)
            {
                controls.SetActive(false);
                controlsOn = false;
            }
            else
            {
                controls.SetActive(true);
                controlsOn = true;
            }
        }
		
	}
}
