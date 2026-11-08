using UnityEngine;

public class ClockManager : MonoBehaviour
{
    public ClockDigitDisplay[] hoursTens;
    public ClockDigitDisplay[] hoursUnits;
    public ClockDigitDisplay[] minutesTens;
    public ClockDigitDisplay[] minutesUnits;

    public void SetTime(int hours, int minutes)
    {
        if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59)
        {
            Debug.Log("Invalid time");
            return;
        }
        int hoursTensDigit = hours / 10;
        int hoursUnitsDigit = hours % 10;
        int minutesTensDigit = minutes / 10;
        int minutesUnitsDigit = minutes % 10;
        foreach (var display in hoursTens)
        {
            display.SetDigit(hoursTensDigit);
        }
        foreach (var display in hoursUnits)
        {
            display.SetDigit(hoursUnitsDigit);
        }
        foreach (var display in minutesTens)
        {
            display.SetDigit(minutesTensDigit);
        }
        foreach (var display in minutesUnits)
        {
            display.SetDigit(minutesUnitsDigit);
        }
    }
}
