using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InputActionAsset playerControls;
    [SerializeField] private InputAction openInventoryAction;

    public GameObject inventoryUI;
    public TMP_Text inventoryText;
    public List<CollectedBug> collectedBugsList;
    public bool isInventoryClosed;


    void Start()
    {
        inventoryText = inventoryUI.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        isInventoryClosed = true;
        openInventoryAction = playerControls.FindActionMap("Inventory").FindAction("Open");
        openInventoryAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (openInventoryAction.triggered)
        {
            ManageInventoryOpening();
        }
    }

    void ManageInventoryOpening()
    {
        inventoryUI.SetActive(isInventoryClosed ? true : false);

        isInventoryClosed = isInventoryClosed ? false : true;
    }

    void UpdateInventory()
    {
        inventoryText.text = "";
        var bugGroups = collectedBugsList.GroupBy(bug => bug.bugName);
        foreach (var group in bugGroups)
        {
            inventoryText.text += $"{group.Key} X{group.Count()}" + System.Environment.NewLine;

        }
    }

    public void CollectBug(CollectedBug newCollectedBug)
    {
        collectedBugsList.Add(newCollectedBug);
        UpdateInventory();
    }
}

[System.Serializable]
public class CollectedBug
{
    public string bugName;
    public int rarity;

}
