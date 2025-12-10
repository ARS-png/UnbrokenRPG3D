using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] private GameObject inventoryUISystemObject;

    private void Awake()
    {
        inventoryUISystemObject.gameObject.SetActive(true);
    }
}