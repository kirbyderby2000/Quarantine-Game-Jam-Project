using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionControl : MonoBehaviour
{
    [SerializeField] float minYPosition;
    [SerializeField] CharacterController charController;

    Vector3 lastGoodPosition;
    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        StartCoroutine(ControlPalyerPosition());
    }

    IEnumerator ControlPalyerPosition()
    {
        lastGoodPosition = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(3.0f);

            if(transform.position.y < minYPosition)
            {
                charController.enabled = false;
                transform.position = lastGoodPosition;
                charController.enabled = true;
            }
            else
            {
                lastGoodPosition = transform.position;
            }
        }
    }
}
