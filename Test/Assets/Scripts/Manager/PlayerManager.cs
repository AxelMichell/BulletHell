using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    Animator animator;
    PlayerState playerState;
    public PlayerController playerController;

    int maxHealth = 100;
    int currentHealth;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        playerState = PlayerState.None;
        currentHealth = maxHealth;
    }

    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //Debug.Log(currentHealth);
    }

    public void changePlayerState(PlayerState newState)
    {
        if (playerState == newState)
        {
            return;
        }
        resetAnimatorParameters();
        playerState = newState;
        switch (playerState)
        {
            case PlayerState.None:
                break;
            case PlayerState.Idle:
                animator.SetBool("isIdleing", true);
                break;
            case PlayerState.Running:
                animator.SetBool("isRunning", true);
                break;
            case PlayerState.Dash:
                animator.SetBool("isDashing", true);
                break;
            case PlayerState.Dead:
                animator.SetBool("isDead", true);
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            changePlayerState(PlayerState.Dead);
            playerController.isDead = true;
            playerController.playerCollider.SetActive(false);
        }
    }
}

public enum PlayerState
{
    None,
    Idle,
    Running,
    Dash,
    Dead

}
