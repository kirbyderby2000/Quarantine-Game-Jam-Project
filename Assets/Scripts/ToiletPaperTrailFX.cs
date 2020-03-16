using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletPaperTrailFX : MonoBehaviour
{
    /// <summary>
    /// Reference to the Line Renderer component on this game object
    /// </summary>
    [SerializeField] LineRenderer tpLine;
    /// <summary>
    /// Toilet paper line unravelling speed
    /// </summary>
    [SerializeField] float lineUnravellingSpeed = 1.0f;

    Transform start, end;

    /// <summary>
    /// Whether or not the player is still latching onto the toilet paper
    /// </summary>
    bool latching = true;

    public void AssignTPLinePositions(Transform start, Transform end)
    {
        this.start = start;
        this.end = end;
        StartCoroutine(AnimateGrappleLine());
    }

    /// <summary>
    /// Method intended to be used as a coroutine to animate the TP grapple line
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimateGrappleLine()
    {
        // While the player is still latching to the TP, keep animating the TP line between the starting
        // and ending transform reference positions
        while (latching)
        {
            AssignLinePoints(start.position, end.position);
            yield return null;
        }

        // Cache a reference to the starting position of the TP line
        Vector3 startingPosition = start.position;
        Debug.Log("Trail FX line broke!");
        // While the starting position of the TP line is away from the end of the TP line, 
        // then keep retracting the start of the line towards the end of the line
        while(Vector3.Distance(startingPosition, end.position) > Mathf.Epsilon)
        {
            // Move the starting position towards the end reference position at the line break ravel speed
            startingPosition = Vector3.MoveTowards(startingPosition, end.position, lineUnravellingSpeed);
            AssignLinePoints(startingPosition, end.position);
            yield return null;
        }
        // When the TP line has been retracted, destroy the TP line
        DestroyTPLine();
    }

    /// <summary>
    /// Assigns the line points (starting and ending) to corresponding positions
    /// </summary>
    /// <param name="startingPoint">The starting point of the line</param>
    /// <param name="endingPoint">The ending point of the line</param>
    private void AssignLinePoints(Vector3 startingPoint, Vector3 endingPoint)
    {
        tpLine.SetPosition(0, startingPoint);
        tpLine.SetPosition(1, endingPoint);
    }

    /// <summary>
    /// Method called to break the TP line
    /// </summary>
    public void BreakTPLine()
    {
        latching = false;
    }

    /// <summary>
    /// Method called to destroy the TP line
    /// </summary>
    public void DestroyTPLine()
    {
        Destroy(this.gameObject);
    }


}
