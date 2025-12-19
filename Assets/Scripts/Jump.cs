using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Salto : MonoBehaviour
{
    public float fuerzaSalto = 25f; // Aumentamos la fuerza de salto para que sea más alto
    public float distanciaCheckSuelo = 0.3f;
    public LayerMask capaSuelo;

    private bool enSuelo = false;
    private Rigidbody rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        // Deja la gravedad normal (no modificamos la gravedad global aquí)
    }

    void Update()
    {
        RevisarSuelo();

        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse); // Aseguramos que el salto sea alto
            anim.SetTrigger("Salto");
        }

        anim.SetBool("EnSuelo", enSuelo);
    }

    void RevisarSuelo()
    {
        // Revisamos si estamos en el suelo con un raycast
        enSuelo = Physics.Raycast(transform.position, Vector3.down, distanciaCheckSuelo, capaSuelo);
        Debug.DrawRay(transform.position, Vector3.down * distanciaCheckSuelo, enSuelo ? Color.green : Color.red);
    }
}
