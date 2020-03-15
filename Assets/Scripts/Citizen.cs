using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citizen : MonoBehaviour
{
    [SerializeField] GameObject triggerColliderPrefab;
    [SerializeField] GameObject loveParticleFX;

    private void Awake()
    {
        GetComponent<Animator>().Play("Idle_Wave");
        Instantiate(triggerColliderPrefab, transform).transform.localPosition = new Vector3(0.0f, 2.0f, 0.0f);
    }



    private void Start()
    {
        ArrowHUD.ArrowHUDInstance.AddObjectToLookingList(transform);
        StartCoroutine(RotateTowardsPlayer());
        StartCoroutine(SetToKinematicInSeconds(2.0f));
    }

    IEnumerator SetToKinematicInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GetComponent<Rigidbody>().isKinematic = true;

    }

    IEnumerator RotateTowardsPlayer()
    {
        Vector3 playerPosition;

        while (true)
        {
            playerPosition = PlayerPull.PlayerSingleton.transform.position;
            playerPosition.y = transform.position.y;
            transform.LookAt(playerPosition);
            yield return null;
        }
    }


    public void OnToiletPaperReceived()
    {
        GetComponent<Animator>().Play("Happy_Jump");
        Instantiate(loveParticleFX, transform.position + new Vector3(0.0f, 3.0f), loveParticleFX.transform.rotation);
        ArrowHUD.ArrowHUDInstance.RemoveObjectFromLookingList(transform);
        ScoreHUDScript.ScoreHUDSingleton.PointsReceived();
        StartCoroutine(DestroyInSeconds(3.0f));
    }

    IEnumerator DestroyInSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }

}
