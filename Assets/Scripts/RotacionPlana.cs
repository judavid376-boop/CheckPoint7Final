using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotacionPlana : MonoBehaviour
{
  
    public float velocidadRotacion = 90.0f;
    
    public int direccion = 1;

    void Update()
    {
        // Rotar el objeto alrededor del eje Z
        transform.Rotate(0, velocidadRotacion * direccion * Time.deltaTime, 0);
    }
}