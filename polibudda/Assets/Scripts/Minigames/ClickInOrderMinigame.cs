using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickInOrderMinigame : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public Button[] buttons;
    public int minNum = 1;
    public int maxNum = 100;
    public float delaySeconds = 3f;

    private List<int> currentNumbers = new List<int>();
    private List<int> expectedOrder = new List<int>();

    private int currentExpectedIndex = 0;
    private bool isEnding = false;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }

    }

    private void OnEnable()
    {
        SetupNewSequence();
    }
    
    private void SetupNewSequence()
    {
        isEnding = false;
        currentExpectedIndex = 0;
        currentNumbers.Clear();
        expectedOrder.Clear();
        infoText.text = "Select numbers in order to fix it!";

        while (currentNumbers.Count < buttons.Length)
        {
            int randomNum = Random.Range(minNum, maxNum + 1);
            if (!currentNumbers.Contains(randomNum))
            {
                currentNumbers.Add(randomNum);
            }
        }

        expectedOrder = currentNumbers.ToList();
        expectedOrder.Sort();

        for (int i = 0; i < buttons.Length; i++)
        {
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = currentNumbers[i].ToString();
            }

            buttons[i].interactable = true;
            SetButtonColor(buttons[i], Color.white);
        }
    }

    public void OnButtonClicked(int buttonIndex)
    {
        if (isEnding)
        {
            return;
        }
        int clickedNumber = currentNumbers[buttonIndex];
        int expectedNumber = expectedOrder[currentExpectedIndex];
        if (clickedNumber == expectedNumber)
        {
            SetButtonColor(buttons[buttonIndex], Color.green);
            buttons[buttonIndex].interactable = false;
            currentExpectedIndex++;

            if (currentExpectedIndex >= buttons.Length)
            {
                infoText.text = "Success! You've fixed it!";
                StartCoroutine(EndMinigameCoroutine(true));
            }
        }
        else
        {
            currentExpectedIndex = 0;
            SetButtonColor(buttons[buttonIndex], Color.red);
            infoText.text = "Wrong button clicked! You were unable to fix it!";
            StartCoroutine(EndMinigameCoroutine(false));
        }
    }

    private IEnumerator EndMinigameCoroutine(bool success)
    {
        isEnding = true;

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        yield return new WaitForSeconds(delaySeconds);

        if (success)
        {
            MinigameManager.Instance.OnMinigameSuccess();
        }
        else
        {
            MinigameManager.Instance.OnMinigameFailure();
        }
    }

    private void SetButtonColor(Button button, Color color)
    {
        var colors = button.colors;
        colors.normalColor = color;
        colors.selectedColor = color;
        colors.disabledColor = color;
        button.colors = colors;
    }


}
