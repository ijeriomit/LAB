using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metals : MonoBehaviour {
    private double potassium, copper, iron, lithium, sodium;
    public double metal;
    
	// Use this for initialization
	void Start () {
        potassium = 39.0983;
        sodium = 22.990;
        lithium = 6.941;
        iron = 55.933;
        copper = 63.546;
        if (gameObject.name == "Potassium")
            metal = potassium;
        else if (gameObject.name == "Sodium")
            metal = sodium;
        else if (gameObject.name == "Copper")
            metal = copper;
        else if (gameObject.name == "Iron")
            metal = iron;
        else if (gameObject.name == "Lithium")
            metal = lithium;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
