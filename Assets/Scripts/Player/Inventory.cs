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
    // public TMP_Text inventoryText;
    public TMP_Text totalWorthText;
    public List<CollectedBug> collectedBugsList;
    public bool isInventoryClosed;

    public GameObject inventoryBugPanelPrefab;
    public GameObject inventoryPanelContainer;
    public float inventoryBugPanelPositionOffset;

    public Movement myMovement;
    public GameObject backButton;


    void Start()
    {
        // inventoryText = inventoryUI.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        totalWorthText = inventoryUI.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>();
        isInventoryClosed = true;
        openInventoryAction = playerControls.FindActionMap("Inventory").FindAction("Open");
        openInventoryAction.Enable();
        myMovement = this.gameObject.GetComponent<Movement>();
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
        // inventoryUI.SetActive(isInventoryClosed ? true : false);

        // isInventoryClosed = isInventoryClosed ? false : true;

        if (isInventoryClosed)
        {
            inventoryUI.SetActive(true);
            UpdateInventory();
            isInventoryClosed = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            myMovement.myNet.canSwing = false;
        }
        else if (!isInventoryClosed)
        {
            ClearInventory();
            inventoryUI.SetActive(false);
            isInventoryClosed = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            myMovement.myNet.canSwing = true;

        }
    }

    void UpdateInventory()
    {
        // int i = 0;
        inventoryBugPanelPositionOffset = 0f;

        var bugGroups = collectedBugsList.GroupBy(bug => bug.bugName);

        foreach (var group in bugGroups)
        {
            // inventoryText.text += $"{group.Key} X{group.Count()}" + System.Environment.NewLine;

            InventoryBugPanel bugPanel = Instantiate(inventoryBugPanelPrefab).GetComponent<InventoryBugPanel>();
            bugPanel.rectTransform = bugPanel.gameObject.GetComponent<RectTransform>();
            inventoryPanelContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryPanelContainer.GetComponent<RectTransform>().sizeDelta.x, inventoryPanelContainer.GetComponent<RectTransform>().sizeDelta.y + bugPanel.rectTransform.rect.height);
            bugPanel.gameObject.transform.SetParent(inventoryPanelContainer.transform, false);
            

            Vector2 targetPosition = new Vector2(bugPanel.gameObject.transform.localPosition.x, bugPanel.gameObject.transform.localPosition.y - inventoryBugPanelPositionOffset);
            bugPanel.gameObject.transform.localPosition = targetPosition;
            bugPanel.thisBugType = group.First().bugType;
            bugPanel.bugNameText.text = group.Key;
            bugPanel.bugAmountText.text = group.Count().ToString();
            bugPanel.bugImage.sprite = group.First().bugSprite;
            inventoryBugPanelPositionOffset += bugPanel.rectTransform.rect.height;
            Bug bugType = group.First().bugType;
            bugPanel.thisButton.onClick.AddListener(delegate { ShowBugs(bugType); });

            // i++;
        }
    }

    public void ShowBugs(Bug bugType)
    {
        backButton.SetActive(true);
        ClearInventory();
        inventoryBugPanelPositionOffset = 0f;
        float totalWorth = 0;
        foreach (CollectedBug bug in collectedBugsList)
        {
            if (bug.bugName == bugType.bugName)
            {
                InventoryBugPanel bugPanel = Instantiate(inventoryBugPanelPrefab).GetComponent<InventoryBugPanel>();
                bugPanel.rectTransform = bugPanel.gameObject.GetComponent<RectTransform>();
                inventoryPanelContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryPanelContainer.GetComponent<RectTransform>().sizeDelta.x, inventoryPanelContainer.GetComponent<RectTransform>().sizeDelta.y + bugPanel.rectTransform.rect.height);
                bugPanel.gameObject.transform.SetParent(inventoryPanelContainer.transform, false);


                Vector2 targetPosition = new Vector2(bugPanel.gameObject.transform.localPosition.x, bugPanel.gameObject.transform.localPosition.y - inventoryBugPanelPositionOffset);
                bugPanel.gameObject.transform.localPosition = targetPosition;
                bugPanel.thisBugType = bug.bugType;
                bugPanel.bugNameText.text = bug.bugName;
                bugPanel.bugAmountText.text = "";
                bugPanel.bugSizeText.text = $"{bug.bugSize}mm";
                bugPanel.bugPriceText.text = $"${bug.sellPrice:F2}";
                bugPanel.bugImage.sprite = bug.bugSprite;
                inventoryBugPanelPositionOffset += bugPanel.rectTransform.rect.height;
                totalWorth += bug.sellPrice;
            }
        }
        totalWorthText.gameObject.SetActive(true);
        totalWorthText.text = $"${totalWorth:F2}";

    }

    public void OnClickBack()
    {
        ClearInventory();
        UpdateInventory();
        backButton.SetActive(false);
    }

    public void ClearInventory()
    {
        for (int i = inventoryPanelContainer.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(inventoryPanelContainer.transform.GetChild(i).gameObject);
        }

        inventoryPanelContainer.GetComponent<RectTransform>().sizeDelta = new Vector2(inventoryPanelContainer.GetComponent<RectTransform>().sizeDelta.x, 0);
        if (totalWorthText.gameObject.activeInHierarchy) totalWorthText.gameObject.SetActive(false);
    }

    public void CollectBug(CollectedBug newCollectedBug)
    {
        collectedBugsList.Add(newCollectedBug);
        // UpdateInventory();
    }
}

[System.Serializable]
public class CollectedBug
{
    public Bug bugType;
    public string bugName;
    public int rarity;
    public float bugSize;
    public float sellPrice;
    public Sprite bugSprite;

}
