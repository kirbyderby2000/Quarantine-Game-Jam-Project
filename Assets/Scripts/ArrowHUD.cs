using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHUD : MonoBehaviour
{
    public static ArrowHUD ArrowHUDInstance
    {
        private set;
        get;
    }

    [SerializeField] private List<Transform> pointingObjectList;

    [SerializeField] Transform currentPointingObject;

    private void Awake()
    {
        ArrowHUDInstance = this;
        EvaluateForClosestObject();
        StartCoroutine(MonitorClosestObjects());
    }

    private void Update()
    {
        if(currentPointingObject != null)
        {
            transform.LookAt(currentPointingObject);
        }
    }

    public void AddObjectToLookingList(Transform transform)
    {
        pointingObjectList.Add(transform);
        RemoveNullObjectsFromsList();
        EvaluateForClosestObject();
    }

    public void RemoveObjectFromLookingList(Transform transform)
    {
        if (pointingObjectList.Contains(transform))
        {
            pointingObjectList.Remove(transform);
            RemoveNullObjectsFromsList();
            EvaluateForClosestObject();
        }
    }

    private void RemoveNullObjectsFromsList()
    {
        pointingObjectList.RemoveAll(observingObject => observingObject == null);
    }

    private void EvaluateForClosestObject()
    {
        float closestObjectDistance = float.MaxValue;

        foreach (Transform objectRef in pointingObjectList)
        {
            if (objectRef == null) continue;
            float objectDistance = Vector3.Distance(objectRef.position, transform.position);
            if (objectDistance < closestObjectDistance)
            {
                closestObjectDistance = objectDistance;
                currentPointingObject = objectRef;
            } 
        }
    }

    IEnumerator MonitorClosestObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            RemoveNullObjectsFromsList();
            EvaluateForClosestObject();
        }
    }


}
