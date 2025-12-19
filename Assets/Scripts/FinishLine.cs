using UnityEngine;
using Photon.Pun;

public class FinishLine : MonoBehaviourPun
{
    private bool finished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (finished) return;

        if (!other.CompareTag("Player")) return;

        PhotonView pv = other.GetComponent<PhotonView>();
        if (pv == null || !pv.IsMine) return;

        finished = true;

        photonView.RPC("FinishGameForAll", RpcTarget.All);
    }

    [PunRPC]
    void FinishGameForAll()
    {
        Debug.Log("La carrera ha terminado para todos");

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
