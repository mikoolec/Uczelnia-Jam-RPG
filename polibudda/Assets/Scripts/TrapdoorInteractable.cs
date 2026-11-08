using UnityEngine;

public class TrapdoorInteractable : MonoBehaviour, IInteractable
{

    public Transform playerTransform;
    public CharacterController playerController;
    public Transform teleportDestination;

    private void Start()
    {
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
                playerController = player.GetComponent<CharacterController>();
            }
        }
    }

    public string GetInteractionMessage()
    {
        return "Return to building [E]";
    }

    public void Interact()
    {
        if (teleportDestination == null)
        {
            Debug.Log("Teleport destination is not set!");
        }

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlaySFX("door_shut");
        }

        TeleportToBuilding();
    }

    public void TeleportToBuilding()
    {
        if (playerTransform != null && playerController != null)
        {
            playerController.enabled = false;
            playerTransform.position = teleportDestination.position;
            playerTransform.rotation = teleportDestination.rotation;
            playerController.enabled = true;
        }
    }
}
