using UnityEngine;
using Photon.Pun;

public class LogicaPersonaje : MonoBehaviourPun
{
    [Header("Movimiento")]
    public float velocidadMovimiento = 5.0f;
    private float x, y;

    [Header("Cámara")]
    public Transform camara;
    public float sensibilidadMouse = 2f;
    private float rotacionHorizontal = 0f;
    private float rotacionVertical = 0f;
    public float anguloMin = -45f;
    public float anguloMax = 45f;
    public float distanciaCamara = 5f;

    [Header("Salto")]
    public float fuerzaSalto = 8f;
    private bool enSuelo;
    public float distanciaChequeoSuelo = 0.6f;

    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // SI NO ES MI JUGADOR, DESACTIVO LA CÁMARA
        if (!photonView.IsMine)
        {
            if (camara != null)
                camara.gameObject.SetActive(false);

            return;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // SOLO EL JUGADOR LOCAL EJECUTA ESTO
        if (!photonView.IsMine) return;

        Movimiento();
        ControlCamara();
        RevisarSuelo();
        Saltar();
    }

    void Movimiento()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 direccion = transform.forward * y + transform.right * x;
        transform.position += direccion * velocidadMovimiento * Time.deltaTime;

        if (anim != null)
        {
            anim.SetFloat("VelX", x);
            anim.SetFloat("VelY", y);
        }
    }

    void ControlCamara()
    {
        rotacionHorizontal += Input.GetAxis("Mouse X") * sensibilidadMouse;
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadMouse;

        rotacionVertical = Mathf.Clamp(rotacionVertical, anguloMin, anguloMax);

        transform.rotation = Quaternion.Euler(0f, rotacionHorizontal, 0f);

        if (camara != null)
        {
            camara.localRotation = Quaternion.Euler(rotacionVertical, 0f, 0f);
            camara.position = transform.position - transform.forward * distanciaCamara;
        }
    }

    void RevisarSuelo()
    {
        enSuelo = Physics.Raycast(transform.position, Vector3.down, distanciaChequeoSuelo);

        if (anim != null)
            anim.SetBool("ground", enSuelo);
    }

    void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);

            if (anim != null)
                anim.SetTrigger("saltar");
        }
    }
}
