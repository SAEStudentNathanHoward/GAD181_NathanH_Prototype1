using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TenSecondTimer : MonoBehaviour
{
    // Declaration of the variables used to track the time
    public float timeRemaining = 10;
    private bool timerIsRunning = false;
    public float secondsRemaining = 10;

    // Declaration of the text object that displays the level time
    [SerializeField] private TextMeshProUGUI timerDisplay;

    // Plays at the start of each script
    private void Start()
    {
        timerIsRunning = true;
    }

    // Constantly updates
    private void Update()
    {
        // Checking if the timer is running
        if (timerIsRunning == true)
        {
            // Checking if the timer is less more 0
            if (timeRemaining > 0)
            {
                // Makes the timer go down and updates the display
                timeRemaining -= Time.deltaTime;
                secondsRemaining = Mathf.FloorToInt(timeRemaining % 60);
                timerDisplay.text = secondsRemaining.ToString();
            }
            else
            {
                // Stops the timer and updates the display
                timerIsRunning = false;
                timeRemaining = 0;
                secondsRemaining = 0;
                timerDisplay.text = secondsRemaining.ToString();
                Debug.Log("time is up");
            }
        }
    }

    // Resets the timer
    public void ResetTimer()
    {
        timeRemaining = 10;
        timerIsRunning = true;
    }
}
