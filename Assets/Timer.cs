using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text TimerText;

    private float Seconds, Minutes, Hours;
    private string SecondsString, MinutesString, HoursString;

    private void Start()
    {
        Seconds = 0;
        Minutes = 0;
        Hours = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Seconds += Time.deltaTime;

        while (Seconds >= 60)
        {
            ++Minutes;
            Seconds = 0;
        }

        while (Minutes >= 0)
        {
            ++Hours;
            Minutes = 0;
        }

        SecondsString = Seconds.ToString();
        MinutesString = Minutes.ToString();
        HoursString = Hours.ToString();
    }
}
