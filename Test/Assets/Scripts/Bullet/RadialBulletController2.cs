using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialBulletController2 : MonoBehaviour
{
    [SerializeField]
    private int bulletsAmount = 10;

    private Vector3 startPoint;
    [SerializeField]
    private float starAngle = 90f, endAngle = 270f;
    private const float radius = 1F;
    private Vector3 bulletMoveDirection;

    private void Start()
    {
        InvokeRepeating("SpawnProjectile", 2.3f, 2.3f);
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


    private void SpawnProjectile()
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
    }
}
