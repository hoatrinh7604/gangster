using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickController : MonoBehaviour
{
    [SerializeField] Animator groupWeaponAnimator;
    [SerializeField] RectTransform panel;
    [SerializeField] RectTransform groupWeapon;

    [SerializeField] int elementAmount;

    private bool isShown = false;

    private void Start()
    {
        GroupEnable(isShown);
        Invoke("UpdatePanelSize", 1);
    }

    public void GroupEnable(bool isEnable)
    {
        if(isEnable)
        {
            groupWeaponAnimator.Play("GroupWeaponEnable");
        }
        else
        {
            groupWeaponAnimator.Play("GroupWeaponDisable");
        }
    }

    public void HandleDisplayListWeapon()
    {
        isShown = !isShown;
        GroupEnable(isShown);
    }

    public void UpdatePanelSize()
    {
        Invoke("DelayUpdateSize", 0.1f);
    }

    private void DelayUpdateSize()
    {
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(groupWeapon.sizeDelta.x, groupWeapon.sizeDelta.y);
    }

}
