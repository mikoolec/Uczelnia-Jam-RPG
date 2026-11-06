using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WireDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public Color wireColor;
    
    public RectTransform wireLine;
    public float snapThreshold = 25f;  // To connect when it's close enough

    private bool isConnected = false;
    private ConnectWiresMinigame minigame;

    void Awake()
    {
        minigame = FindFirstObjectByType<ConnectWiresMinigame>(FindObjectsInactive.Include);
    }

    public void SetColor(Color color)
    {
        wireColor = color;
        GetComponent<Image>().color = color;
        wireLine.GetComponent<Image>().color = color;
    }

    public void ResetWire()
    {
        isConnected = false;
        wireLine. sizeDelta = new Vector2(0, wireLine.sizeDelta.y);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isConnected)
        {
            return;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isConnected)
        {
            return;
        }
        
        WireReceiver hoveredReceiver = GetClosestReceiver(Input.mousePosition);

        if (hoveredReceiver != null)
        {
            UpdateLineShape(hoveredReceiver.transform.position);
        }
        else
        {
            UpdateLineShape(Input.mousePosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isConnected)
        {
            return;
        }

        WireReceiver closestReceiver = GetClosestReceiver(Input.mousePosition);

        if (closestReceiver != null)
        {
            UpdateLineShape(closestReceiver.transform.position);
            isConnected = true;

            if (closestReceiver.wireColor == this.wireColor)
            {
                minigame.OnWireConnected();
            }
            else
            {
                minigame.OnWrongWireConnected();
            }
        }
        else
        {
            wireLine.sizeDelta = new Vector2(0, wireLine.sizeDelta.y);
        }
    }

    private WireReceiver GetClosestReceiver(Vector3 position)
    {
        WireReceiver closestReceiver = null;
        float closestDistance = float.MaxValue;
        for (int i = 0; i < minigame.rightReceivers.Length; i++)
        {
            float distance = Vector3.Distance(position, minigame.rightReceivers[i].transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestReceiver = minigame.rightReceivers[i];
            }
        }

        if (closestDistance <= snapThreshold)
        {
            return closestReceiver;
        }
        return null;
    }

    private void UpdateLineShape(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float distance = direction.magnitude;

        wireLine.sizeDelta = new Vector2(distance, wireLine.sizeDelta.y);
        wireLine.rotation = Quaternion.Euler(0, 0, angle);
    }

}
