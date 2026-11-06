using NUnit.Framework;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickInOrderMinigame : MinigameBase
{
    public Button[] buttons;
    public int minNum = 1;
    public int maxNum = 100;

    private List<int> currentNumbers = new List<int>();
    private List<int> expectedOrder = new List<int>();
    private int currentExpectedIndex = 0;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => OnButtonClicked(index));
        }

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetupNewSequence();
    }
    
    private void SetupNewSequence()
    {
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
                EndMinigame(true, "Success! You've fixed it!");
            }
        }
        else
        {
            currentExpectedIndex = 0;
            SetButtonColor(buttons[buttonIndex], Color.red);
            EndMinigame(false, "Wrong button clicked! You were unable to fix it!");
        }
    }

    protected override void OnMinigameEnding()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
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
