using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameplayUI : MonoBehaviour
{
    public TextMeshProUGUI scoreCounter;
    int targetCount;
    float currentCount;
    public void ScoreUpdate()
    {
        targetCount = Director.GetManager<ScoreManager>().score;
    }

    private void Update()
    {
        currentCount = (Mathf.Lerp(currentCount, targetCount, .1f));
        scoreCounter.text = ((int)currentCount).ToString();
    }
}
