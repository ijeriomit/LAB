using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcDistance : MonoBehaviour {

    private GameObject ObjofInterest;
    private Vector3 pos;
    public float maxDistance = 3.5f;
    // Use this for initialization
    void Start () {
        ObjofInterest = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
	}

    public bool inRangeofPlayer(float newDistance,GameObject obj1)
    {
        return (GetDistanceFromPlayer(obj1) <= newDistance);
    }
    public bool inRangeofPlayer(GameObject obj1)
    {
        return (GetDistanceFromPlayer(obj1) <= maxDistance);
    }

    public bool inRangeofTwoObjects(GameObject obj1, GameObject obj2)
    {
        return (GetDistanceBetweenTwoObjects(obj1, obj2) <= maxDistance);
    }
    public bool inRangeofTwoObjects(GameObject obj1, GameObject obj2,float newDistance)
    {
        return (GetDistanceBetweenTwoObjects(obj1, obj2) <= newDistance);
    }

    public float GetDistanceBetweenTwoObjects(GameObject obj1, GameObject obj2)
    {
        return Vector3.Distance(obj1.transform.position,obj2.transform.position);
    }
    public float GetDistanceFromPlayer(GameObject obj1)
    {
        return Vector3.Distance(obj1.transform.position, pos);
    }

    public Vector3 GetPlayerPosition()
    {
        return pos;
    }

    public void setObject(GameObject obj)
    {
        ObjofInterest = obj;
    }

    void FixedUpdate()
    {
        pos = ObjofInterest.transform.position;
        //print(playerpos);
    }
}
