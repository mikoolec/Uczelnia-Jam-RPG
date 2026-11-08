using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Time")]
    public bool isTimeRunning = false;
    public float currentTimeMinutes;
    public int startHour = 8;
    public int endHour = 20;
    public float realSecToGameMinRatio = 1f; // 1 real second equals 1 game minute

    [Header("Game State")]
    public int currentEcts = 0;
    public int requiredEcts = 30;
    public int clockFixStagesCompleted = 0;
    public int totalClockStages = 3;

    [Header("UI")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI ectsText;
    public GameObject mainMenuPanel;
    public GameObject infoPanel;

    public ClockManager physicalClock;

    private bool HasWon()
    {
        // NOTE: Fixing clock is must have, but maybe ECTS could be optional?
        return clockFixStagesCompleted >= totalClockStages && currentEcts >= requiredEcts;
    }

    private void UpdateUI()
    {
        int hours = Mathf.FloorToInt(currentTimeMinutes / 60);
        int minutes = Mathf.FloorToInt(currentTimeMinutes % 60);

        if (timeText != null)
        {
            timeText.text = $"{hours:00}:{minutes:00}";
        }
        if (ectsText != null)
        {
            ectsText.text = $"ECTS: {currentEcts}/{requiredEcts}";
        }
        if (physicalClock != null && clockFixStagesCompleted >= totalClockStages)
        {
            physicalClock.SetTime(hours, minutes);
        }
    }
    
    public void AddEcts(int amount)
    {
        currentEcts += amount;
        UpdateUI();
        if(HasWon())
        {
            EndGame();
        }
    }

    public void ProgressClockFixStage()
    {
        clockFixStagesCompleted++;
        UpdateUI();
        if(HasWon())
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        if (mainMenuPanel!= null)
        {
            mainMenuPanel.SetActive(false);
        }

        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }

        if (timeText != null)
        {
            timeText.gameObject.SetActive(true);
        }

        if (ectsText != null)
        {
            ectsText.gameObject.SetActive(true);
        }

            Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isTimeRunning = true;
        currentEcts = 0;
        currentTimeMinutes = startHour * 60;
        clockFixStagesCompleted = 0;
        UpdateUI();

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StartPlaylist();
        }
    }

    public void EndGame()
    {
        isTimeRunning = false;
        if (HasWon())  
        {
            Debug.Log("Game Over: You Win!");  // We'll change it to a proper UI elementd later
        }
        else
        {
            Debug.Log("Game Over: You Lose!");
        }
    }

    public void ToggleInfoPanel()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(!infoPanel.activeSelf);
        }
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        isTimeRunning = false;

        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
    }

    void Update()
    {
        if (!isTimeRunning)
        {
            return;
        }

        currentTimeMinutes += (Time.deltaTime / realSecToGameMinRatio);
        UpdateUI();

        if (currentTimeMinutes >= endHour * 60)
        {
            EndGame();
        }
    }
}
