using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused;

    public GameObject pauseObj;
    public GameObject uiObj;

    private void Start()
    {
        uiObj.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
            uiObj.SetActive(false);
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseObj.SetActive(false);
    }

    public void SaveAndLoad()
    {
        DataManager.Instance.SaveData();
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void Save()
    {
        float hunger = PlayerStats.Instance.hunger;
        float oxygen = PlayerStats.Instance.oxygen;
        PlayerPrefs.SetFloat("hunger", hunger);
        PlayerPrefs.SetFloat("oxygen", oxygen);
    }
}
