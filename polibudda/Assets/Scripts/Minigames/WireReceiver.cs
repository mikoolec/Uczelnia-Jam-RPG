using UnityEngine;
using UnityEngine.UI;

public class WireReceiver : MonoBehaviour
{
    [HideInInspector]
    public Color wireColor;

    public void SetColor(Color color)
    {
        wireColor = color;
        GetComponent<Image>().color = color;
    }
}
