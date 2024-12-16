using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public float countdownTime = 600f; 
    public Text countdownText; 
    public AudioSource voiceClip;  
    private bool isCountingDown = false;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("tema");
        StartCountdown();
    }

    public void StartCountdown()
    {
        if (!isCountingDown)
        {
            isCountingDown = true;
            StartCoroutine(CountdownCoroutine());
        }
    }

    IEnumerator TriggerEventWithDelay(CountdownEvent countdownEvent, float delay)
    {
        yield return new WaitForSeconds(delay);
        TriggerEvent(countdownEvent);
    }

    private IEnumerator QuitGame()
{
    yield return new WaitForSeconds(2f);
    
    Application.Quit();

    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
}


    public enum CountdownEvent
    {
        Narracao1,
        Relogio,
        FimCountdown
    }

    private IEnumerator CountdownCoroutine()
    {
        float remainingTime = countdownTime;

        while (remainingTime > 0)
        {
            if (countdownText != null)
            {
                countdownText.text = FormatTime(remainingTime);
            }

            if (Mathf.Approximately(remainingTime, 598f))
            {
                TriggerEvent(CountdownEvent.Narracao1);
            }

            yield return new WaitForSeconds(1f);

            remainingTime--;
        }

        CountdownFinished();
    }

    private void TriggerEvent(CountdownEvent countdownEvent)
    {
        switch (countdownEvent)
        {
            case CountdownEvent.Narracao1:
                FindObjectOfType<AudioManager>().Play("narracao-1");
                StartCoroutine(TriggerEventWithDelay(CountdownEvent.Relogio, 4f));
                break;

            case CountdownEvent.Relogio:
                FindObjectOfType<AudioManager>().Play("relogio");
                break;

            case CountdownEvent.FimCountdown:
                Debug.Log("Evento final executado.");
                break;
        }
    }

    private void CountdownFinished()
{
    Debug.Log("Countdown terminado!");

    if (countdownText != null)
    {
        countdownText.text = "Game Over";
    }

    StartCoroutine(QuitGame());
}


    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return $"{minutes:00}:{seconds:00}";
    }
}
