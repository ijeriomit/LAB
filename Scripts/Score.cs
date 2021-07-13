using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{

    public Text scoreText;

    void Start()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = PlayerPrefs.GetString("Time", "0");
    }

    void Update()
    {
    }
}
