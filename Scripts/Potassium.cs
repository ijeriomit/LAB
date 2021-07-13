using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potassium : MonoBehaviour {
    private ParticleSystem fire;
    private GameObject flame;
    public GameObject Icon;
    private CalcDistance dist;
    private bool isFireActive;
    void OnMouseOver()
    {
      /*  if (dist.inRangeofPlayer(gameObject))
        {*/
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isFireActive)
                {
                    fire.Stop();
                    isFireActive = false;
                    //flame.SetActive(false);
                }
                else
                {
                    fire.Play();
                    isFireActive = true;
                   // flame.SetActive(true);
                   // checkList();
                    //print("FIRE");
                }
            }
        //}

    }
    // Use this for initialization
    void Start()
    {
        fire = GetComponentInChildren<ParticleSystem>();
        //flame = gameObject.transform.Find("flame").gameObject;
       // list = GameObject.Find("Check 4");
        isFireActive = false;
       // flame.SetActive(false);
        fire.Stop();
       // list.SetActive(false);
        dist = GetComponent<CalcDistance>();
    }
   /* void checkList()
    {
        list.SetActive(true);
    }*/
    // Update is called once per frame
    void Update()
    {

    }
}
