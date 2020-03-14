using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script responsible for pulling the player (like Spiderman)
/// </summary>
public class PlayerPull : MonoBehaviour
{
    [SerializeField] float pullDistance;
    [SerializeField] float minimumDistanceToBePulled = 5.0f;
    [SerializeField] float pullPositionClippingOffset = 3.0f;
    private UnityStandardAssets.Characters.FirstPerson.FirstPersonController firstPersonController;
    private Rigidbody rigidBody;
    /// <summary>
    /// The player pull script singleton instance
    /// </summary>
    public static PlayerPull PlayerSingleton
    {
        private set;
        get;
    }

    private void Awake()
    {
        PlayerSingleton = this;
        firstPersonController = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();
        rigidBody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Method called to pull the player towards a specific position
    /// </summary>
    /// <param name="point">The point to pull the player towards</param>
    public void PullPlayerTowardsPoint(Vector3 point)
    {
        if (PullAllowed(point) == false) return;
        Debug.Log($"Pulling player towards {point.ToString()}");

        point = OffsetPullDestinationPosition(point);

        // Disable the first person controller component
        firstPersonController.PullStarted();
        // Turn off gravity
        rigidBody.useGravity = false;
        // Start moving the player towards the given point
        StartCoroutine(MovePlayerTowardsPoint(point));
    }
    
    IEnumerator MovePlayerTowardsPoint(Vector3 point)
    {
        while(Vector3.Distance(transform.position, point) >= Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, point, pullDistance);
            Debug.Log($"Pulling player towards {point.ToString()}");
            yield return null;
        }

        rigidBody.velocity = Vector3.zero;
        

        yield return null;

        rigidBody.useGravity = true;
        firstPersonController.enabled = true;
        firstPersonController.PullJump();

        yield return new WaitForSeconds(4f);
        
    }

    /// <summary>
    /// Returns an offset "pull" destination position to allow space for the player to jump up and avoid clipping into the collision object
    /// </summary>
    /// <param name="destinationPoint">The destination point</param>
    /// <returns></returns>
    private Vector3 OffsetPullDestinationPosition(Vector3 destinationPoint)
    {
        // Cache and assign the current position
        Vector3 offsetPosition = transform.position;

        // Offset the y value to the destination position if the y distance between the current and destination points is greater than 3.0f 
        if (Vector3.Distance(destinationPoint, offsetPosition) > pullPositionClippingOffset)
        {
            offsetPosition.y = destinationPoint.y;
        }

        // Cast a ray between the a normalized offset between the destination position and the player's position
        // Note: This provides space between the destination position and the player
        // to prevent the player from clipping into the colliding destination object
        Ray ray = new Ray(destinationPoint, offsetPosition - destinationPoint);
        Vector3 pointToReturn = ray.GetPoint(pullPositionClippingOffset);
        if(Physics.Raycast(pointToReturn, Vector3.down, 1.4f)) pointToReturn.y += 1.4f;
        return pointToReturn;

    }

    /// <summary>
    /// Returns whether or not a pull is allowed at the given point
    /// </summary>
    /// <param name="point">The point to be pulled towards</param>
    /// <returns></returns>
    private bool PullAllowed(Vector3 point)
    {
        return Vector3.Distance(transform.position, point) >= minimumDistanceToBePulled;
    }
}
