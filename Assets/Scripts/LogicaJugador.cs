using UnityEngine;

public class LogicaJugador : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    public Animator anim;

    private float x, y;
    private Transform tr;
    private Transform cam;

    // Cámara
    public Transform cameraShoulder;
    public Transform cameraHolder;
    private float rotY = 0f;
    public float rotationSpeed = 200f;
    public float minAngle = -45f;
    public float maxAngle = 45f;
    public float cameraSpeed = 200f;

    void Start()
    {
        anim = GetComponent<Animator>();
        tr = transform;
        cam = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Movimiento();
        ControlCamara();
    }

    void Movimiento()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(x, 0, y) * velocidadMovimiento * Time.deltaTime;
        transform.Translate(movimiento, Space.Self);

        anim.SetFloat("velX", x);
        anim.SetFloat("velY", y);
    }

    void ControlCamara()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float deltaT = Time.deltaTime;

        tr.Rotate(0, mouseX * rotationSpeed * deltaT, 0);

        rotY += mouseY * rotationSpeed * deltaT;
        rotY = Mathf.Clamp(rotY, minAngle, maxAngle);

        Quaternion localRotation = Quaternion.Euler(-rotY, 0, 0);
        cameraShoulder.localRotation = localRotation;

        cam.position = Vector3.Lerp(cam.position, cameraHolder.position, cameraSpeed * deltaT);
        cam.rotation = Quaternion.Lerp(cam.rotation, cameraHolder.rotation, cameraSpeed * deltaT);
    }
}
