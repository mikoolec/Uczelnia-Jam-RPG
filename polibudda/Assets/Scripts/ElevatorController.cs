using System.Collections;
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

    private bool isAnimating = false;

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
        if (isAnimating)
        {
            return;
        }

        isOpen = !isOpen;
        StartCoroutine(DoorAnimationCoroutine());
    }

    private IEnumerator DoorAnimationCoroutine()
    {
        isAnimating = true;

        if (doorAnimator != null)
        {
            float speedModifier = isOpen ? -1f : 1f;
            doorAnimator.SetFloat("DoorSpeed", speedModifier);

            float normalizedTime = isOpen ? 0.99f : 0f;
            doorAnimator.Play("ElevatorDoorAnimation", 0, normalizedTime);
            yield return null;
            
            float animationDuration = doorAnimator.GetCurrentAnimatorStateInfo(0).length;
            yield return new WaitForSeconds(animationDuration);
        }

        isAnimating = false;
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
