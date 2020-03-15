using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    
    [SerializeField] private string fireInputButtonName = "Fire1";
    /// <summary>
    /// The fps camera reference (used for raycasting lines for bullet spawning)
    /// </summary>
    private Camera fpsCameraReference;

    [SerializeField] BulletCountManager bulletCountManager;

    /// <summary>
    /// The distance of the bullet
    /// </summary>
    [Header("Bullet Settings")]
    [SerializeField] private float bulletDistance = 60.0f;


    /// <summary>
    /// The bullet prefab
    /// </summary>
    [SerializeField] private Bullet bulletPrefab;

    

    /// <summary>
    /// The transform reference point where bullets will spawn from (Ideally the hand / gun location)
    /// </summary>
    [SerializeField] private Transform bulletSpawnTransformOriginPoint;

    [Space]
    [Header("Audio")]
    [FMODUnity.EventRef] [SerializeField]
    private string ShootSound = "event:/Player/Shoot";


    private void Awake()
    {
        fpsCameraReference = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        // If the fire button is being pressed, shoot a bullet
        if (Input.GetButtonDown(fireInputButtonName) && bulletCountManager.BulletsAvailable())
        {
            ShootBullet();
        }
    }

    /// <summary>
    /// Mehtod called to shoot a bullet
    /// </summary>
    private void ShootBullet()
    {
        Debug.Log("Firing bullet!");
        // Create a ray from the center of the camera's viewport (center of the screen)
        Ray bulletRay = fpsCameraReference.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        // Create a variable for raycast hit information
        RaycastHit hitInfo;
        // Cast a ray with the bullet ray and assign the hit info variable, set the ray to travel at the bullet's travel distance
        if(Physics.Raycast(bulletRay, out hitInfo, bulletDistance))
        {
            // If the raycast hit another object, send the bullet to the point of collision
            Debug.Log($"Hit {hitInfo.collider.gameObject.name} at {hitInfo.point.ToString()}" );
            // Spawn a bullet and send it towards the hit position
            SpawnBullet(bulletSpawnTransformOriginPoint.position, hitInfo.point);
        }
        else
        {
            // If the raycast didn't hit another object, send the bullet to the end of the raycast
            Debug.Log("Didn't hit any object. Simply firing bullet at the end of raypoint");
            SpawnBullet(bulletSpawnTransformOriginPoint.position, bulletRay.GetPoint(bulletDistance));
        }

        bulletCountManager.SpendBullet();

    }

    /// <summary>
    /// Method called to spawn a bullet at a specific position and send it towards a destination position
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <param name="endPosition"></param>
    private void SpawnBullet(Vector3 spawnPosition, Vector3 endPosition)
    {
        // Instantiate and cache the spawned bullet
        Bullet instantiatedBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        // Add force to the bullet in the specified destination position
        instantiatedBullet.AddForceTowardsPoint(endPosition);
        // Set the bullet's distance to be travelled before being dispensed
        instantiatedBullet.SetDistanceTrackingForDisposal(bulletDistance);

        PlayShootingSound(spawnPosition);
    }

    private void PlayShootingSound(Vector3 position)
    {
        FMODUnity.RuntimeManager.PlayOneShot(ShootSound, position);
    }
}
