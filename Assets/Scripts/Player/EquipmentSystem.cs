using UnityEngine;

public class EquipmentSystem : MonoBehaviour
{
    [SerializeField] GameObject weaponHolder;
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject weaponSheath;


    GameObject currentWeapon;

    private void Start()
    {

        currentWeapon = Instantiate(weapon, weaponSheath.transform);
    }

    public void DrawWeapon()
    {

        currentWeapon.transform.SetParent(weaponHolder.transform);
        ResetWeaponTransform(currentWeapon);
    }

    public void SheathWeapon()
    {

        currentWeapon.transform.SetParent (weaponSheath.transform);
        ResetWeaponTransform (currentWeapon);

    }

    public void StartDealDamage()
    {
        currentWeapon.GetComponentInChildren<DamageDealer>().StartDealDamage();
    }
    //добавить класс enemy health
    public void EndDealDamage()
    {
        currentWeapon.GetComponentInChildren<DamageDealer>().EndDealDamage();
    }

    private void ResetWeaponTransform(GameObject currentWeapon)
    {
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
        currentWeapon.transform.localScale = Vector3.one;
    }


}
