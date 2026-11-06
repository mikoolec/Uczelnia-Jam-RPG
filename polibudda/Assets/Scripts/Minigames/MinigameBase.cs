using System.Collections;
using TMPro;
using UnityEngine;

public abstract class MinigameBase : MonoBehaviour
{
    public float delaySeconds = 3f;
    public TextMeshProUGUI infoText;

    protected bool isEnding = false;

    protected virtual void OnEnable()
    {
        isEnding = false;
    }

    protected void EndMinigame(bool success, string message = "")
    {
        if (isEnding)
        {
            return;
        }
        StartCoroutine(EndMinigameCoroutine(success, message));
    }

    private IEnumerator EndMinigameCoroutine(bool success, string message)
    {
        isEnding = true;
        infoText.text = message;

        OnMinigameEnding();

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

    protected virtual void OnMinigameEnding() { }
}
