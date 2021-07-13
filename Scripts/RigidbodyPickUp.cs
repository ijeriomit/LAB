/* --------------------------------------- COPYRIGHT & DISTRIBUTION & SALES & CONTACT NOTICE -------------------------------------------------
 *  Copyright 2013-2018 Ambiguous Interactive Games
 *  
 *  Ambiguous Interactive Games hereby allows you to modify this script and its files for your Unity3D project only if you've purchased Rigidbody Pick-Up from the Unity3D Asset Store. 
 *  Sale of your own project/video game that includes Rigidybody Pick-Up is hereby allowed on the basis that ONLY a compiled build of your project/video game be sold only 
 *  AFTER you email Ambiguous Interactive Games with your Invoice No. to prove you've purchased Rigidbody Pick-Up from the Unity3D Asset Store.
 *  
 *  If you wish to contact support about Rigidbody Pick-Up, you agree to provide your Invoice No. to prove you've purchased Rigidbody Pick-Up from the Unity3D Asset Store.
 *  
 *  If you wish to sell your project files, or in any way distribute your project as open-soruce, Ambiguous Interactive Games requires you to remove any and all
 *  files relating to Rigidbody Pick-Up without hesitation.
 *  -------------------------------------------------------------------------------------------------------------------------------------------
 */

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;

[AddComponentMenu("Character/Rigidbody Pick-Up")]
[RequireComponent(typeof(AudioSource))]
public class RigidbodyPickUp : MonoBehaviour
{
    [Tooltip("Input the name (STRING) of the input to pick up objects. \n\nInputs are put into the Input Manager by going to:\nEdit -> Project Settings -> Input")]
    public string pickupButton = "Fire1"; //The name of the Pick Up button you are going to use.
    [Tooltip("TRUE: Press the pick up button to hold the object.\nFALSE: Hold the pick up button to hold the object.")]
    public bool togglePickUp = false; //Make picking up objects toggable.
    [Tooltip("The distance the object will be held away from you.")]
    public float distance = 3f; //How far the object is being held away from you
    [Tooltip("The maximum amount of distance the object can be held.\nIf the object's distance passes this value, the object will be released.")]
    public float maxDistanceHeld = 4f; //How far the object can be held. If it goes past, it releases.
    [Tooltip("Maximum amount of distance to grab an object from you.")]
    public float maxDistanceGrab = 10f; //The maximum distance an object can be grabbed.
    [Tooltip("Place your main camera here. \nThis is where the raycast will shoot from so keep that in mind when dealing with 3rd person camera setups.")]
    public GameObject playerCam;//Camera of player

    private bool objectIsToggled = false; //Check if the object is held //PRIVATE
    private float togg_time = 0.5f; //A short timer for when the object be allowed to press again //PRIVATE
    private float timeHeld = 0.5f; // PRIVATE
    private bool objectDefaultGravity = true; //Use the object's default gravity setting //PRIVATE
    private Ray playerAim; //Vector3 of main camera's direction //PRIVATE
    public static GameObject objectHeld; // Object being held currently //STATIC
    public static bool isObjectHeld; // is the object being held? //STATIC
    private bool objectCan; // Can the object the player is looking at be held? //PRIVATE
    private float intTimeHeld; // PRIVATE

    public physicsSub physicsMenu = new physicsSub(); //Brings the Physic settings into the Inspector
    public crosshairSystem crosshairsSystem = new crosshairSystem(); //Bring the Crosshair System into the Inspector
    public audioSoundsSub audioSystem = new audioSoundsSub(); //Brings the audio menu into the Inspector
    public objectAlphaSub objectHoldingOpacity = new objectAlphaSub(); //Bring the Object Alpha system into the Inspector
    public throwingSystemMenu throwingSystem = new throwingSystemMenu(); //Bring the Throwing System into the Inspector
    public rotationSystemSub rotationSystem = new rotationSystemSub(); //Bring the Rotation System into the Inspector
    public objectZoomSub zoomSystem = new objectZoomSub(); //Brings the Object Zoom System into the Inspector
    public objectFreezing objectFreeze = new objectFreezing();

    void Start()
    {
        //Set bools, objects, and floats to proper defaults.
        ResetPickUp(false);
        timeHeld = 0.5f;
        intTimeHeld = timeHeld;
        zoomSystem.intDistance = distance;
        zoomSystem.maxZoom = maxDistanceHeld - 0.7f;
        throwingSystem.defaultThrowTime = throwingSystem.throwTime;

        //Certain files are not selected in the inspector, will default to ones included if the systems are enabled. 
        //If the systems are not enabled on debug start, they will not load so keep that in mind.
        if (crosshairsSystem.enabled)
        {
            if (crosshairsSystem.crosshairTextures.Length == 0)
            {
                crosshairsSystem.crosshairTextures = new Texture2D[3];
            }
            if (crosshairsSystem.crosshairTextures[0] == null)
            {
                crosshairsSystem.crosshairTextures[0] = Resources.Load<Texture2D>("Crosshair/crosshair128");
            }
            if (crosshairsSystem.crosshairTextures[1] == null)
            {
                crosshairsSystem.crosshairTextures[1] = Resources.Load<Texture2D>("Crosshair/crosshair_able");
            }
            if (crosshairsSystem.crosshairTextures[2] == null)
            {
                crosshairsSystem.crosshairTextures[2] = Resources.Load<Texture2D>("Crosshair/crosshair_grab");
            }
        }
        if (audioSystem.enabled)
        {
            if (audioSystem.pickedUpAudio == null)
            {
                audioSystem.pickedUpAudio = Resources.Load<AudioClip>("Audio/Rigid_PickUp");
            }
            if (audioSystem.objectHeldAudio == null)
            {
                Debug.LogWarning("[RIGIDBODY PICK-UP WARNING 1]The audio for 'Object Held Audio' is missing. You may leave it blank for no audio");
            }
            if (audioSystem.throwAudio == null)
            {
                audioSystem.throwAudio = Resources.Load<AudioClip>("Audio/Rigid_Dropped");
            }
        }
        if (rotationSystem.enabled)
        {
            rotationSystem.mouseScripts = new MouseLook[1];
            rotationSystem.mouseScripts[0] = GetComponent<RigidbodyFirstPersonController>().mouseLook;
            rotationSystem.initRotX = rotationSystem.mouseScripts[0].XSensitivity;
            rotationSystem.initRoyY = rotationSystem.mouseScripts[0].YSensitivity;
        }
    }

    void LateUpdate()
    {
        if (isObjectHeld && physicsMenu.placeObjectBack && objectHeld.GetComponent<Placeback>() != null)
        {
            for (int i = 0; i < objectHeld.GetComponent<Placeback>().placeLocations.Length; i++)
            {
                if (Vector3.Distance(objectHeld.GetComponent<Placeback>().placeLocations[i].position, objectHeld.transform.position) < physicsMenu.placeDistance)
                {
                    physicsMenu.canPlaceBack = true;
                    physicsMenu.placeLocation = objectHeld.GetComponent<Placeback>().placeLocations[i];
                }
                else if (Vector3.Distance(objectHeld.GetComponent<Placeback>().placeLocations[i].position, objectHeld.transform.position) > physicsMenu.placeDistance)
                {
                    physicsMenu.canPlaceBack = false;
                    physicsMenu.placeLocation = null;
                }
                else if (Vector3.Distance(objectHeld.GetComponent<Placeback>().pos, objectHeld.transform.position) < physicsMenu.placeDistance)
                {
                    physicsMenu.canPlaceBack = true;
                }
                else if (Vector3.Distance(objectHeld.GetComponent<Placeback>().pos, objectHeld.transform.position) > physicsMenu.placeDistance)
                {
                    physicsMenu.canPlaceBack = false;
                }
            }
        }

        //Check to see if the object held is deleted, if so, make it false.
        if (objectHeld == null)
        {
            isObjectHeld = false;
            if (togglePickUp)
            {
                togg_time = 0.5f;
                objectIsToggled = false;
            }
            if (zoomSystem.enabled)
            {
                distance = zoomSystem.intDistance;
            }
        }

        if ((physicsMenu.objectRotated && !isObjectHeld && !Input.GetButton(rotationSystem.rotateButton) && rotationSystem.enabled) &&
                                                            (physicsMenu.objectDirection == physicsSub.objectRotation.FaceForward ||
                                                             physicsMenu.objectDirection == physicsSub.objectRotation.TurnOnY ||
                                                             physicsMenu.objectDirection == physicsSub.objectRotation.None))
        {
            physicsMenu.objectRotated = false;
            for (int x = 0; x < rotationSystem.mouseScripts.Length; x++)
            {
                rotationSystem.mouseScripts[0].XSensitivity = rotationSystem.initRotX;
                rotationSystem.mouseScripts[0].YSensitivity = rotationSystem.initRoyY;
            }
        }
    }

    void Update()
    {
        //Rotation System
        if (rotationSystem.enabled)
        {
            if (Input.GetButton(rotationSystem.rotateButton) && isObjectHeld)
            {
                physicsMenu.objectRotated = true;
                objectHeld.GetComponent<Rigidbody>().freezeRotation = true;
                objectHeld.GetComponent<Rigidbody>().velocity = Vector3.zero;
                for (int x = 0; x < rotationSystem.mouseScripts.Length; x++)
                {
                    rotationSystem.mouseScripts[0].XSensitivity = 0;
                    rotationSystem.mouseScripts[0].YSensitivity = 0;
                }
                switch (rotationSystem.lockRotationTo)
                {
                    case rotationSystemSub.lockingRotation.X:
                        objectHeld.transform.Rotate(playerCam.transform.up, -Mathf.Deg2Rad * (rotationSystem.xRotationSpeed * Input.GetAxis("Mouse X")), Space.World);
                        break;
                    case rotationSystemSub.lockingRotation.Y:
                        objectHeld.transform.Rotate(playerCam.transform.right, Mathf.Deg2Rad * (rotationSystem.yRotationSpeed * Input.GetAxis("Mouse Y")), Space.World);
                        break;
                    case rotationSystemSub.lockingRotation.None:
                        objectHeld.transform.Rotate(playerCam.transform.up, -Mathf.Deg2Rad * (rotationSystem.xRotationSpeed * Input.GetAxis("Mouse X")), Space.World);
                        objectHeld.transform.Rotate(playerCam.transform.right, Mathf.Deg2Rad * (rotationSystem.yRotationSpeed * Input.GetAxis("Mouse Y")), Space.World);
                        break;
                }
            }
            else if (!Input.GetButton(rotationSystem.rotateButton) && isObjectHeld)
            {
                objectHeld.GetComponent<Rigidbody>().freezeRotation = false;
                for (int x = 0; x < rotationSystem.mouseScripts.Length; x++)
                {
                    rotationSystem.mouseScripts[0].XSensitivity = rotationSystem.initRotX;
                    rotationSystem.mouseScripts[0].YSensitivity = rotationSystem.initRoyY;
                }
            }
        }
    }
    void FixedUpdate()
    {
        //Crosshair Raycasting
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        RaycastHit hit;
        //Debug.DrawRay(playerAim.origin, playerAim.direction, Color.red);

        if (Physics.Raycast(playerAim, out hit, maxDistanceGrab - 1.5f))
        {
            objectCan = hit.transform.tag == "Pickable";
        }
        else
        {
            objectCan = false;
        }

        if (isObjectHeld && objectIsToggled)
        {
            holdObject();
            togg_time = togg_time - Time.deltaTime;
        }

        if (Input.GetButton(throwingSystem.throwButton) && !throwingSystem.throwing && throwingSystem.enabled && objectHeld != null)
        {
            ThrowObject();
        }

        if (throwingSystem.throwing)
        {
            throwingSystem.throwTime -= Time.deltaTime;
            if (throwingSystem.throwTime < 0)
            {
                throwingSystem.throwing = false;
                throwingSystem.throwTime = throwingSystem.defaultThrowTime;
                throwingSystem.obj = null;
            }
        }

        //Button toggles for Raycasting
        if (togglePickUp)
        {
            //Button toggles for Raycasting
            if (Input.GetButtonDown(pickupButton) && !throwingSystem.throwing && !isObjectHeld && !objectIsToggled && togg_time > 0.49f)
            {
                /*If no object is held, try to pick up an object.
                  works if you hold down the button as well.*/
                tryPickObject();
            }

            if (Input.GetButtonDown(pickupButton) && isObjectHeld && objectIsToggled && togg_time < 0)
            {
                if (physicsMenu.placeObjectBack && objectHeld.GetComponent<Placeback>() != null)
                {
                    if (physicsMenu.placeLocation != null && Vector3.Distance(physicsMenu.placeLocation.position, objectHeld.transform.position) < physicsMenu.placeDistance)
                    {
                        objectHeld.transform.position = physicsMenu.placeLocation.position;
                        objectHeld.transform.rotation = physicsMenu.placeLocation.rotation;//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    else if (Vector3.Distance(objectHeld.GetComponent<Placeback>().pos, objectHeld.transform.position) < physicsMenu.placeDistance)
                    {
                        objectHeld.transform.position = objectHeld.GetComponent<Placeback>().pos;
                        objectHeld.transform.rotation = objectHeld.GetComponent<Placeback>().rot; //////////////////////////////////////////////////////////////////////////////////////////////
                    }
                    objectHeld.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ResetPickUp(true);
                    togg_time = 0.5f;
                }
                else
                {
                    ResetPickUp(true);
                    togg_time = 0.5f;
                }
            }
        }
        else if (!togglePickUp)
        {
            //Button toggles for Raycasting
            if (Input.GetButton(pickupButton) && !throwingSystem.throwing)
            {
                if (!isObjectHeld)
                /*If no object is held, try to pick up an object.
                  works if you hold down the button as well.*/
                {
                    tryPickObject();
                }
                else if (isObjectHeld)
                {
                    holdObject();
                }
            }
            else if (!Input.GetButton(pickupButton) && isObjectHeld)
            {
                if (physicsMenu.placeObjectBack && objectHeld.GetComponent<Placeback>() != null && Vector3.Distance(objectHeld.GetComponent<Placeback>().pos, objectHeld.transform.position) < physicsMenu.placeDistance)
                {
                    objectHeld.transform.position = objectHeld.GetComponent<Placeback>().pos;
                    objectHeld.transform.rotation = objectHeld.GetComponent<Placeback>().rot;//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    objectHeld.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ResetPickUp(true);
                }
                ResetPickUp(true);
            }

        }

        //Object Freeze System
        if (objectFreeze.enabled)
        {
            if (Input.GetButtonDown(objectFreeze.freezeButton) && isObjectHeld)
            {
                objectFreeze.objectFrozen = true;
            }

            if (objectFreeze.objectFrozen && isObjectHeld)
            {
                objectHeld.GetComponent<Rigidbody>().isKinematic = true;
                ResetPickUp(true);
                throwingSystem.throwing = true;
            }
            else if (!objectFreeze.objectFrozen && isObjectHeld)
            {
                objectHeld.GetComponent<Rigidbody>().isKinematic = false;
            }
        }



        //Object Alpha System
        if (isObjectHeld && objectHoldingOpacity.enabled)
        {
            Color alpha = objectHeld.GetComponent<Renderer>().material.color;
            alpha.a = objectHoldingOpacity.transparency;
            objectHeld.GetComponent<Renderer>().material.color = alpha;
            objectHoldingOpacity.alphaSet = true;
        }
    }

    //Will try to pick up the rigidbody in the 'pickableObjs' array.
    private void tryPickObject()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Physics.Raycast(playerAim, out hit);//Outputs the Raycast

        if (hit.collider == null) return;
        if (hit.collider.tag == "Pickable")
        {
            if (Vector3.Distance(hit.collider.gameObject.transform.position, playerCam.transform.position) <= maxDistanceGrab)
            {
                isObjectHeld = true; //If object is successfully held, turn on bool
                objectHeld = hit.collider.gameObject; //Makes the object that got hit by the raycast go into the gun's objectHeld
                objectDefaultGravity = objectHeld.GetComponent<Rigidbody>().useGravity;
                objectHeld.GetComponent<Rigidbody>().useGravity = false; //Disable gravity to fix a bug
                objectHeld.GetComponent<Rigidbody>().velocity = Vector3.zero;
                objectHeld.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                objectHeld.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                if (audioSystem.enabled)
                {
                    GetComponent<AudioSource>().PlayOneShot(audioSystem.pickedUpAudio);
                    audioSystem.letGoFired = false;
                }
                if (togglePickUp)
                {
                    objectIsToggled = true;
                }
                if (objectFreeze.enabled)
                {
                    objectFreeze.objectFrozen = false;
                }
            }
        }
    }

    private void holdObject()
    {
        Ray playerAim = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        /*Finds the next position for the object held to move to, depending on the Camera's position
        ,direction, and distance the object is held between you two.*/
        Vector3 nextPos = playerCam.transform.position + playerAim.direction * distance;
        //Takes the current position of the object held
        Vector3 currPos = objectHeld.transform.position;
        timeHeld = timeHeld - 0.1f * Time.deltaTime;

        if (audioSystem.enabled && audioSystem.objectHeldAudio != null)
        {
            GetComponent<AudioSource>().PlayOneShot(audioSystem.objectHeldAudio);
        }

        /*Checking the distance between the player and the object held.
         * If the distance exceeds the 'maxDistanceHeld', it will let the object go. This also
         * stops a bug that forces objects through walls if you move back too far with an object held
         * maxDistanceGrab is how far you are able to grab an object, if it exceeds the amount, it won't do anything
         */
        if (Vector3.Distance(objectHeld.transform.position, playerCam.transform.position) > maxDistanceGrab || throwingSystem.throwing)
        {
            ResetPickUp(true);
        }

        //If an object is held, apply the object's placement.
        else if (isObjectHeld)
        {
            
            if (Vector3.Distance(objectHeld.transform.position, playerCam.transform.position) > maxDistanceHeld && timeHeld < 0)
            {
                ResetPickUp(true);
            }
            else
            {
                objectHeld.GetComponent<Rigidbody>().velocity = (nextPos - currPos) * physicsMenu.objectHoldSpeed;
                if (physicsMenu.keepRotation)
                {
                    physicsMenu.intRotation = objectHeld.transform.rotation;
                }
                if (!physicsMenu.objectRotated)
                {
                    switch (physicsMenu.objectDirection)
                    {
                        case physicsSub.objectRotation.TurnOnY:
                            objectHeld.transform.eulerAngles = new Vector3(0, playerCam.transform.eulerAngles.y, 0);
                            break;
                        case physicsSub.objectRotation.FaceForward:
                          //  objectHeld.transform.LookAt(playerCam.transform.position); ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                            break;
                    }
                }

                if (distance < zoomSystem.minZoom)
                {
                    distance = zoomSystem.minZoom + 0.1f;
                }
                else if (distance > zoomSystem.maxZoom)
                {
                    distance = zoomSystem.maxZoom - 0.1f;
                }

                if (zoomSystem.enabled)
                {
                    if (zoomSystem.useAxis)
                    {
                        if (Input.GetAxis(zoomSystem.zoomAxisButton) > 0 && isObjectHeld)
                        {
                            distance = distance + Input.GetAxis(zoomSystem.zoomAxisButton);
                        }
                        else if (Input.GetAxis(zoomSystem.zoomAxisButton) < 0 && isObjectHeld)
                        {
                            distance = distance + Input.GetAxis(zoomSystem.zoomAxisButton);
                        }
                    }
                    else
                    {
                        if (Input.GetButton(zoomSystem.zoomInButton) && isObjectHeld)
                        {
                            distance = distance + 0.05f;
                        }
                        else if (Input.GetButton(zoomSystem.zoomOutButton) && isObjectHeld)
                        {
                            distance = distance - 0.05f;
                        }
                    }
                }
            }
        }
    }


    //TODO: Remove and add new GUI system.
    void OnGUI()
    {
        if (crosshairsSystem.enabled)
        {
            if (isObjectHeld) //Object Is Being Held Crosshair
            {
                GUI.DrawTexture(new Rect(Screen.width / 2 - (crosshairsSystem.crosshairTextures[2].width / 2), Screen.height / 2 - (crosshairsSystem.crosshairTextures[2].height / 2),
                                        crosshairsSystem.crosshairTextures[2].width,
                                        crosshairsSystem.crosshairTextures[2].height),
                                        crosshairsSystem.crosshairTextures[2]);
            }
            else if (objectCan) //Object Can Be Held Crosshair
            {
                GUI.DrawTexture(new Rect(Screen.width / 2 - (crosshairsSystem.crosshairTextures[1].width / 2), Screen.height / 2 - (crosshairsSystem.crosshairTextures[1].height / 2),
                                        crosshairsSystem.crosshairTextures[1].width,
                                        crosshairsSystem.crosshairTextures[1].height),
                                        crosshairsSystem.crosshairTextures[1]);
            }
            else if (!isObjectHeld && !objectCan) //Default Crosshair
            {
                if (crosshairsSystem.crosshairTextures[0] == null)
                {
                    Debug.LogError("Crosshairs are null");
                }
                else
                {
                    GUI.DrawTexture(new Rect(Screen.width / 2 - (crosshairsSystem.crosshairTextures[0].width / 2), Screen.height / 2 - (crosshairsSystem.crosshairTextures[0].height / 2),
                                            crosshairsSystem.crosshairTextures[0].width,
                                            crosshairsSystem.crosshairTextures[0].height),
                                            crosshairsSystem.crosshairTextures[0]);
                }
            }
        }
    }

    /// <summary>
    /// Will throw <see cref="objectHeld"/>
    /// </summary>
    private void ThrowObject()
    {
        throwingSystem.obj = objectHeld;
        throwingSystem.throwing = true;
        ResetPickUp(true);
        throwingSystem.obj.GetComponent<Rigidbody>().AddForce(playerCam.transform.forward * throwingSystem.strength);
        if (audioSystem.enabled)
        {
            GetComponent<AudioSource>().PlayOneShot(audioSystem.throwAudio);
        }
    }

    /// <summary>
    /// Reset the currently <see cref="objectHeld"/>, settings and other related settings. Should always use this if you want to let go of an objection.
    /// </summary>
    public void ResetPickUp(bool disableGravity = true)
    {
       
        if (disableGravity && isObjectHeld && objectDefaultGravity)
        {
            //objectHeld.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;///////////////////////////////////////////////
            objectHeld.GetComponent<Rigidbody>().useGravity = true;
        }
        if (objectHoldingOpacity.alphaSet && objectHoldingOpacity.enabled)
        {
            Color alpha = objectHeld.GetComponent<Renderer>().material.color;
            alpha.a = 1f;
            objectHeld.GetComponent<Renderer>().material.color = alpha;
            objectHoldingOpacity.alphaSet = false;
        }
        if (objectHeld != null)
        {
            //objectHeld.GetComponent<Rigidbody>().freezeRotation = false;/////////////////////////////////////////////////////////////////////////////////////////////////
        }
        isObjectHeld = false;
        physicsMenu.canPlaceBack = false;
        objectHeld = null;
        timeHeld = intTimeHeld;
    }

}

[System.Serializable]
public class physicsSub
{
    public enum objectRotation { FaceForward, TurnOnY, None };
    [Tooltip("This will allow objects to be held a certain way in front of you.\n\nFace Forward: Objects will always be facing you.\n\nTurn on Y: Objects will be facing your direction, but keeping it's Y.\n\nNone: Allows the object to rotate however.")]
    public objectRotation objectDirection;
    [Tooltip("TRUE: Will add a script to the object, called 'Placeback', which allows objects to be returned to it's original placement.")]
    public bool placeObjectBack = false;
    [Tooltip("The distance between the original position and the object. When within the value, it'll allow you to place the object back.")]
    public float placeDistance = 2f;
    public bool keepRotation = false;
    [Tooltip("How fast an option will move between its old position and new position when object is being held.")]
    public float objectHoldSpeed = 15;
    [System.NonSerialized]
    public Quaternion intRotation;
    [System.NonSerialized]
    public bool objectRotated = false;
    [System.NonSerialized]
    public Quaternion objectRot;
    [System.NonSerialized]
    public Vector3 objectPos;
    [System.NonSerialized]
    public bool canPlaceBack = false;
    [System.NonSerialized]
    public Transform placeLocation;
}

[System.Serializable]
public class throwingSystemMenu //Throwing System
{
    [Tooltip("TRUE: Allows objects to be thrown by pressing a button.")]
    public bool enabled = false;
    [Tooltip("Input the name (STRING) of the input to throw objects. \n\nInputs are put into the Input Manager by going to:\nEdit -> Project Settings -> Input")]
    public string throwButton = "Fire2";
    [Tooltip("Strength is how strong you are able to throw objects.")]
    public float strength = 100f;

    [System.NonSerialized]
    public GameObject obj = null;
    [System.NonSerialized]
    public bool throwing = false;
    [System.NonSerialized]
    public float throwTime = 1f;
    [System.NonSerialized]
    public float defaultThrowTime;
}

[System.Serializable]
public class crosshairSystem //Crosshair System - You are no longer required to just remove the code, just disable it from the inspector!
{
    [Tooltip("TRUE: Uses Rigidbody Pick-Up's built in crosshair system.")]
    public bool enabled = true;
    [Tooltip("Default size should be 3. The following is what textures should be placed in the array for each function.\nNOTE: If set to 0, the script will set them for you if crosshair system is enabled.\n\n0 = No Objects held or in front of you, default.\n\n1 = Object is infront of you and able to be picked up.\n\n3 = Object is currently held.")]
    public Texture2D[] crosshairTextures; //Array of textures to use for the crosshair
    //0 = default | 1 = Object can be held | 2 = Object is being held currently
}

[System.Serializable]
public class rotationSystemSub
{
    [Tooltip("TRUE: Enable the rotation system, allowing you to rotate objects with your mouse.")]
    public bool enabled = false;
    [Tooltip("Input the name (STRING) of the input to rotate objects. You must HOLD this button to rotate. \n\nInputs are put into the Input Manager by going to:\nEdit -> Project Settings -> Input")]
    public string rotateButton = "Rotate"; //A Input name from the Input Manager.
    [Tooltip("The speed of rotation of objects in the X axis.")]
    public float xRotationSpeed = 100; //Rotation speed of the Rigidbody
    [Tooltip("The speed of rotation of objects in the Y axis.")]
    public float yRotationSpeed = 100; //Rotation speed of the Rigidbody
    public enum lockingRotation { None, X, Y };
    [Tooltip("None: Allows the object to be rotated without restrictions.\n\nX: objects are only allowed to be rotated in the X axis.\n\nY: objects are only allowed to be rotated in the Y axis.")]
    public lockingRotation lockRotationTo;
    //Change "MouseLook" to your own Mouse Script name. The one currently used is from the default FPS controller package.
    [Tooltip("Insert all mouse scripts that is attached to your controller to allow your camera to stop looking around while rotating objects.\n\nNOTE FOR DIFFERENT SCRIPTS: If you're using a script that isn't called 'MouseLook' for looking around, you will need to go in code and look near the bottom for the line of code. You will need to manually change it. Unity does not allow an empty array for an script sadly.")]
    public MouseLook[] mouseScripts;
    [System.NonSerialized]
    public float rotY = 0F;
    [System.NonSerialized]
    public float initRotX;
    [System.NonSerialized]
    public float initRoyY;
}

[System.Serializable]
public class objectAlphaSub
{
    [Tooltip("TRUE: Enable the system that allows transparancy for objects.\n\n\nWARNING:\nMake sure the objects material rendering mode is set to 'Fade' or 'Transparent' if you wish to use this mode.")]
    public bool enabled = true;
    [Tooltip("The value of which the object should be transparent.\n\n0 float = Completely transparent.\n\n1 float = Completely visible.")]
    public float transparency = 0.5f;
    [System.NonSerialized]
    public bool alphaSet = false;
}

[System.Serializable]
public class audioSoundsSub
{
    [Tooltip("TRUE: Enable the built-in audio system.")]
    public bool enabled = true;

    [Tooltip("The audio clip for picking up an object.")]
    public AudioClip pickedUpAudio;
    [Tooltip("IMPORTANT: The sound for this should be extremely short. Look at my audio file for example of how long it should be.\n\nThe sound for when you are currently holding something.")]
    public AudioClip objectHeldAudio;
    [Tooltip("The sound for throwing an object.")]
    public AudioClip throwAudio;
    [System.NonSerialized]
    public bool letGoFired = false;
}

[System.Serializable]
public class objectZoomSub
{
    [Tooltip("TRUE: Enable objects to be brought in and out, zooming.")]
    public bool enabled = false;

    [Tooltip("Input the name (STRING) of the input to ZOOM IN and OUT. \n\nInputs are put into the Input Manager by going to:\nEdit -> Project Settings -> Input")]
    public string zoomInButton;
    [Tooltip("Input the name (STRING) of the input to ZOOM IN and OUT. \n\nInputs are put into the Input Manager by going to:\nEdit -> Project Settings -> Input")]
    public string zoomOutButton;
    [Tooltip("TRUE: Use an axis button instead of two indiviual buttons. If you set this, you can leave the two buttons at the top empty.")]
    public bool useAxis = true; //if true, it will use the Axis Button for both zooming in and out. If alse, will use the two buttons instead
    [Tooltip("Input the name (STRING) of the input axis to ZOOM IN and OUT. \n\nInputs are put into the Input Manager by going to:\nEdit -> Project Settings -> Input")]
    public string zoomAxisButton = "Mouse ScrollWheel";
    [Tooltip("How close you can bring an object towards you.")]
    public float minZoom = 1.5f; //Set the minimum amount for how close the object can be held. Will use maxDistanceHeld for maximum distance your object can be held.
    [System.NonSerialized]
    public float maxZoom; //Leave default to allow maxDistanceHeld to use the variable.
    [System.NonSerialized]
    public float intDistance;
}

[System.Serializable]
public class objectFreezing
{
    [Tooltip("TRUE: Enable objects freezing in the air. This will allow objects to be floating in the air, just picked it up to unfreeze it again.")]
    public bool enabled = false;
    [Tooltip("Input the name (STRING) of the input to freeze objects. \n\nInputs are put into the Input Manager by going to:\nEdit -> Project Settings -> Input")]
    public string freezeButton = "Freeze"; //Set a custom button from the Input Manager to whatever you choose. I made a "Freeze" and set it to "r"
    [System.NonSerialized]
    public bool objectFrozen = false;
}