using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject winScreen;
    public GameObject loseScreen;
    GameObject enemyBullet;


    private void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(EnemyManager.instance.currentEnemyHealth <= 0)
        {
            StartCoroutine(Restart());
            enemyBullet = GameObject.Find("Sphere(Clone)");
            Destroy(enemyBullet);
            PlayerController.instance.win = true;
            winScreen.SetActive(true);
            PlayerController.instance.begin = false;
        }

        if (PlayerManager.instance.currentHealth <= 0)
        {
            StartCoroutine(Restart());
            loseScreen.SetActive(true);
        }
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
