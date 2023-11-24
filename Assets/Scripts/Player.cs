using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int HP = 100;


    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;

        if (HP <= 0)
        {
            print("Player Dead");
            SceneManager.LoadScene("GameOver");



        }
        else
        {
            print("Player hit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyHand"))
        {
            TakeDamage(25);
        }
    }

}
