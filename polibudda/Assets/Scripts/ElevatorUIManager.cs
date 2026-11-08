using TMPro;
using UnityEngine;

public class ElevatorUIManager : MonoBehaviour
{
    public static ElevatorUIManager Instance { get; private set; }

    [Header("UI")]
    public GameObject elevatorUIPanelRoot;
    public TextMeshProUGUI currentFloorDisplay;

    private ElevatorController activeElevator;

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

        if (elevatorUIPanelRoot != null)
        {
            elevatorUIPanelRoot.SetActive(false);
        }
    }

    public void OpenUI(ElevatorController elevator, string floorName)
    {
        activeElevator = elevator;

        if (currentFloorDisplay != null)
        {
            currentFloorDisplay.text = floorName;
        }

        if (elevatorUIPanelRoot != null)
        {
            elevatorUIPanelRoot.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
            CameraMovement cameraMovement = player.GetComponentInChildren<CameraMovement>();
            if (cameraMovement != null) 
            {
                cameraMovement.enabled = false;
            }
        }
    }

    public void CloseUI()
    {
        activeElevator = null;
        if (elevatorUIPanelRoot != null)
        {
            elevatorUIPanelRoot.SetActive(false);
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            CameraMovement cameraMovement = player.GetComponentInChildren<CameraMovement>();
            if (cameraMovement != null) 
            {
                cameraMovement.enabled = true;
            }
        }
    }

    public void GoToFloor(int targetFloorNumber)
    {
        if (activeElevator != null)
        {
            activeElevator.TeleportToFloor(targetFloorNumber);
        }
        CloseUI();
    }
}
