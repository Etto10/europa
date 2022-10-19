using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject options;
    public GameObject main;

    public void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Options()
    {
        options.SetActive(true);
        main.SetActive(false);
    }

    public void Socials()
    {
        Application.OpenURL("instagram.com/carlo.lamedica");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void CloseOptions()
    {
        options.SetActive(false);
        main.SetActive(true);
    }

}
