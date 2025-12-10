using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour //button wrapper
{
    [HideInInspector] public Button button;
    public string CurrentItemId { get; private set; } = "";

    private void Awake()
    {
        button = GetComponent<Button>();
      
    }

    public void SetItem(string itemId, Sprite icon, int amount)
    {
        CurrentItemId = itemId;
        button.image.sprite  = icon;
        button.GetComponentInChildren<TextMeshProUGUI>().text = amount.ToString();
    }

    public void ResetSlot(Sprite defaultImage)
    {
        CurrentItemId = "";
        button.GetComponentInChildren<TextMeshProUGUI>().text = "";
        button.image.sprite = defaultImage;
        button.onClick.RemoveAllListeners();
    }
    

}
