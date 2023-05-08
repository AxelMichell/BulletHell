using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialBulletController : MonoBehaviour
{

    public int numberOfProjectiles;             
    public float projectileSpeed;               
    public GameObject ProjectilePrefab;         

    private Vector3 startPoint;                 
    private const float radius = 1F;            



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startPoint = transform.position;
            SpawnProjectile(numberOfProjectiles);
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


    private void SpawnProjectile(int _numberOfProjectiles)
    {
        float angleStep = 360f / _numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i <= _numberOfProjectiles - 1; i++)
        {
            float projectileDirXPosition = startPoint.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYPosition = startPoint.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;


            Vector3 projectileVector = new Vector3(projectileDirXPosition, projectileDirYPosition, 0);
            Vector3 projectileMoveDirection = Normalizar(projectileVector - startPoint) * projectileSpeed;


            GameObject tmpObj = Instantiate(ProjectilePrefab, startPoint, Quaternion.identity);
            tmpObj.GetComponent<Rigidbody>().velocity = new Vector3(projectileMoveDirection.x, 0, projectileMoveDirection.y);


            Destroy(tmpObj, 10F);

            angle += angleStep;
        }
    }
}
