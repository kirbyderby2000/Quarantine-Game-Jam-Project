using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCountManager : MonoBehaviour
{
    [SerializeField] AmmoHUDScript ammoHUD;
    [SerializeField] private int startingBulletCount = 15;
    private int currentBulletCount;

    private void Awake()
    {
        ammoHUD.UpdateAmmoCountDisplayed(startingBulletCount);
        currentBulletCount = startingBulletCount;
    }

    public bool BulletsAvailable()
    {
        return currentBulletCount > 0;
    }

    public void SpendBullet()
    {
        currentBulletCount--;
        UpdateBulletCountHUD();
    }

    public void PickUpBullet(int bulletPickUpCount = 1)
    {
        currentBulletCount += bulletPickUpCount;
        UpdateBulletCountHUD();
    }

    private void UpdateBulletCountHUD()
    {
        ammoHUD.UpdateAmmoCountDisplayed(currentBulletCount);
    }

}
