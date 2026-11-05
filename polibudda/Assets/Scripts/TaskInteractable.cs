using UnityEngine;

public class TaskInteractable : MonoBehaviour, IInteractable
{
    [Header("Task Settings")]
    public string taskName = "TASK X";
    public int ectsReward = 2;
    public bool isCompleted = false;

    [Header("UI")]
    public GameObject minigamePanel;
    
    public string GetInteractionMessage()
    {
        if (isCompleted)
        {
            return "DONE!";
        }
        return $"SOLVE TASK {taskName} [E]";
    }

    public void Interact()
    {
        if (isCompleted)
        {
            return;
        }

        Debug.Log($"Interacting with task: {taskName}");

        if (minigamePanel != null)
        {
            minigamePanel.SetActive(true);
            MinigameManager.Instance.StartMinigame(this);
        }
    }

    public void CompleteTask()
    {
        if (isCompleted)
        {
            return;
        }

        isCompleted = true;
        Debug.Log($"TASK {taskName} COMPLETED! Rewarding {ectsReward} ECTS.");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddEcts(ectsReward);
        }
    }
}
