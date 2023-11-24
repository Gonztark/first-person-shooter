using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // For UI elements

public class gamemanagerr : MonoBehaviour
{
    public static gamemanagerr Instance;
    public Text wintext;
    private int enemyCount = 0;
    private const int winCondition = 2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterEnemy()
    {
        enemyCount++;
    }

    public void EnemyDied()
    {
        enemyCount--;
        if (enemyCount == winCondition)
        {
            PlayerWins();
        }
    }

    private void PlayerWins()
    {
        wintext.text = "GANASTE!";
    }
}

