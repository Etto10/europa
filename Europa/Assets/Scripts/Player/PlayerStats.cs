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

        hunger = PlayerPrefs.GetFloat("hunger");
        oxygen = PlayerPrefs.GetFloat("oxygen");
        if (oxygen < 0 || hunger < 0)
        {
            NewValues();
        }

        SetMaxValues();
        SetSliders();
        StartCoroutine(Hunger());
        StartCoroutine(Oxygen());
        StartCoroutine(SaveCoroutine());
    }

    private void NewValues()
    {
        PlayerPrefs.SetFloat("oxygen", maxOxygen);
        PlayerPrefs.SetFloat("hunger", maxHunger);
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

    private void SetMaxValues()
    {
        oxSlider.maxValue = maxOxygen;
        hungerSlider.maxValue = maxHunger;
    }

    private void SetSliders()
    {
        oxSlider.value = oxygen;
        hungerSlider.value = hunger;
    }

    IEnumerator SaveCoroutine()
    {
        yield return new WaitForSeconds(10f);

        PlayerPrefs.SetFloat("hunger", hunger);
        PlayerPrefs.SetFloat("oxygen", oxygen);
        StartCoroutine(SaveCoroutine());
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
}
