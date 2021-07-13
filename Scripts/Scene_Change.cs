using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene_Change: MonoBehaviour
{
    public Timer time;
    private void Start()
    {
        time = GameObject.Find("Clock").GetComponentInChildren<Timer>();
    }
    private void Update()
    {
        //PlayerPrefs.SetString("Timer", time.Text.text);
    }

    public void Level1()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void exit()
    {
        Debug.Log("game quit");
        Application.Quit();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            PlayerPrefs.SetString("Time", time.Text.text);
            SceneManager.LoadScene("EndGame");
        }
    }

}