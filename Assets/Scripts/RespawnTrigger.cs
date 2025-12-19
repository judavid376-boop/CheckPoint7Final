using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public float respawnDelay = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerSpawnMemory spawnMemory = other.GetComponent<PlayerSpawnMemory>();
        if (spawnMemory != null)
        {
            spawnMemory.RespawnWithDelay(respawnDelay);
        }
    }
}
