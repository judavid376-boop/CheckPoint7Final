using UnityEngine;

public class MovimientoPendular : MonoBehaviour
{
    public float anguloMaximo = 45.0f;
    
    public float velocidad = 2.0f;
    
    public Vector3 ejeDePivote = Vector3.forward;
    
    public float faseInicial = 0.0f;
    
    // Rotación inicial del objeto
    private Quaternion rotacionInicial;
    private float tiempoTranscurrido;

    void Start()
    {
        // Guardar la rotación inicial del objeto
        rotacionInicial = transform.rotation;
        
        // Aplicar fase inicial
        tiempoTranscurrido = faseInicial;
    }

    void Update()
    {
        // Incrementar el tiempo
        tiempoTranscurrido += Time.deltaTime * velocidad;
        
        // Calcular el ángulo actual usando una función sinusoidal
        float anguloActual = anguloMaximo * Mathf.Sin(tiempoTranscurrido);
        
        // Aplicar la rotación
        transform.rotation = rotacionInicial * Quaternion.AngleAxis(anguloActual, ejeDePivote);
    }
}