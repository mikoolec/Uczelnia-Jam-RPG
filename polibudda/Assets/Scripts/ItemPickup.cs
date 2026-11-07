using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public string itemID = "1";
    public string itemName = "Gear";

    public string GetInteractionMessage()
    {
        return $"Pick up {itemName} [E]";
    }

    public void Interact()
    {
        if (PlayerInventory.Instance == null)
        {
            return;
        }

        if (PlayerInventory.Instance.AddItem(itemID))
        {
            Debug.Log($"Picked up {itemName}");
            Destroy(gameObject);
        }
    }
}
