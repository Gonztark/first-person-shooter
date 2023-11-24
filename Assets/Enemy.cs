using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private bool isDead = false;
    [SerializeField] private int HP = 100;
    private Animator animator;

    private NavMeshAgent navAgent;

    private void Start()
    {
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(int damageAmount)
    {

        if (isDead) return;
        HP -= damageAmount;

        if (HP <= 0)
        {
            isDead = true;
            int randomValue = Random.Range(0, 2);

            if(randomValue == 0)
            {
                animator.SetTrigger("DIE1");
            }

            else
            {
                animator.SetTrigger("DIE2");
            }
            

        }
        else
        {
            animator.SetTrigger("DAMAGE");
        }

    }


}
