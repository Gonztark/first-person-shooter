using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI elements

public class GameManager : MonoBehaviour
{
    public int score;
    public float timeRemaining = 60f; // 1 minuto
    public Text scoreText;
    public Text timerText;
    public Text finalscoretext;

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            // Time's up, proceed to next level
            LoadNextLevel();
        }
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreDisplay();
    }

    void UpdateScoreDisplay()
    {
        scoreText.text = "Puntaje: " + score;
    }

    void UpdateTimerDisplay()
    {
        timerText.text = "Tiempo: " + Mathf.FloorToInt(timeRemaining);
    }

    void LoadNextLevel()
    {

        finalscoretext.text = "Puntuación final: " + score;
        Debug.Log("Final Score: " + score);

        StartCoroutine(ChangeSceneAfterDelay(3));

    }

    IEnumerator ChangeSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

     
        SceneManager.LoadScene("lv2");
    }
}
