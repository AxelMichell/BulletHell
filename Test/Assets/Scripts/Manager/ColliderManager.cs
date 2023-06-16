using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderManager : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            PlayerManager.instance.PlayerTakeDamage(25);
            Debug.Log("Hit");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == ("Lava"))
        {
            PlayerManager.instance.PlayerTakeDamage(1);
            Debug.Log("BURN");
        }
    }
}
