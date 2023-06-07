using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public bool isDead;

    float horizontalMove;
    float verticalMove;
    float playerSpeed;
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float dashTime;
    float dashCooldown;
    bool canDash;
    Vector3 playerInput;
    Vector3 movePlayer;
    CharacterController player;

    public Camera mainCamera;
    public GameObject playerCollider;
    Vector3 camForward;
    Vector3 camRight;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        playerSpeed = 5;
        player.detectCollisions = false;
        dashCooldown = 0.5f;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            dashCooldown -= Time.deltaTime;

            dashCooldown = Mathf.Clamp(dashCooldown, 0, 1);

            horizontalMove = Input.GetAxis("Horizontal");
            verticalMove = Input.GetAxis("Vertical");

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);

            camDirection();

            movePlayer = playerInput.x * camRight + playerInput.z * camForward;

            player.transform.LookAt(player.transform.position + movePlayer);

            player.Move(playerInput * playerSpeed * Time.deltaTime);

            if (Input.GetMouseButtonDown(0) && canDash)
            {
                StartCoroutine(Dash());
                dashCooldown = 0.5f;
            }

            if (horizontalMove == 0 && verticalMove == 0)
            {
                PlayerManager.instance.changePlayerState(PlayerState.Idle);
                canDash = false;
                playerCollider.SetActive(true);
            }
            else if (horizontalMove != 0 || verticalMove != 0)
            {
                PlayerManager.instance.changePlayerState(PlayerState.Running);
                playerCollider.SetActive(true);
                canDash = false;
                if (dashCooldown == 0)
                {
                    canDash = true;
                }
            }
        }
    }

    public IEnumerator Dash()
    {

        float startTime = Time.time;


        while (Time.time < startTime + dashTime)
        {
            PlayerManager.instance.changePlayerState(PlayerState.Dash);
            player.Move(playerInput * dashSpeed * Time.deltaTime);
            playerCollider.SetActive(false);

            yield return null;
        }
    }

    private void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;

    }
}
