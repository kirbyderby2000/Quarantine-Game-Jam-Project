using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitizenTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        transform.parent.GetComponent<Citizen>().OnToiletPaperReceived();
        GetComponent<CapsuleCollider>().enabled = false;
    }
}
