using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [HideInInspector]
    public bool isPaused;

    public GameObject pauseObj;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseObj.SetActive(false);
    }

    public void Quit()
    {
        DataManager.Instance.SaveData();
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
