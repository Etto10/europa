using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayCycle : MonoBehaviour
{
    public static DayCycle Instance;

    [Range(0, 10)]
    [SerializeField] private float sunSpeed;
    [SerializeField] private Transform sun;

    [Header("Points")]
    [SerializeField] private Transform p1;
    [SerializeField] private Transform p2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            Load();
        }
    }

    private void Update()
    {
        //sunSpeed /= 100000; //Just to make the editor cleaner
        float x = Mathf.Lerp(sun.position.x, p1.position.x - 5, sunSpeed); //We subtract 5 so that it goes slightly under p1.position.x and triggers the if statement below
        sun.position = new Vector2(x, sun.position.y);

        if(x < p1.transform.position.x)
        {
            sun.position = p2.position;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("sunPos", transform.position.x);
    }

    private void Load()
    {
        sun.position = new Vector2(PlayerPrefs.GetFloat("sunPos"), transform.position.y);
    }
}
