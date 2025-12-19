using UnityEngine;
using Photon.Pun;
using System.Collections;

public class PlayerSpawnMemory : MonoBehaviourPun
{
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Rigidbody rb;
    private MonoBehaviour movementScript;

    void Start()
    {
        if (!photonView.IsMine) return;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

        rb = GetComponent<Rigidbody>();

        // Cambia este nombre por TU script real de movimiento
        movementScript = GetComponent<LogicaPersonaje>();
    }

    public void RespawnWithDelay(float delay)
    {
        if (!photonView.IsMine) return;

        StartCoroutine(RespawnCoroutine(delay));
    }

    private IEnumerator RespawnCoroutine(float delay)
    {
        // Bloquear movimiento
        if (movementScript != null)
            movementScript.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        yield return new WaitForSeconds(delay);

        transform.position = initialPosition;
        transform.rotation = initialRotation;

        if (rb != null)
            rb.isKinematic = false;

        // Reactivar movimiento
        if (movementScript != null)
            movementScript.enabled = true;
    }
}
