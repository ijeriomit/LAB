using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuTemplate : MonoBehaviour{
    public GameObject pausePanel;
    public GameObject characterBody;
    public FirstPersonController freeze;

    // Use this for initialization
    void Start()
    {
         freeze = GetComponent<FirstPersonController>();
        pausePanel.SetActive(false);
    }

    // Update is called once per frame

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                freeze.setFreeze(true);
                // characterBody.SetActive(false);
            }
            else if (pausePanel.activeInHierarchy)
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                freeze.setFreeze(false);
                //characterBody.SetActive(true);
            }
        }
    }
}