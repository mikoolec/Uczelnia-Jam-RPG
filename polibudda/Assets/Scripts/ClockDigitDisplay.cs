using UnityEngine;

public class ClockDigitDisplay : MonoBehaviour
{
    public GameObject[] digitObjects;
    private void Start()
    {
        SetDigit(-1);
    }

    public void SetDigit(int digit)
    {
        if (digitObjects == null || digitObjects.Length != 10)
        {
            Debug.Log("Objects not set up correctly");
            return;
        }

        for (int i = 0; i < digitObjects.Length; i++)
        {
            if (digitObjects[i] != null)
            {
                digitObjects[i].SetActive(false);
            }
        }

        if (digit >= 0 && digit <= 9)
        {
            if (digitObjects[digit] != null)
            {
                digitObjects[digit].SetActive(true);
            }
        }
    }
}
