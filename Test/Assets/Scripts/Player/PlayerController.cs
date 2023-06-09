using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public bool isDead;


    public Joystick joystickMove;
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

    public Transform destino;
    public Transform origen;
    public float tiempo;
    public GameObject bolaPrefab;
    private Vector3 velocidadInicial;
    public float shootCooldown;
    public bool isShootCooldown;
    public bool begin;
    public bool win;
    public bool onButtonPressed;


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
        isShootCooldown = false;
        begin = false;
        win = false;
        onButtonPressed = false;
        shootCooldown = Mathf.Clamp(shootCooldown, 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        shootCooldown = Mathf.Clamp(shootCooldown, 0f, 0.5f);
        if (isDead || win)
        {
            return;
        }
        else
        {
            velocidadInicial = VelocidadInicialCalculo(destino.transform.position, origen.transform.position, tiempo);

            dashCooldown -= Time.deltaTime;

            dashCooldown = Mathf.Clamp(dashCooldown, 0, 1);

            horizontalMove = joystickMove.Horizontal + Input.GetAxis("Horizontal");
            verticalMove = joystickMove.Vertical + Input.GetAxis("Vertical");

            playerInput = new Vector3(horizontalMove, 0, verticalMove);
            playerInput = Vector3.ClampMagnitude(playerInput, 1);

            movePlayer = playerInput.x * camRight + playerInput.z * camForward;

            player.transform.LookAt(player.transform.position + movePlayer);

            camDirection();


            player.Move(playerInput * playerSpeed * Time.deltaTime);


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

            if (isShootCooldown)
            {
                shootCooldown -= Time.deltaTime;
            }

            if (shootCooldown <= 0)
            {
                isShootCooldown = false;
            }

            if (onButtonPressed)
            {
                ShootButton();
            }

        }
    }

    public void changeBoolean()
    {
        onButtonPressed = !onButtonPressed;
    }

    public void ShootButton()
    {
        if (shootCooldown <= 0 && begin)
        {
            Shoot();
            shootCooldown = 0.5f;
            isShootCooldown = true;
        }
    }

    public void DashButton()
    {
        if (canDash && !isDead)
        {
            StartCoroutine(Dash());
            dashCooldown = 0.5f;
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


    public void Shoot()
    {
        GameObject bola = Instantiate(bolaPrefab, origen.transform.position, Quaternion.identity);
        bola.transform.Rotate(180, 0, 0);
        bola.GetComponent<Rigidbody>().velocity = velocidadInicial;
    }

    public Vector3 VelocidadInicialCalculo(Vector3 destino, Vector3 origen, float tiempo)
    {
        Vector3 distancia = destino - origen;
        float viX = distancia.x / tiempo;
        float viY = distancia.y / tiempo + 0.5f * Mathf.Abs(Physics2D.gravity.y) * tiempo;
        float viZ = distancia.z / tiempo;

        Vector3 velocidadInicial = new Vector3(viX, viY, viZ);
        return velocidadInicial;
    }
}
