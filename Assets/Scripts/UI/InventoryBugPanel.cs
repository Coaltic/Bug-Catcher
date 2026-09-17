using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryBugPanel : MonoBehaviour
{
    public Button thisButton;

    public TMP_Text bugNameText;
    public TMP_Text bugAmountText;
    public Image bugImage;

    public RectTransform rectTransform;
    public float targetPositionBelow;
    public float height;

    // public string lobbyIDNumber;

    void Awake()
    {
        bugNameText = this.gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        bugAmountText = this.gameObject.transform.GetChild(1).GetComponent<TMP_Text>();
        bugImage = this.gameObject.transform.GetChild(2).GetComponent<Image>();
        thisButton = this.gameObject.GetComponent<Button>();
        // thisButton.onClick.AddListener(OnClick);
        Canvas.ForceUpdateCanvases();
        Invoke("GetRectSize", 0.01f);
    }

    private void GetRectSize()
    {
        rectTransform = this.GetComponent<RectTransform>();
        height = rectTransform.rect.height;
        //targetPositionBelow = rectTransform.localPosition.y -  * 2;
        // scrollRect = this.gameObject.transform.parent.transform.parent.transform.parent.GetComponent<ScrollRect>();
        // Debug.Log($"Local Position Y: {rectTransform.localPosition.y}, Height: {height}");

        // LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content);
    }

    public void OnClick()
    {
        // testLobby.JoinLobby(this);
    }
}
