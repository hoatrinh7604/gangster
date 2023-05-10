using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeWeapon : Weapon
{
    [SerializeField] Collider hitCollider;
    [SerializeField] float timingEnableCollider = 0.5f;
    override public void Equip()
    {
        base.Equip();
    }

    public override void UnEquip()
    {
        base.UnEquip();
    }

    public override void UnEquipNow()
    {
        HandleShowMeshUnEquip();
    }

    public override void Attack()
    {
        base.Attack();
        hitCollider.enabled = true;
        Invoke("HandleCollider", timingEnableCollider);
    }

    public override void UnAttack()
    {
        base.UnAttack();
        //collider.enabled = false;
    }

    public void HandleCollider()
    {
        hitCollider.enabled = false;
    }

    public override void OnAttack(Rigidbody rigidBody)
    {
        base.OnAttack(rigidBody);
        Attack();
    }

    public override void SetInfo()
    {
        ID = (int)WEAPON.AXE;
    }
}
