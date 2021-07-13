using UnityEngine;
using System.Collections;

public class doorOpenandClose : MonoBehaviour {

	//Text variables
	public bool T_ActivatedOpen = true;
	public bool T_ActivatedClose = false;
	public bool activateTrigger = false;
    private CalcDistance dist;
	//Animator variables
	Animator animator;
	bool doorOpen;


	void Start () { //what happens in the beginning of the game.
		T_ActivatedOpen = true;
		T_ActivatedClose = false;
        dist = GetComponent<CalcDistance>();
		animator = GetComponent<Animator> ();
		doorOpen = false;

	
	}
	

	void Update () { //The main function wich controlls how the system will work.
        if (dist.inRangeofPlayer(gameObject))
         {

            if (T_ActivatedOpen == true)
                T_ActivatedClose = false;

            else if (T_ActivatedClose == true)
                T_ActivatedOpen = false;

            if (activateTrigger && Input.GetKeyDown(KeyCode.E)) //For some reaseon you can't have both E (open and close).
            {
                T_ActivatedOpen = false;
                T_ActivatedClose = true;
                doorOpen = true;

                if (doorOpen)
                {
                    doorOpen = true;
                    doorController("Open");
                }

            }
            else if (T_ActivatedClose && activateTrigger && Input.GetKey(KeyCode.C))
            {
                T_ActivatedOpen = true;
                T_ActivatedClose = false;

                if (doorOpen)
                {
                    doorOpen = false;
                    doorController("Close");
                }

            }
        }
	}


														
	void OnTriggerEnter(Collider col) //If you enter the trigger this will happen.
	{
		if(col.gameObject.tag == "Player")
		{

				activateTrigger = true;
            if ((T_ActivatedOpen == true))
            { }
            if ((T_ActivatedClose == true))
            { }
		}
		
	}


	void OnTriggerExit(Collider col) //If you exit the trigger this will happen.
	{
		if(col.gameObject.tag == "Player")
		{
			activateTrigger = false;
		}

	}

	void doorController(string direction) //Animator function.
	{
		animator.SetTrigger(direction);
	}
		
}
