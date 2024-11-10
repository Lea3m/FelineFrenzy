using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameManager gameManager;
    public HealthBar timerBar;
    public int maxTime = 60;
    private int currentTime;

    private void Start()
    {
        currentTime = maxTime;
        timerBar.SetMaxHealth(maxTime);
        timerBar.SetHealth(currentTime);

        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            timerBar.SetHealth(currentTime);
        }

        gameManager.RestartGame();
}
}
