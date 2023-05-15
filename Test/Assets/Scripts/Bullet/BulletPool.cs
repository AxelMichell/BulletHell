using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    [SerializeField]
    private GameObject pooledBullet;
    private bool notEnoughBulletsInPool;

    private List<GameObject> bullets;

    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        notEnoughBulletsInPool = true;
        bullets = new List<GameObject>();
    } 

    void Update()
    {
        
    }

    public GameObject GetBullet()
    {
        if(bullets.Count > 0)
        {
            for(int i = 0; i < bullets.Count; i++)
            {
                if (!bullets[i].activeInHierarchy)
                {
                    return bullets[i];
                }
            }
        }

        if (notEnoughBulletsInPool)
        {
            GameObject bul = Instantiate(pooledBullet);
            bul.SetActive(false);
            bullets.Add(bul);
            return bul;
        }

        return null;
    }
}
