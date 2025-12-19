using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlataformaMovil : MonoBehaviour
{
 
    public float distancia = 5.0f;
    
 
    public float velocidad = 2.0f;
    
 
    public Vector3 direccion = Vector3.right;  // Por defecto se mueve horizontalmente en el eje X
    
 
    public float tiempoDePausa = 0.5f;
    
 
    public int tipoDeMovimiento = 1;
    
    // Variables internas
    private Vector3 posicionInicial;
    private Vector3 posicionDestino;
    private float tiempoActual = 0f;
    private bool moviendoHaciaDestino = true;
    private float tiempoDePausaRestante = 0f;

    void Start()
    {
        // Normalizar la dirección
        direccion = direccion.normalized;
        
        // Guardar posición inicial
        posicionInicial = transform.position;
        
        // Calcular posición destino
        posicionDestino = posicionInicial + direccion * distancia;
    }

    void Update()
    {
        // Si estamos en pausa, reducir el tiempo de pausa restante
        if (tiempoDePausaRestante > 0)
        {
            tiempoDePausaRestante -= Time.deltaTime;
            return;
        }

        // Mover la plataforma
        if (moviendoHaciaDestino)
        {
            // Moviéndose hacia el destino
            tiempoActual += Time.deltaTime * velocidad;
            
            if (tiempoActual >= 1.0f)
            {
                tiempoActual = 1.0f;
                moviendoHaciaDestino = false;
                tiempoDePausaRestante = tiempoDePausa;
            }
        }
        else
        {
            // Moviéndose hacia el origen
            tiempoActual -= Time.deltaTime * velocidad;
            
            if (tiempoActual <= 0.0f)
            {
                tiempoActual = 0.0f;
                moviendoHaciaDestino = true;
                tiempoDePausaRestante = tiempoDePausa;
            }
        }

        // Aplicar movimiento según el tipo seleccionado
        float factor;
        if (tipoDeMovimiento == 0)
        {
            // Movimiento lineal
            factor = tiempoActual;
        }
        else
        {
            // Movimiento suave (aceleración y desaceleración)
            factor = Mathf.SmoothStep(0, 1, tiempoActual);
        }

        // Actualizar posición
        transform.position = Vector3.Lerp(posicionInicial, posicionDestino, factor);
    }

    // Este método mantiene al jugador "pegado" a la plataforma
    void OnCollisionStay(Collision collision)
    {
        // Si colisiona con el jugador u otros objetos, los hará "hijos" de la plataforma 
        // para que se muevan junto con ella
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("MovableObject"))
        {
            collision.transform.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Cuando el objeto deja de estar en contacto con la plataforma, se elimina la relación padre-hijo
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("MovableObject"))
        {
            collision.transform.SetParent(null);
        }
    }
}