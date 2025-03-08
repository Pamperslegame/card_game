using UnityEngine;
using TMPro;

public class TurnTimer : MonoBehaviour
{
    public TMP_Text timerText;
    public float turnDuration = 30f;
    private float timeRemaining;
    private bool isTimerRunning = false;

    public delegate void TimerEndHandler();
    public event TimerEndHandler OnTimerEnd;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isTimerRunning = false;
                OnTimerEnd?.Invoke();
            }
        }
    }

    public void StartTimer()
    {
        timeRemaining = turnDuration;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResetTimer()
    {
        timeRemaining = turnDuration;
        UpdateTimerUI();
    }

    public void SetTimerDuration(float duration)
    {
        turnDuration = duration;
        ResetTimer();
    }

    private void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(timeRemaining).ToString() + "s";
    }

    public float GetTimeRemaining()
    {
        return timeRemaining;
    }
}
