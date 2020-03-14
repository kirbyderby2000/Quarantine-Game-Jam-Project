using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCrate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Picked up ammo!");
        PlayerPull.PlayerSingleton.GetComponent<BulletCountManager>().PickUpBullet();
        Destroy(this.gameObject);
    }
}
