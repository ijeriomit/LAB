using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class ScaleWeight : MonoBehaviour
{
    private GameObject scaleData;
    private GameObject scale;
    ///private FirstPersonController freeze;
    private CalcDistance dist;
    private Text text;
    private GameObject player;
    private bool flip, virgin = true, virgin2 = true;
    
    private void OnMouseOver()
    {
        if (dist.inRangeofPlayer(gameObject))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                print("scale weuight");
                if (virgin)
                {
                    player.GetComponent<PlayerInfo>().interactScale = true;
                    virgin = true;
                }
                DataPopUp();
            }
           
        }
    }
    private void OnMouseExit()
    {
        scaleData.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(virgin2&&!virgin)
            player.GetComponent<PlayerInfo>().usedScale = true;
        text.text = other.gameObject.GetComponent<Metals>().metal.ToString() + " g";
        print("other.gameObject.GetComponent<Metals>().metal.ToString()");

    }
    private void OnTriggerExit(Collider other)
    {
        text.text = "-------";
    }
    // Use this for initialization
    void Start()
    {
        flip = false;
      //  freeze = GetComponent<FirstPersonController>();
        scaleData = GameObject.Find("ScaleInfo");
        //scale = GameObject.Find("balance");
        dist = GetComponent<CalcDistance>();
        text = GameObject.Find("Weight").GetComponent<Text>();
        scaleData.SetActive(false);
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void DataPopUp()
    {
        if (scaleData.activeSelf.Equals(false))
        {
           // Time.timeScale = 0;
            //freeze.setFreeze(true);
            scaleData.SetActive(true);
        }
        else
        {
           // Time.timeScale = 1;
            scaleData.SetActive(false);
            //freeze.setFreeze(false);
        }
    }
    void Update()
    {
     
   }

}