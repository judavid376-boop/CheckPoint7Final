using Photon.Pun;
using UnityEngine;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    [Header("Player")]
    [SerializeField] private string playerPrefabName = "Player";

    [Header("Spawn")]
    [SerializeField] private Transform[] spawnPoints;

    private bool playerInstantiated = false;

    void Start()
    {
        TrySpawnPlayer();
    }

    public override void OnJoinedRoom()
    {
        TrySpawnPlayer();
    }

    private void TrySpawnPlayer()
    {
        if (playerInstantiated) return;

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            Debug.LogWarning("Photon no está listo aún. Esperando conexión...");
            return;
        }

        Transform spawn = GetSpawnPoint();

        PhotonNetwork.Instantiate(
            playerPrefabName,
            spawn.position,
            spawn.rotation
        );

        playerInstantiated = true;

        Debug.Log("Jugador instanciado correctamente en spawn controlado.");
    }

    private Transform GetSpawnPoint()
    {
        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogWarning("No hay SpawnPoints asignados. Usando Vector3.zero");
            return null;
        }

        int index = PhotonNetwork.LocalPlayer.ActorNumber % spawnPoints.Length;
        return spawnPoints[index];
    }
}
