using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RadialBulletController : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;
    private Vector3 startPoint;
    private bool firstBalls;
    private bool secondBalls;
    private bool thirdBalls;
    public bool canInvoke;
    public float timerPerInvoke;
    public float timer;
    [SerializeField]
    private float starAngle = 90f, endAngle = 270f;
    private const float radius = 1F;

    private void Start()
    {
        firstBalls = true;
        secondBalls = false;
        thirdBalls = false;

        canInvoke = false;

        timer = 0;
    }

    private void Update()
    {
        timerPerInvoke += Time.deltaTime;

        if(timerPerInvoke >= 2f)
        {
            CanInvoke();
            timer = 0;
            timerPerInvoke = 0;
        }

        if (canInvoke)
        {

            timer += Time.deltaTime;

            if (firstBalls && timer >= 0.5f)
            {
                Invoke("SpawnProjectile180", 0f);
                timer = 0f;
            }

            if (secondBalls && timer >= 0.5f)
            {
                Invoke("SpawnProjectile170", 0f);
                timer = 0f;
            }

            if (thirdBalls && timer >= 0.5f)
            {
                Invoke("SpawnProjectile180Again", 0f);
                timer = 0f;
            }
        }
    }


    public float Magnitud(Vector3 mag)
    {
        float x = mag.x * mag.x;
        float y = mag.y * mag.y;
        float z = mag.z * mag.z;

        float sum = x + y + z;

        float resultado = Mathf.Sqrt(sum);
        return resultado;
    }

    public Vector3 Normalizar(Vector3 normal)
    {
        return normal / Magnitud(normal);
    }


    private void SpawnProjectile180()
    {
        float angleStep = (endAngle - starAngle) / bulletsAmount;
        float angle = starAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;


            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = Normalizar(projectileVector - startPoint);


            GameObject bul = BulletPool.instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            ;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(projectileMoveDirection);



            angle += angleStep;
        }

        firstBalls = false;
        secondBalls = true;
    }

    private void SpawnProjectile170()
    {
        float angleStep = (endAngle - starAngle) / bulletsAmount;
        float angle = starAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 170) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 170) * radius;


            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = Normalizar(projectileVector - startPoint);


            GameObject bul = BulletPool.instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            ;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(projectileMoveDirection);



            angle += angleStep;
        }

        secondBalls = false;
        thirdBalls = true;
    }

    private void SpawnProjectile180Again()
    {
        float angleStep = (endAngle - starAngle) / bulletsAmount;
        float angle = starAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;


            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = Normalizar(projectileVector - startPoint);


            GameObject bul = BulletPool.instance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            ;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMoveDirection(projectileMoveDirection);



            angle += angleStep;
        }

        thirdBalls = false;
        firstBalls = true;
    }

    private void CanInvoke()
    {
        canInvoke = !canInvoke;
    }

}
