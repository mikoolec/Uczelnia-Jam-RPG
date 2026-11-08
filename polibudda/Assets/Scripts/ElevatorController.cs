using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public Transform playerTransform;
    public CharacterController playerController;
    public Animator doorAnimator;

    [Header("Floor Settings")]
    public float floorHeight = 3.5f;
    public float baseFloorY = 0f;

    [Header("Door State")]
    public bool isOpen { get; private set; } = false;

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

    public void ToggleDoors()
    {
        isOpen = !isOpen;
        PlayDoorAnimation();
    }

    private void PlayDoorAnimation()
    {
        if (doorAnimator != null)
        {
            float speedModifier = isOpen ? -1f : 1f;
            doorAnimator.SetFloat("DoorSpeed", speedModifier);

            doorAnimator.Play("ElevatorDoorAnimation", 0, isOpen ? 1f : 0f);
        }
    }

    public void TeleportToFloor(int floorNumber)
    {
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        Vector3 newPos = playerTransform.position;
        newPos.y = baseFloorY + ((floorNumber - 1) * floorHeight);
        playerTransform.position = newPos;

        if (playerController != null)
        {
            playerController.enabled = true;
        }
    }
}
