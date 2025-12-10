using UnityEngine;

[CreateAssetMenu(fileName = "BasicSwordItemSO", menuName = "Inventory/new SwordItemSO", order = 1)]
public class InventoryWeaponItemSO : ScriptableObject
{
    [Header("Weapon Holder")]
    public Vector3 weaponHolderPosition;
    public Vector3 weaponHolderRotation;

    [Space]

    [Header("Sheath Weapon")]
    public Vector3 weaponSheathPosition;
    public Vector3 weaponSheathRotation;


}
