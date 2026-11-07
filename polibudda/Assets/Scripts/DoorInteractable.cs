using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    private Animator animator;
    private bool isOpen = false;

    private void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void Interact()
    {
        isOpen = !isOpen;

        if (animator != null)
        {
            animator.SetBool("isOpen", isOpen);
        }
    }

    public string GetInteractionMessage()
    {
        if (isOpen)
        {
            return "Close door [E]";
        }
        else
        {
            return "Open door [E]";
        }
    }
}
