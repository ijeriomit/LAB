using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equip : MonoBehaviour {
	private GameObject icon;
    //public GameObject list;
    private GameObject rHand;
    private GameObject lHand;
    public Material gloveMat;
    public Material skinMat;
    private CalcDistance dist;
    private GameObject player;
    bool virgin = true;
    void OnMouseOver()
    {
        if (dist.inRangeofPlayer(gameObject))
            if (Input.GetKey(KeyCode.E))
            {
                EquipIcon();
                if (virgin)
                {
                    if (gameObject.name.Equals("Gloves"))
                        player.GetComponent<PlayerInfo>().equipGloves = true;
                    else if (gameObject.name.Equals("SafetyGlasses"))
                        player.GetComponent<PlayerInfo>().equipGlasses = true;
                    else if (gameObject.name.Equals("LabCoat"))
                        player.GetComponent<PlayerInfo>().equipCoat = true;
                    virgin = false;
                }

            }
                
    }
    public GameObject getIcon()
    {
        return icon;
    }
    public void EquipIcon()
    {
        if (gameObject.name.Equals("Gloves"))
            EquipHands();
        icon.SetActive(true);
        //list.SetActive(true);
        gameObject.SetActive(false);

    }
    public void UnEquipIcon()
    {
        if (gameObject.name.Equals("Gloves"))
            UnEquipHands();
        gameObject.SetActive(true);
        icon.SetActive(false);
        gameObject.SetActive(false);
        // rHand.SetActive(false);
    }
    public void EquipHands()
    {
        rHand.GetComponent<SkinnedMeshRenderer>().material = gloveMat;
        lHand.GetComponent<SkinnedMeshRenderer>().material = gloveMat;
        //list.SetActive(true);
    }
    public void UnEquipHands()
    {
        rHand.GetComponent<SkinnedMeshRenderer>().material = skinMat;
        lHand.GetComponent<SkinnedMeshRenderer>().material = skinMat;
        //hands.SetActive(false);
        //list_1.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        dist = GetComponent<CalcDistance>();
        rHand = GameObject.Find("rightHand");
        lHand = GameObject.Find("leftHand");
        if (gameObject.name.Equals("Gloves"))
        {
            icon = GameObject.Find("GlovesIcon");
            //list.SetActive(true);    
        }
        else if (gameObject.name.Equals("SafetyGlasses"))
        {
            icon = GameObject.Find("GlassesIcon");
            //list.SetActive(true);
          
        }
        else if (gameObject.name.Equals("LabCoat"))
        {
            icon = GameObject.Find("CoatIcon");
           // list.SetActive(true);
           
        }
        icon.SetActive(false);
        //list.SetActive(false);
        player = GameObject.Find("Player");
    }
	// Update is called once per frame
	void Update () {
        
    }
}
