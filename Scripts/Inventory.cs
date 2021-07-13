using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public struct InventorySlot
    {
        bool isAvailible;
        int slotNum;
        GameObject slot;
        string inSlot;
        
        public void setData(bool a, int num, GameObject obj)
        {
            isAvailible = a;
            slotNum = num;
            slot = obj;
        }
        public void setSlotAvailibility(bool flip)
        {
            isAvailible = flip;
        }
        public void setinSlot(string obj)
        {
            inSlot = obj;
        }
        public GameObject getSlot()
        {
            return slot;
        }
        public string getinSlot()
        {
            return inSlot;
        }
        public int getSlotNum()
        {
            return slotNum;
        }
        public bool isSlotAvailible()
        {
            return isAvailible;
        }
        
    }

    public Vector3[] pos;
    private const int numSlots = 3;
    public InventorySlot[] slots;
    public GameObject pTongs, pPitcher, pHands,player,pLitmus,inventory;
    ParticleSystem lhandfire, rhandfire;
    private float timeStamp, currentTime;
    bool invON;

    // Use this for initialization
    void Start()
    {
        pTongs = GameObject.Find("PlayerTong");
        pPitcher = GameObject.Find("PlayerPitcher");
        pLitmus = GameObject.Find("PlayerLitmus");
        pHands = GameObject.Find("Hands");
        player = GameObject.Find("Player");
        lhandfire = GameObject.Find("burnLHand").GetComponent<ParticleSystem>();
        rhandfire = GameObject.Find("burnRHand").GetComponent<ParticleSystem>();
        inventory = GameObject.Find("InventorySlot");

        if (pTongs == null || pPitcher == null || pHands == null)
            print("Check Inventory");

        slots = new InventorySlot[numSlots];
        slots[0].setData(false, 0, GameObject.Find("InvSlot0"));
        for (int i = 1; i < numSlots; i++)
        {
            slots[i].setData(true, i, GameObject.Find("InvSlot" + i.ToString()));
        }
        slots[0].setinSlot("Hands");
        pTongs.SetActive(false);
        pPitcher.SetActive(false);
        pLitmus.SetActive(false);

       // inventory.SetActive(false);
        invON = false;

        pos = new Vector3[numSlots];
        pos[0] = Vector3.zero;
        pos[1] = Vector3.zero;
        pos[2] = Vector3.zero;
        currentTime = Time.time;
        timeStamp = currentTime + 5f;
    }
    public void showInventory()
    {
        inventory.SetActive(true);
        timeStamp = currentTime + 5f;
    }
    public int getNumSlots()
    {
        int num = numSlots;
        return num;
    }
    public bool InventoryFull()
    {
        for (int i = 0; i < numSlots; i++)
        {
            if(slots[i].isSlotAvailible())
            {
                return false;
            }
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        pos[0] = slots[0].getSlot().GetComponent<RectTransform>().position;
        pos[1] = slots[1].getSlot().GetComponent<RectTransform>().position;
        pos[2] = slots[2].getSlot().GetComponent<RectTransform>().position;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ToggleInventory(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ToggleInventory(1);
        }
       if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ToggleInventory(2);
        }
        currentTime = Time.time;
        if(Input.GetKeyDown(KeyCode.I))
        {
            showInventory();
        }
        if (inventory.activeSelf)
        {
            if (currentTime >= timeStamp)
                inventory.SetActive(false);
        }
    }
    void ToggleInventory(int i)
    {
        if(i == 0)
        {
            player.GetComponent<PlayerInfo>().hasTongs = false;
            player.GetComponent<PlayerInfo>().hasPitcher = false;
            player.GetComponent<PlayerInfo>().hasLitmus = false;
            SwitchObj(true);
            lhandfire.Stop();
            rhandfire.Stop();
        }
        else if(slots[i].getinSlot() == pTongs.gameObject.name) //"PlayerTong")
        {
            player.GetComponent<PlayerInfo>().hasTongs = true;
            player.GetComponent<PlayerInfo>().hasPitcher = false;
            player.GetComponent<PlayerInfo>().hasLitmus = false;
            SwitchObj(false);
        }
        else if(slots[i].getinSlot() == pPitcher.gameObject.name) // "PlayerPitcher")
        {
            player.GetComponent<PlayerInfo>().hasTongs = false;
            player.GetComponent<PlayerInfo>().hasPitcher = true;
            player.GetComponent<PlayerInfo>().hasLitmus = false;
            SwitchObj(false);
        }
        else if(slots[i].getinSlot() == pLitmus.gameObject.name) //"PlayerLitmus")
        {
            player.GetComponent<PlayerInfo>().hasTongs = false;
            player.GetComponent<PlayerInfo>().hasPitcher = false;
            player.GetComponent<PlayerInfo>().hasLitmus = true;
            SwitchObj(false);
        }

    }
    void SwitchObj(bool hands)
    {
        pTongs.SetActive(player.GetComponent<PlayerInfo>().hasTongs);
        pPitcher.SetActive(player.GetComponent<PlayerInfo>().hasPitcher);
        pLitmus.SetActive(player.GetComponent<PlayerInfo>().hasLitmus);
        pHands.SetActive(hands);

    }
}