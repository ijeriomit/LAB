using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptPopUp: MonoBehaviour {

    public GameObject prompt;
    private CalcDistance dist;
    private Equip equip;
    private GameObject obj;
    private float pDist;
    private float currentTime;
    private float timeStamp;
    private bool flip = true;
    private bool flip2 = false;

    // Use this for initialization
    void Start()
    {
       
        prompt = gameObject.transform.Find("Interaction Prompt").gameObject;
        if(prompt == null)
        {
            print("Hi");
        }
        prompt.SetActive(false);
        dist = GetComponent<CalcDistance>();
        equip = GetComponent<Equip>();
        
        pDist = .3f;
        if(gameObject.name.Equals("LabCoat"))
        {
            pDist = .5f;
        }
        if(gameObject.name.Equals("Periodic Table"))
        {
            pDist = 1f;
        }
        currentTime = Time.time;
        timeStamp = currentTime;
    }
    private void OnMouseOver()
    {
        if (dist.inRangeofPlayer(gameObject))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                prompt.SetActive(false);
            }
        }
    }
    private void OnMouseEnter()
    {
        //dist.setObject(GameObject.Find("Player"));
        if (dist.inRangeofPlayer(gameObject))
        {
            if (prompt.activeSelf == false && flip)
                ActivatePrompt();
            flip2 = true;
        }
        flip = false;
    }
    private void OnMouseExit()
    {
        flip = true;
        flip2 = false;
    }
    public void ActivatePrompt()
    {
        Vector3 temp;
        prompt.GetComponent<RectTransform>().position = new Vector3(gameObject.transform.position.x, prompt.GetComponent<RectTransform>().position.y, gameObject.transform.position.z);
        prompt.GetComponent<RectTransform>().LookAt(dist.GetPlayerPosition());
        temp = Vector3.MoveTowards(prompt.GetComponent<RectTransform>().position, dist.GetPlayerPosition(), pDist);
        prompt.transform.position = new Vector3(temp.x, prompt.GetComponent<RectTransform>().position.y, temp.z);
        timeStamp = currentTime + 5f;
        prompt.SetActive(true);
    }
    
	// Update is called once per frame
	void Update () {
        currentTime = Time.time;
        if (prompt.activeSelf == true)
            if (currentTime >= timeStamp)
                prompt.SetActive(false);
    }
}
