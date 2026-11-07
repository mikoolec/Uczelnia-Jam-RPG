using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    [SerializeField]
    private int maxSlots = 5;

    private HashSet<string> items = new HashSet<string>();

    public event Action OnInventoryChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public bool HasItem(string itemID)
    {
        return items.Contains(itemID);
    }

    public bool AddItem(string itemID)
    {
        if (items.Count >= maxSlots)
        {
            Debug.Log("Inventory full");
            return false;
        }
        
        if (items.Add(itemID))
        {
            OnInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }

    public bool RemoveItem(string itemID)
    {
        if (items.Remove(itemID))
        {
            OnInventoryChanged?.Invoke();
            return true;
        }
        return false;
    }

    public List<string> GetAllItems()
    {
        return new List<string>(items);
    }

}

