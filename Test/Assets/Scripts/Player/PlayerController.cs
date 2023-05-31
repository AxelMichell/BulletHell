using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    float horizontalMove;
    float verticalMove;
    float playerSpeed;
    [SerializeField]
    float dashSpeed;
    [SerializeField]
    float dashTime;
    Vector3 playerInput;
    Vector3 movePlayer;
    CharacterController player;

    public Camera mainCamera;
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
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontalMove, 0,verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        player.transform.LookAt(player.transform.position + movePlayer);

        player.Move(playerInput * playerSpeed * Time.deltaTime);

        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Dash());
        }
    }

    public IEnumerator Dash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            player.Move(playerInput * dashSpeed * Time.deltaTime);

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