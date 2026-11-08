using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClockRepairStation : MonoBehaviour, IInteractable
{
    [System.Serializable]
    public class ClockStage
    {
        public string requiredItemID;
        public GameObject visualObjectInBox;
        public bool isPlaced = false;
        public bool isRepaired = false;
    }

    public List<ClockStage> stages = new List<ClockStage>();
    public float holdDurationPerStage = 20f;  // In seconds
    private float currentHoldTimer = 0f;
    private bool isPlayerHovering = false;

    public GameObject repairProgressBarRoot;
    public Image repairProgressFillImage;
    public TextMeshProUGUI repairProgressBarText;

    private void Start()
    {
        for (int i = 0; i < stages.Count; i++)
        {
            if (stages[i].visualObjectInBox != null)
            {
                stages[i].visualObjectInBox.SetActive(false);
            }
        }

        if (repairProgressBarRoot != null)
        {
            repairProgressBarRoot.SetActive(false);
        }
    }

    private void Update()
    {
        ClockStage currentStage = GetCurrentPendingStage();

        if (currentStage != null && currentStage.isPlaced && !currentStage.isRepaired)
        {
            if (PlayerInteraction.CurrentTargetObject == gameObject)
            {
                if (repairProgressBarRoot != null)
                {
                    repairProgressBarRoot.SetActive(true);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    currentHoldTimer += Time.deltaTime;

                    UpdateVisualProgress(currentHoldTimer / holdDurationPerStage);

                    if (currentHoldTimer >= holdDurationPerStage)
                    {
                        CompleteCurrentStage(currentStage);
                    }
                }
            }
            else
            {
                if (repairProgressBarRoot != null && repairProgressBarRoot.activeSelf)
                {
                    repairProgressBarRoot.SetActive(false);
                }
            }
        }
    }

    public void Interact()
    {
        ClockStage currentStage = GetCurrentPendingStage();

        if (currentStage == null)
        {
            Debug.Log("Clock is fixed");
            return;
        }

        if (!currentStage.isPlaced)
        {
            if (PlayerInventory.Instance != null && PlayerInventory.Instance.HasItem(currentStage.requiredItemID))
            {
                PlayerInventory.Instance.RemoveItem(currentStage.requiredItemID);
                currentStage.isPlaced = true;

                if (currentStage.visualObjectInBox != null)
                {
                    currentStage.visualObjectInBox.SetActive(true);
                }
                Debug.Log($"Added part {currentStage.requiredItemID}");
            }
            else
            {
                Debug.Log($"Missing part {currentStage.requiredItemID}");
            }
        }
    }

    private ClockStage GetCurrentPendingStage()
    {
        for (int i = 0; i < stages.Count; ++i)
        {
            if (!stages[i].isRepaired)
            {
                return stages[i];
            }
        }
        return null;
    }

    private void CompleteCurrentStage(ClockStage currentStage)
    {
        currentStage.isRepaired = true;
        currentHoldTimer = 0f;

        if (repairProgressBarRoot != null)
        {
            repairProgressBarRoot.SetActive(false);
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.ProgressClockFixStage();
        }

        Debug.Log($"Fixing stage {GameManager.Instance.clockFixStagesCompleted}/{GameManager.Instance.totalClockStages}");
    }

    private void UpdateVisualProgress(float currentStagePercent)
    {
        if (repairProgressFillImage == null || GameManager.Instance == null )
        {
            return;
        }

        float completedStages = GameManager.Instance.clockFixStagesCompleted;
        float totalProgress = (completedStages + Mathf.Clamp01(currentStagePercent)) / GameManager.Instance.totalClockStages;

        repairProgressFillImage.fillAmount = totalProgress;
        repairProgressBarText.text = $"Clock repaired in {(totalProgress * 100):F2}%";
    }

    public string GetInteractionMessage()
    {
        isPlayerHovering = true;

        ClockStage currentStage = GetCurrentPendingStage();
        if (currentStage == null)
        {
            return "Clock was fixed!";
        }

        if (currentStage.isPlaced)
        {
            return "Hold [E] to repair";
        }

        if (PlayerInventory.Instance != null && PlayerInventory.Instance.HasItem(currentStage.requiredItemID))
        {
            return $"Place {currentStage.requiredItemID} [E]";
        }

        return $"Requires {currentStage.requiredItemID}";

    }
}
