using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;


    [Header("Oxygen")]
    public float oxygen;
    public float maxOxygen;
    public float oxygenRate;
    private bool getsOxygen;

    [Header("Hunger")]
    public float hunger;
    public float maxHunger;
    public float hungerRate;

    [Header("UI")]
    [SerializeField] private Slider oxSlider;
    [SerializeField] private Slider hungerSlider;

    [Header("Items")]
    public InventoryItemData metal, oxygenGens, plasticSheets, seedsItem, soilItem;
    [HideInInspector]public List<bool> elementsFound = new(5);

    private bool stopOx;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        stopOx = false;
        LoadValues();
        SetSliders();
        StartCoroutine(Hunger());
        StartCoroutine(Oxygen());
    }

    IEnumerator Hunger()
    {

        yield return new WaitForSeconds(1);
        hunger -= hungerRate;
        SetSliders();
        StartCoroutine(Hunger());
    }

    IEnumerator Oxygen()
    {
        if (stopOx)
            yield break;
        yield return new WaitForSeconds(1);
        oxygen -= oxygenRate;
        StartCoroutine(Oxygen());
        SetSliders();
    }

    private void SetSliders()
    {
        oxSlider.value = oxygen;
        hungerSlider.value = hunger;
    }


    public bool midExitingTrigger = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plastic"))
        {
            midExitingTrigger = false;
            getsOxygen = true;
            stopOx = true;
        }
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plastic"))
        {
            if (!midExitingTrigger)
            {
                StartCoroutine(StopOxRefill());
            }
        }
    }

    IEnumerator StopOxRefill()
    {
        midExitingTrigger = true;
        yield return new WaitForSeconds(1f);
        midExitingTrigger = false;
        getsOxygen = false;
        stopOx = false;
        StartCoroutine(Oxygen());
    }

    private void Update()
    {
        if (getsOxygen)
        {
            if(oxygen >= maxOxygen)
            {
                oxygen = maxOxygen;
            }
            else
            {
                oxygen += 0.1f;
            }
            SetSliders();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("oxygen", oxygen);
        PlayerPrefs.SetFloat("hunger", hunger);
    }

    private void LoadValues()
    {
        oxygen = PlayerPrefs.GetFloat("oxygen");
        hunger = PlayerPrefs.GetFloat("hunger");

        if(oxygen == 0)
        {
            oxygen = maxOxygen;
            hunger = maxHunger;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("oxygen", oxygen);
        PlayerPrefs.SetFloat("hunger", hunger);
    }
}
