using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    //TODO: Make a real settings controller
    [SerializeField] Animator animatorController;
    bool canShow = true;

    public void ShowSettingWarningBar()
    {
        if (!canShow)
            return;

        animatorController.SetBool("ShowBar", true);
        canShow = false;
        StartCoroutine(PreparingToHide());
    }

    IEnumerator PreparingToHide()
    {
        yield return new WaitForSeconds(5f);
        canShow = true;
        animatorController.SetBool("ShowBar", false);
    }
}
