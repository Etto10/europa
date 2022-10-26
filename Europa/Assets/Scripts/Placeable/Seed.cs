using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public float growthTime, currentTime;
    private string id;

    private void Start()
    {
        float _temp = PlayerPrefs.GetFloat(id);
        if(_temp == 0) // It means playerprefs doesn't exist
        {
            id = Random.Range(0f, float.MaxValue).ToString() + Time.deltaTime;
            PlayerPrefs.SetFloat(id, growthTime);
        }
        else
        {
            currentTime = _temp;
        }
    }

    private void Update()
    {
        currentTime -= Time.deltaTime;
    }
}
