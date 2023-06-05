using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    Animator animator;
    PlayerState playerState;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        playerState = PlayerState.None;
    }

    void Update()
    {

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Debug.Log("Hit");
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
