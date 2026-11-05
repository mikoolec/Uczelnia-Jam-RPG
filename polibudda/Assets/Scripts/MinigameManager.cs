using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance { get; private set; }

    public bool isMinigameActive = false;
    private TaskInteractable currentTask;

    public PlayerMovement playerMovement;
    public CameraMovement cameraMovement;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartMinigame(TaskInteractable task)
    {
        currentTask = task;
        isMinigameActive = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        if (cameraMovement != null)
        {
            cameraMovement.enabled = false;
        }
    }

    public void EndMinigame()
    {
        isMinigameActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        if (cameraMovement != null)
        {
            cameraMovement.enabled = true;
        }

        currentTask = null;
    }

    public void OnMinigameSuccess()
    {
        if (currentTask != null)
        {
            currentTask.CompleteTask();
            if (currentTask.minigamePanel != null)
            {
                currentTask.minigamePanel.SetActive(false);
            }
        }
        EndMinigame();
    }

    public void OnMinigameFailure()
    {
        if (currentTask != null)
        {
            if (currentTask.minigamePanel != null)
            {
                currentTask.minigamePanel.SetActive(false);
            }
        }
        EndMinigame();
    }
}
