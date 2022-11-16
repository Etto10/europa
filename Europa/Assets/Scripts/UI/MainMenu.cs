using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] panels;

    public void SetSlotAndPlay(int slot)
    {
        PlayerPrefs.SetInt("slot", slot);
        StartCoroutine(SwitchScene());
    }

    private IEnumerator SwitchScene ()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("GameScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void OpenPanel(GameObject panel)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        panel.SetActive(true);
    }
}
