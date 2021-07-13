using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {
    public GameObject litmusScene, balance, burner, litmus, tong, acidSpill, shower , PotExp;
    private GameObject bodyfire, rhandfire, lhandfire;
    public GameObject glassIcon, gloveIcon, coatIcon, Acidburn;

    public float time, seconds;
    private float timeStamp, currentTime;

    public bool isStart, isBegin,/*ischecklist,*/ isburner, islit,isacid, isBurning, hasTongs,hasBottle, hasFlask, isAlive, hasPitcher, hasLitmus, hasGloves, hasGlasses, hasLabCoat;
    public bool equipTongs,equipLitmus, equipPitcher, usedBurner,usedShower, usedScale, 
        usedManual, hasLabWear, exp1, exp2, equipGloves, equipGlasses, equipCoat, interactBurner, interactScale, readScalePop;
    public bool spillBool, litBool, tongBool, showerBool, bunsenBool, scaleBool, potExpBool;
    private bool[] audioBools;

    private Countdown countdown;
    private ParticleSystem gloveParticle, manualParticle, coatParticle,
        glassesParticle, bunsenParticle, scaleParticle, showerParticle, tongsParticle, litmusParticle, popOutParticle;
    public AudioSource audio;
    public AudioClip Acid_F, BeginPrompt_F, checklist_F, EndTrack_F, Fire_F, Metal_Exp_F, PH2O_F, Warning_F, Welcome_F, shatter, AcidShower, experiments;
    public Text whiteboardText;

    private Vector3 tongPos, scencePos, spillPos ,showerPos, burnerPos, scalePos, potExpPos;
    int i = 0;
    // Use this for initialization
    void Start() {
        isStart = false;
        isBegin = false;
        //ischecklist = false;
        isburner = false;
        islit = false;
        isacid = false;
        isBurning = false;
        hasGlasses = false;
        hasLabCoat = false;
        hasGloves = false;
        hasTongs = false;
        hasBottle = false;
        hasFlask = false;
        isAlive = true;
        hasPitcher = false;
        hasLitmus = false;
        exp1 = false;
        exp2 = false;
        usedBurner = false;
        usedScale = false;
        usedManual = false;
        hasLabWear = false;
        usedShower = false;
        equipTongs = false;
        equipPitcher = false;
        equipLitmus = false;
        equipGlasses = false;
        equipGloves = false;
        equipCoat = false;
        interactScale = false;
        interactBurner = false;
        tongBool = false;
        spillBool = false;
        litBool = false;
        showerBool = false;
        scaleBool = false;
        bunsenBool = false;
        readScalePop = false;
        potExpBool = false;
        audioBools = new bool[20]; //change to number of audio clips
        for (int j = 0; j < 19; j++)
            audioBools[j] = false;
        currentTime = Time.time;
        timeStamp = currentTime;

        gloveIcon = GameObject.Find("Gloves").GetComponent<Equip>().getIcon();
        glassIcon = GameObject.Find("SafetyGlasses").GetComponent<Equip>().getIcon();
        coatIcon = GameObject.Find("LabCoat").GetComponent<Equip>().getIcon();

        gloveParticle = GameObject.Find("Gloves").GetComponentInChildren<ParticleSystem>();
        manualParticle = GameObject.Find("BookSafety").GetComponentInChildren<ParticleSystem>();
        coatParticle = GameObject.Find("LabCoat").GetComponentInChildren<ParticleSystem>();
        glassesParticle = GameObject.Find("SafetyGlasses").GetComponentInChildren<ParticleSystem>();
        bunsenParticle = GameObject.Find("burner").GetComponentInChildren<ParticleSystem>();
        scaleParticle = GameObject.Find("balance").GetComponentInChildren<ParticleSystem>();
        showerParticle = GameObject.Find("shower").GetComponentInChildren<ParticleSystem>();
        tongsParticle = GameObject.Find("Tong").GetComponentInChildren<ParticleSystem>();
        litmusParticle = GameObject.Find("LitmusPaper").GetComponentInChildren<ParticleSystem>();
        popOutParticle = GameObject.Find("ScaleExperiment").GetComponentInChildren<ParticleSystem>();


        gloveParticle.Stop();
        gloveParticle.Clear();
       
        coatParticle.Stop();
        coatParticle.Clear();

        glassesParticle.Stop();
        glassesParticle.Clear();

        bunsenParticle.Stop();
        bunsenParticle.Clear();

        scaleParticle.Stop();
        scaleParticle.Clear();

        showerParticle.Stop();
        showerParticle.Clear();

        tongsParticle.Stop();
        tongsParticle.Clear();

        litmusParticle.Stop();
        litmusParticle.Clear();

        popOutParticle.Stop();
        popOutParticle.Clear();



        bodyfire = gameObject.transform.Find("Flames").gameObject;
        bodyfire.GetComponent<ParticleSystem>().Stop();
        bodyfire.GetComponent<ParticleSystem>().Clear();
        rhandfire = GameObject.Find("burnRHand");
        rhandfire.GetComponent<ParticleSystem>().Stop();
        rhandfire.GetComponent<ParticleSystem>().Clear();
        lhandfire = GameObject.Find("burnLHand");
        lhandfire.GetComponent<ParticleSystem>().Stop();
        lhandfire.GetComponent<ParticleSystem>().Clear();
        countdown = GetComponent <Countdown>();
        audio = GetComponent<AudioSource>();

        whiteboardText = GameObject.Find("Lab_Whiteboard").GetComponentInChildren<Text>();

        tong = GameObject.Find("Tong");
        litmusScene = GameObject.Find("LitmusScene");
        acidSpill = GameObject.Find("AcidFlask");
        burner = GameObject.Find("burner");
        balance = GameObject.Find("balance");
        PotExp = GameObject.Find("PotassiumGroup");
        shower = GameObject.Find("shower");



        tongPos = tong.transform.position;
        scencePos = litmusScene.transform.position;
        spillPos = acidSpill.transform.position;
        burnerPos = burner.transform.position;
        scalePos = balance.transform.position;
        showerPos = shower.transform.position;
        potExpPos = PotExp.transform.position;

        //acidSpill.SetActive(false);
        //litmusScene.SetActive(false);
        //tong.SetActive(false);

        Acidburn = GameObject.Find("AcidEffect");
        Acidburn.SetActive(false);

        /*disableObj(tong);
        disableObj(litmusScene);
        disableObj(acidSpill);
        disableObj(burner);
        disableObj(shower);
        disableObj(balance);
        disableObj(PotExp);*/
        
        whiteboardText.text = "Welcome to Lab!\n ツ\nOBJECTIVES: " + "\n -Read the Manual";
    }
    public void disableObj(GameObject obj)
    {
        obj.transform.position += new Vector3(50f, 50f, 50f);
    }
    public void enableObj(GameObject obj, Vector3 pos)
    {
        obj.transform.position = pos;
    }
    public void BurnPlayer()
    {
       
      bodyfire.GetComponent<ParticleSystem>().Play();
        if(!audio.isPlaying)
         audio.PlayOneShot(Fire_F);
      isBurning = true;
      timeStamp = currentTime + 10f;
       
    }
    public void BurnHands()
    {
        rhandfire.GetComponent<ParticleSystem>().Play();
        lhandfire.GetComponent<ParticleSystem>().Play();
    }
    public void Extinguish()
    {

        Acidburn.SetActive(false);
        bodyfire.GetComponent<ParticleSystem>().Stop();
        rhandfire.GetComponent<ParticleSystem>().Stop();
        lhandfire.GetComponent<ParticleSystem>().Stop();
        isBurning = false; 
    }
    public void StopParticle(ParticleSystem system)
    {
        if (system.isPlaying)
        {
           system.Stop();
           system.Clear();
        }
    }
    public void StartParticle(ParticleSystem system)
    {
        if (!system.isPlaying)
            system.Play();
    }
    public void PlayAudio(AudioClip clip, int i)
    {
        if(!audio.isPlaying && !audioBools[i])
        {
            audio.PlayOneShot(clip);
            audioBools[i] = true;
        }
    }
    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        
        PlayAudio(Welcome_F, 0);
  /*
            if (usedManual) //read manual
        {
            whiteboardText.text = "Welcome to Lab! \nツ \n Objectives:\n - Find the gloves\n - Find the goggles\n - Find the lab coat\n";
            //add code this scenario
            //print("manual used");
            StopParticle(manualParticle);

            PlayAudio(checklist_F, 1);

            if (!gloveParticle.isPlaying && !glassesParticle.isPlaying && !coatParticle.isPlaying) //&&!ischecklist)
            {
                StartParticle(gloveParticle);
                StartParticle(glassesParticle);
                StartParticle(coatParticle);
                
                //ischecklist = true;
            }
           if(equipGlasses && equipGloves && equipCoat)//checks if collected all safety equipment
             {
                //add code this scenario
                PlayAudio(experiments, 2);
                if (!showerBool)
                {
                    enableObj(shower, showerPos);
                    showerBool = true;
                }
                StartParticle(showerParticle);
                whiteboardText.text = "\nOBJECTIVES:\nCheck the Shower's functionality";
                if(usedShower)//checks if used shower
                {
                    if (!tongBool)
                    {
                        enableObj(tong, tongPos);
                        tongBool = true;
                    }
                    if (!bunsenBool)
                    {
                        enableObj(burner, burnerPos);
                        bunsenBool = true;
                    }
                    if (!scaleBool)
                    {
                        enableObj(balance, scalePos);
                        scaleBool = true;
                    }

                    StopParticle(showerParticle);
                    StartParticle(bunsenParticle);
                    StartParticle(tongsParticle);
                    StartParticle(scaleParticle);
                    
                    whiteboardText.text = "\nOBJECTIVES: \n-PICK UP THE TONGS\n-PLACE A BEAKER ON THE BURNER WITH \n   THE TONGS AND BOIL WATER\n-FIND OUT WHICH METAL IS POTASSIUM";
                    if (interactBurner)
                    {
                        StopParticle(bunsenParticle);
                        PlayAudio(Warning_F, 3);

                    }
                    if (interactScale)
                    {
                        StopParticle(scaleParticle);
                        PlayAudio(Metal_Exp_F, 4);
                        enableObj(PotExp, potExpPos);
                        StartParticle(popOutParticle);
                        if (readScalePop)
                        {
                            StopParticle(popOutParticle);
                        }
                    }
                    
                    if(exp1&&usedBurner&&equipTongs)
                    {
                        StopParticle(popOutParticle);
                        whiteboardText.text = "\nOBJECTIVES:\n-Use the Litmus paper to figure out \nwhich vial is Acidic and Basic.\n-Interact with the acid spill using the \nbasic vial.";
                        if(!litBool && !spillBool)
                        {
                            enableObj(acidSpill, spillPos);
                            enableObj(litmusScene, scencePos);
                            litBool = true;
                            spillBool = true;
                        }
                        StartParticle(litmusParticle);
                        //print("DADADADAA");
                        PlayAudio(shatter, 5);
                        PlayAudio(Acid_F, 6);
                    }
                   
                }
             }

        } */
        currentTime = Time.time;
        if (currentTime >= timeStamp && isBurning)
            BurnHands();
        if (isBurning)
        {
            if(!islit)
            {
                audio.PlayOneShot(Fire_F);
                islit = true;
            }
            countdown.Count();
            //countdown.CD.text = countdown.timeTilDeath.ToString();
        }
        else
        {
            countdown.timeTilDeath = 20f;
        }


    }
}
