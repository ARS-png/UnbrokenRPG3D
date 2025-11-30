using UnityEngine;

public class WitchSwordSystem : MonoBehaviour
{
    [SerializeField] GameObject attackPrefab;

    [SerializeField] Transform rightHand;

    private GameObject newAttackPrefab;

    public void ShowSword()
    {
        newAttackPrefab =  Instantiate(attackPrefab, rightHand.position,  rightHand.rotation);

        if (newAttackPrefab)
        {
            Destroy(newAttackPrefab, 15f);
        }
    }

    public void HideSword()
    {
        if (newAttackPrefab != null)
        {
            Destroy(newAttackPrefab);
        }



    }
}