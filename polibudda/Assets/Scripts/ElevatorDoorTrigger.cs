using UnityEngine;

public class ElevatorDoorTrigger : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ElevatorController elevatorController;

    public string GetInteractionMessage()
    {
        if (elevatorController != null && elevatorController.isOpen)
        {
            return "Close door [E]";
        }
        else
        {
            return "Open door [E]";
        }
    }

    public void Interact()
    {
        if (elevatorController != null)
        {
            elevatorController.ToggleDoors();
        }
    }

    void Start()
    {
        if (elevatorController == null)
        {
            elevatorController = GetComponentInParent<ElevatorController>();
        }
        
    }
}
