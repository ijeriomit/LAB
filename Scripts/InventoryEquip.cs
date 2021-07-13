using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEquip : MonoBehaviour {
	GameObject icon;
    Inventory invStatus;
    string objName;
    //GameObject obj;
    GameObject player;
    bool virgin = true;
    private CalcDistance dist;
	//public GameObject equippable;
	void OnMouseOver(){
        if (Input.GetKeyDown(KeyCode.E) && (!invStatus.InventoryFull()))
            if (dist.inRangeofPlayer(gameObject))
            {
                if (virgin)
                {
                    if (gameObject.name.Contains("Tong"))
                    {
                        print("Equip Tongs");
                        player.GetComponent<PlayerInfo>().equipTongs = true;
                        virgin = false;
                    }
                    else if (gameObject.name.Contains("Litmus"))
                    {
                        player.GetComponent<PlayerInfo>().equipLitmus = true;
                        virgin = false;
                    }
                    else if (gameObject.name.Contains("WaterPitcher"))
                    {
                        player.GetComponent<PlayerInfo>().equipPitcher = true;
                        virgin = false;
                    }
                }
                Equip();
            }
                
    
                
	}
	// Use this for initialization
	void Start () {
        dist = GetComponent<CalcDistance>();
		invStatus = GameObject.Find("Player").GetComponent<Inventory>();
        player = GameObject.Find("Player");
        if (gameObject.name.Contains("Tong"))
        {
            icon = GameObject.Find("Tongs Icon");
            objName = "PlayerTong";
        }
        else if (gameObject.name.Contains("WaterPitcher"))
        {
            icon = GameObject.Find("Pitcher Icon");
            objName = "PlayerPitcher";
        }
        else if (gameObject.name.Contains("Litmus"))
        {
            icon = GameObject.Find("Litmus Icon");
            objName = "PlayerLitmus";
        }
        if (icon == null)
            print("CheckInventoryEquip");
		if (icon.activeSelf) {
			icon.SetActive (false);
		}
        //invStatus.inventory.SetActive(false);
        //obj = GameObject.Find(objName);
    }
	
	// Update is called once per frame
	void Update () {

	}
	void Equip(){
        invStatus.showInventory();
        icon.SetActive (true);
        //GameObject obj = GameObject.Find(objName);
       // obj.SetActive(true);
        for(int i=1; i < invStatus.getNumSlots();i++)
        {
            if(invStatus.slots[i].isSlotAvailible())
            {
                invStatus.slots[i].setSlotAvailibility(false);
                icon.GetComponent<RectTransform>().position = invStatus.pos[i];
                invStatus.slots[i].setinSlot(objName);
                break;
            }
        }
        //Destroy (gameObject);
        gameObject.SetActive(false);
	}
}
