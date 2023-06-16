using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    Animator animator;
    EnemyState enemyState;


    int maxEnemyHealth = 100;
    public int currentEnemyHealth;

    public Image enemyHealthBar;
    float enemyFill = 1;

    public AudioSource bgSong;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyState = EnemyState.None;
        currentEnemyHealth = maxEnemyHealth;
        changeEnemyState(EnemyState.Scream);
    }

    void Update()
    {
        currentEnemyHealth = Mathf.Clamp(currentEnemyHealth, 0, maxEnemyHealth);
        Debug.Log(currentEnemyHealth);
    }

    public void changeEnemyState(EnemyState newState)
    {
        if (enemyState == newState)
        {
            return;
        }
        resetAnimatorParameters();
        enemyState = newState;
        switch (enemyState)
        {
            case EnemyState.None:
                break;
            case EnemyState.Scream:
                animator.SetBool("isEnemyScream", true);
                break;
            case EnemyState.Idle:
                animator.SetBool("isEnemyIdleing", true);
                break;
            case EnemyState.Dead:
                animator.SetBool("isEnemyDead", true);
                break;
        }


    }

    private void resetAnimatorParameters()
    {
        foreach (AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.type == AnimatorControllerParameterType.Bool)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }

    public void idleState()
    {
        changeEnemyState(EnemyState.Idle);
        PlayerController.instance.begin = true;
        bgSong.Play();
    }

    public void EnemyTakeDamage(int amount)
    {
        currentEnemyHealth -= amount;

        enemyFill = currentEnemyHealth * 0.01f;
        enemyHealthBar.fillAmount = enemyFill;

        if (currentEnemyHealth <= 0)
        {
            changeEnemyState(EnemyState.Dead);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("PlayerBullet"))
        {
            EnemyTakeDamage(2);
        }
    }
}

public enum EnemyState
{
    None,
    Scream,
    Idle,
    Dead

}
