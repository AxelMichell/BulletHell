using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Pendulo pendulo;
    public GameObject pivot;
    public float speed;
    public float delta;
    public float amplitude;
    public bool pendulumIsActive;
    void Start()
    {
        pendulo = new Pendulo();
        delta = 200;
        amplitude = 5;
        pendulumIsActive = true;
    }

    void Update()
    {
        if(pendulumIsActive)
        {
            amplitude -= Time.deltaTime;
            speed += Time.deltaTime * delta;
            float a = pendulo.Armonic(amplitude, speed);
            pivot.transform.position = new Vector3(pendulo.Armonic(a, speed), pivot.transform.position.y, pivot.transform.position.z);
        }

        if(amplitude <= 0)
        {
            pendulumIsActive = false;
        }
    }
}
