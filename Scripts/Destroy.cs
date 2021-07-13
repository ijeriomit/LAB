using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

	BoxCollider col;
	Rigidbody rb;
    GameObject [] pieces = new GameObject[10];
    GameObject innerFlask;
	// Use this for initialization
	void OnCollisionEnter(Collision obcol){
		if(obcol.gameObject.tag == "Floor"){
            if (!innerFlask.name.Contains("InnerFlask"))
              print("oops");
            innerFlask.SetActive(false);
            for (int i = 0; i < 10; i++)
            {
                pieces[i].SetActive(true);
            }
            print ("FLOOOOOR");
			//transform.parent = null;
            foreach (GameObject i in pieces) 
            {
                i.GetComponent<Rigidbody>().isKinematic = false;
                i.GetComponent<BoxCollider>().isTrigger = false;
            }
            transform.DetachChildren();
            //col.isTrigger = false;
            //rb.isKinematic = false;
            gameObject.SetActive(false);

		}
	}
	void Start () {
		//rb = GetComponent<Rigidbody> ();
		//col = GetComponent<BoxCollider> ();
        innerFlask = gameObject.transform.GetChild(0).gameObject;
        for (int i = 0; i < 10; i++)
        {
            pieces[i] = gameObject.transform.GetChild(i+1).gameObject;
        }
        for(int i = 0; i < 10; i++)
        {
            pieces[i].SetActive(false);
        }
       
    }
	
	// Update is called once per frame
	void Update () {
		/*if (transform.parent == null) {
			//transform.parent = null;
			col.isTrigger = false;
			rb.isKinematic = false;
		}
		/*if (rb.velocity.x > 0) {
			print ("AAAAAH");
			transform.parent = null;
			col.isTrigger = false;
			rb.isKinematic = false;
		}*/
	}
}
