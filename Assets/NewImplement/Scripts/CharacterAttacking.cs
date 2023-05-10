using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterAttacking : Button
{
    [SerializeField] Button thisButton;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        OnAttack();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        UnAttack();
    }

    public void OnAttack()
    {
        GameElements.Instance.weaponController.OnAttack();
    }

    public void UnAttack()
    {
        GameElements.Instance.weaponController.UnAttack();
    }

}
