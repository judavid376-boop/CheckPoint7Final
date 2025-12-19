using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotacionCilindro : MonoBehaviour
{
  
    public float velocidadRotacion = 30.0f;
    
    public int direccion = 1;

    void Update()
    {
        // Rotar el objeto alrededor del eje Z
        transform.Rotate(velocidadRotacion * direccion * Time.deltaTime, 0, 0 );
    }
}