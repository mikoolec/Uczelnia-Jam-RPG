using UnityEngine;

public class ElevatorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ElevatorController elevatorController;

    public string thisFloorName = "1";

    void Start()
    {
        if (elevatorController == null)
        {
            elevatorController = GetComponentInParent<ElevatorController>();
        }
    }

    public void Interact()
    {
        if (elevatorController != null && elevatorController.isOpen)
        {
            elevatorController.ToggleDoors();
        }

        if (ElevatorUIManager.Instance != null)
        {
            ElevatorUIManager.Instance.OpenUI(elevatorController, thisFloorName);
        }
    }

    public string GetInteractionMessage()
    {
        return "Choose floor [E]";
    }
}
