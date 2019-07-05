using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameplayUI : MonoBehaviour
{
    public TextMeshProUGUI scoreCounter;
    public Transform gameOverScreen;
    public TextMeshProUGUI gameOverText;
    public OptionsMenu optionsMenu;

    int targetScoreCount;
    float currentScoreCount;
    bool windowOpen;

    bool pressingCancelOnPreviousframe = true;
    public void ScoreUpdate()
    {
        targetScoreCount = Director.GetManager<ScoreManager>().score;
    }

    private void Update()
    {
        currentScoreCount = (Mathf.Lerp(currentScoreCount, targetScoreCount, .1f));
        scoreCounter.text = ((int)currentScoreCount).ToString();
        if (!windowOpen)
        {

            if (!pressingCancelOnPreviousframe && Input.GetAxis("Cancel") > 0)
            {
                windowOpen = true;
                optionsMenu.gameObject.SetActive(true);
                optionsMenu.OnClose += OnOptionsClose;
            }

        }
        pressingCancelOnPreviousframe = Input.GetAxis("Cancel") > 0;
    }
    void OnOptionsClose()
    {
        optionsMenu.OnClose -= OnOptionsClose;
        windowOpen = false;
    }

    public void ShowGameOver(bool win)
    {
        optionsMenu.gameObject.SetActive(false);
        gameOverScreen.gameObject.SetActive(true);
        windowOpen = true;
        gameOverText.text = win ? "You win!" : "You lose";
        gameOverText.text += "\r\nYour score: " + targetScoreCount;
        Director.GetManager<TimeManager>().Paused = true;
    }

    public void ReturnToMenu()
    {
        Director.GetManager<TimeManager>().Paused = false;

        SceneManager.LoadScene("MainMenu");
    }

    public void Replay()
    {
        Director.GetManager<TimeManager>().Paused = false;

        SceneManager.LoadScene("Gameplay");
    }
}