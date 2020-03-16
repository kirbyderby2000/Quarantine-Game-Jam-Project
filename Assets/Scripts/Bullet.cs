using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Toilet paper bullet script
/// </summary>
public class Bullet : MonoBehaviour
{
    /// <summary>
    /// Velocity set for this bullet
    /// </summary>
    [SerializeField] float forceVelocity = 60.0f;

    /// <summary>
    /// Seconds to live for this bullet after it's been dispensed
    /// </summary>
    [SerializeField] float secondsToLive = 10.0f;

    /// <summary>
    /// Variable indicating if the toilet paper roll has been dispensed
    /// </summary>
    private bool rollDispensed = false;

    /// <summary>
    /// Origin point where this bullet was spawned
    /// </summary>
    private Vector3 originPoint;

    [SerializeField]
    private string fireButton = "Fire1";

    [SerializeField] ToiletPaperTrailFX toiletPaperTrailFXPrefab;

    private ToiletPaperTrailFX toiletTrailInstance;

    [SerializeField] Transform toiletPaperTrailEndRefPosition;

    private bool playerHoldingFireButton = true;

    private void Awake()
    {
        // Cache and assign the origin point
        originPoint = transform.position;
        StartCoroutine(MonitorHoldingButton());
    }

    

    /// <summary>
    /// Method called to move this bullet towards a position in space (ideally the crosshair)
    /// </summary>
    /// <param name="point">The position to move this bullet towards</param>
    public void AddForceTowardsPoint(Vector3 point)
    {
        Vector3 eulerAngle = transform.eulerAngles;

        // Look at the given destination point
        transform.LookAt(point);
        // Cache the rigidbody of this bullet
        Rigidbody rb = GetComponent<Rigidbody>();
        // Add force forward towards the destination point with the velocity assigned to this bullet
        rb.AddForce((transform.forward)* forceVelocity);
        transform.eulerAngles = eulerAngle;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If this toilet paper collided with another object and it hasn't been dispensed yet, then dispense it
        if (rollDispensed == false)
        {
            Debug.Log($"Collided with {collision.collider.gameObject.name}, dispensing roll!");
            DispenseRoll(collision.GetContact(0).point);
        };
    }

    /// <summary>
    /// Method called to dispense this toilet paper roll
    /// </summary>
    private void DispenseRoll()
    {
        Debug.Log("Dispensing Roll!");
        // Assign the dispensed boolean value to true
        rollDispensed = true;
        // Turn on the gravity for this object
        GetComponent<Rigidbody>().useGravity = true;
        // Self destruct in the provided amount of seconds assigned
        StartCoroutine(SelfDestructCoroutine());
    }

    /// <summary>
    /// Method called to dispense this toilet paper roll and also pull the player towards a pull position
    /// </summary>
    /// <param name="pullPosition"></param>
    private void DispenseRoll(Vector3 pullPosition)
    {
        DispenseRoll();
        if(playerHoldingFireButton == true) PlayerPull.PlayerSingleton.PullPlayerTowardsPoint(pullPosition, toiletTrailInstance);
    }

    /// <summary>
    /// IEnumerator intended to be used as a coroutine to self destruct in the assigned amount of seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator SelfDestructCoroutine()
    {
        yield return new WaitForSeconds(secondsToLive);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Method called to start tracking the distance of of this bullet
    /// This bullet will be dispensed when it reaches the required distance provided
    /// </summary>
    /// <param name="distance"></param>
    public void SetDistanceTrackingForDisposal(float distance)
    {
        originPoint = transform.position;
        StartCoroutine(TrackDistanceForDisposal(distance));
    }


    IEnumerator TrackDistanceForDisposal(float distance)
    {
        // While this bullet has traveled less than the required distance, keep checking to see if the bullet has been dispensed
        while(Vector3.Distance(originPoint, transform.position) < distance)
        {
            // Keep waiting every 1/4th of a second
            yield return new WaitForSeconds(0.25f);
            // If this roll has been dispensed, break out of the coroutine, discontinue to monitor the distance of this bullet
            if (rollDispensed) break;
        }
        // If this roll hasn't been dispensed yet, then dispense the roll
        if(rollDispensed == false)
        {
            Debug.Log($"Bullet travelled {distance} units but hasn't collided with anything. Dispensing roll!");
            toiletTrailInstance.BreakTPLine();
            DispenseRoll();
            
        }
    }

    IEnumerator MonitorHoldingButton()
    {
        while (Input.GetButton(fireButton))
        {
            yield return null;
        }
        playerHoldingFireButton = false;
        Debug.Log("Player let go of fire button!");
        toiletTrailInstance.BreakTPLine();
    }

    /// <summary>
    /// Method called to instantiate a toilet paper line FX
    /// Then assing the line FX starting reference position
    /// </summary>
    /// <param name="startingPosition">The starting position reference</param>
    public void AssignTPLineStartingPosition(Transform startingPosition)
    {
        // Instantiate and assign the toilet paper instance reference
        toiletTrailInstance = Instantiate(toiletPaperTrailFXPrefab, startingPosition.position, toiletPaperTrailFXPrefab.transform.rotation, this.transform);
        // Assign it's proper starting and ending position reference points
        toiletTrailInstance.AssignTPLinePositions(startingPosition, toiletPaperTrailEndRefPosition);
        toiletTrailInstance.transform.LookAt(startingPosition);
    }

}
