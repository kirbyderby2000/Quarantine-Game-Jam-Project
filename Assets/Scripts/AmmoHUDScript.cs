using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoHUDScript : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ammoHUDText;
    [SerializeField]
    private Animator animator;

    public void UpdateAmmoCountDisplayed(int count)
    {
        ammoHUDText.text = "x " + count.ToString();
        animator.SetTrigger("growthTrigger");
    }
}
