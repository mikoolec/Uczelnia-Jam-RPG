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

    private bool HasWon()
    {
        // NOTE: Fixing clock is must have, but maybe ECTS could be optional?
        return clockFixStagesCompleted >= totalClockStages && currentEcts >= requiredEcts;
    }

    private void UpdateUI()
    {
        if (timeText != null)
        {
            timeText.text = $"{Mathf.Floor(currentTimeMinutes / 60):00}:{Mathf.Floor(currentTimeMinutes % 60):00}";
        }
        if (ectsText != null)
        {
            ectsText.text = $"ECTS: {currentEcts}/{requiredEcts}";
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
        isTimeRunning = true;
        currentEcts = 0;
        currentTimeMinutes = startHour * 60;
        clockFixStagesCompleted = 0;
        UpdateUI();
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
        StartGame();
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
