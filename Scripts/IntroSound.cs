using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSound : MonoBehaviour {

        public AudioClip Welcome;
        public AudioClip Prompt;
        public AudioClip CheckList;
       // public AudioClip Transition;
        void Start()
        {
          //  GetComponent<AudioSource>().loop = true;
            StartCoroutine(playEngineSound());
        }

        IEnumerator playEngineSound()
        {
            
            GetComponent<AudioSource>().clip = Welcome;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
            GetComponent<AudioSource>().clip = Prompt;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
            GetComponent<AudioSource>().clip = CheckList;
            GetComponent<AudioSource>().Play();
    }
}
