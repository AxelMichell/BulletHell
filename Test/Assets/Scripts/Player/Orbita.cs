using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbita : MonoBehaviour
{
    public float velocidadDeRotacion = 50f;
    public Transform pivote;

    void Update()
    {
        this.transform.RotateAround(pivote.transform.position, Vector3.up, velocidadDeRotacion * Time.deltaTime);


    }
}
