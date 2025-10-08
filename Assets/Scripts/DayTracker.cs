using TMPro;
using UnityEngine;

public class DayTracker : MonoBehaviour, IDayProvider
{
    public int day;
    public int Day => day; //makes this work with the interface

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Debug.Log("update to next day");
            day = day + 1;
        }
    }
}
