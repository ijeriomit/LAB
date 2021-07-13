using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class next_level : MonoBehaviour
{

    public void OnCollisionEnter(Collision info)
    {
        if (info.gameObject.name.Equals("Player"))
        {
            Debug.Log("next");
            Application.LoadLevel("shop");
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}