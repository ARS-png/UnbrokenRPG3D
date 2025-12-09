using Unity.VisualScripting;
using UnityEngine;

public class EquipmentSystem : MonoBehaviour //над этим тже надо будет поработать
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;


    GameObject currentWeapon;

    private void OnEnable()
    {
        GameEventsManager.instance.inventoryEvents.onWeaponSwitch += SwitchWeapon;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.inventoryEvents.onWeaponSwitch -= SwitchWeapon;
    }

    private void Start()
    {
        currentWeapon = Instantiate(weapon, weaponSheath.transform);
        currentWeapon.GetComponent<PickUpItem>().SetIsInteractableValue(false);
    }

    public void DrawWeapon()
    {
        currentWeapon.transform.SetParent(weaponHolder.transform);
        ResetWeaponTransform(currentWeapon);
    }

    public void SheathWeapon()
    {
        currentWeapon.transform.SetParent(weaponSheath.transform);
        ResetWeaponTransform(currentWeapon);

    }

    public void StartDealDamage()
    {
        if (currentWeapon.GetComponentsInChildren<DamageDealer>() == null)
        {
            Debug.LogError("There is no damage dealer component");
            return;
        }

        currentWeapon.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }


    public void EndDealDamage()
    {
        if (currentWeapon.GetComponentsInChildren<DamageDealer>() == null)
        {
            Debug.LogError("There is no damage dealer component");
            return;
        }

        currentWeapon.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }

    private void ResetWeaponTransform(GameObject currentWeapon)
    {
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }

    public void SwitchWeapon(GameObject newWeapon)
    {
        if (IsCharacterWeaponIsNull())
        {
            Destroy(currentWeapon);

            currentWeapon = Instantiate(newWeapon, weaponSheath.transform);
            currentWeapon.GetComponent<PickUpItem>().SetIsInteractableValue(false);
        }

        else
        {
            InventoryItemSO currentWeaponInfo = currentWeapon.GetComponent<PickUpItem>().GetItemSO();
            PickUpItem currentWeaponPickUp = currentWeapon.GetComponent<PickUpItem>();

            InventoryItemSO newWeaponInfo = newWeapon.GetComponent<PickUpItem>().GetItemSO();


            ChangeHoldersData(weaponSheath, weaponHolder, newWeaponInfo);

            GameEventsManager.instance.inventoryEvents.AddItemToInventory(currentWeaponInfo, 1, currentWeaponPickUp);

            currentWeapon = Instantiate(newWeapon, weaponSheath.transform);
            currentWeapon.GetComponent<PickUpItem>().SetIsInteractableValue(false);
        }
    }

    private bool IsCharacterWeaponIsNull()
    {
        return currentWeapon == null;
    }


    private void ChangeHoldersData(GameObject weaponSheath, GameObject weaponHolder, InventoryItemSO itemSO)
    {
        InventoryWeaponItemSO weaponItemSO = itemSO.WeaponItemSO;

        weaponHolder.transform.localPosition = weaponItemSO.weaponHolderPosition;
        weaponHolder.transform.localRotation = Quaternion.Euler(weaponItemSO.weaponHolderRotation);

        weaponSheath.transform.localPosition = weaponItemSO.weaponSheathPosition;
        weaponSheath.transform.localRotation = Quaternion.Euler(weaponItemSO.weaponSheathRotation);
    }
}
